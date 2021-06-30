using InstrumentPriceMonitor.Models;
using InstrumentPriceMonitor.Util;
using InstrumentPriceMonitorEngine;
using InstrumentPriceMonitorEngine.Interfaces;
using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace InstrumentPriceMonitor.ViewModels
{
    public class MainViewModel
    {

        private readonly IInstrumentPriceMonitorEngine _instrumenPriceEngine;
        private readonly ITickerRepo _supportedTickerRepo;
        private InstrumentDataObserver _instrumentObserver;
        private bool _isEngineRunning;

        public ObservableCollection<Instrument> SubscribedInstruments { get; private set; }
        private string _newTicker = "";
        public string NewTicker
        {
            get { return _newTicker; }

            set
            {
                _newTicker = value;
                SubscribeCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel(IInstrumentPriceMonitorEngine instrumenPriceEngine, ITickerRepo supportedTickerRepo)
        {
            _instrumenPriceEngine = instrumenPriceEngine;
            _supportedTickerRepo = supportedTickerRepo;
            _instrumentObserver = new InstrumentDataObserver();
            _instrumentObserver.OnInstrumentDataChange += OnInstrumentDataChange;
            SubscribedInstruments = new ObservableCollection<Instrument>();

            InitializeCommands();

        }

        private void InitializeCommands()
        {
            StartEngineCommand = new RelayCommand(StartEngine, CanStartEngine);
            StopEngineCommand = new RelayCommand(StopEngine, CanStopEngine);
            SubscribeCommand = new RelayCommand(SubscribeToInstrument, CanSubscribe);
        }

        private void OnInstrumentDataChange(object sender, InstrumentMarketData e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var changedInstrument = SubscribedInstruments.FirstOrDefault(i => i.Ticker == e.Ticker);

                if (changedInstrument != null)
                {
                    changedInstrument.Price = e.Price;
                    changedInstrument.SourceName = e.Source;
                    changedInstrument.LastUpdated = e.LastUpdated;
                }
            }));
        }

        public void StartEngine()
        {
            if (_isEngineRunning)
            {
                return;
            }

            _instrumenPriceEngine.StartEngine();
            _isEngineRunning = true;
            RaiseCanExecuteChanged();
        }

        public void StopEngine()
        {
            if (!_isEngineRunning)
            {
                return;
            }

            _instrumenPriceEngine.StopEngine();
            _isEngineRunning = false;
            RaiseCanExecuteChanged();
        }

        public void SubscribeToInstrument()
        {
            var ticker = NewTicker.ToUpperInvariant();

            if (string.IsNullOrEmpty(ticker))
            {
                return;
            }
            Instrument instrument = SubscribedInstruments.FirstOrDefault(i => i.Ticker == ticker);

            if (instrument == null)
            {
                SubscribedInstruments.Add(new Instrument(ticker));
                _supportedTickerRepo.AddTickerSupportIfDoesNotExist(ticker);
                _instrumenPriceEngine.SubscribeToTicker(ticker, _instrumentObserver);
            }

            NewTicker = string.Empty;
            RaiseCanExecuteChanged();
        }

        public void UnsubscribeFromInstrument(string ticker)
        {
            Instrument instrument = SubscribedInstruments.FirstOrDefault(i => i.Ticker == ticker);

            if (instrument != null)
            {

                _instrumenPriceEngine.UnsubscribeToTicker(ticker, _instrumentObserver);
                SubscribedInstruments.Remove(instrument);
            }
        }

        public RelayCommand StartEngineCommand { get; private set; }

        private bool CanStartEngine()
        {
            return !_isEngineRunning;
        }

        public RelayCommand StopEngineCommand { get; private set; }

        private bool CanStopEngine()
        {
            return _isEngineRunning;
        }

        private void RaiseCanExecuteChanged()
        {
            StopEngineCommand.RaiseCanExecuteChanged();
            StartEngineCommand.RaiseCanExecuteChanged();
            SubscribeCommand.RaiseCanExecuteChanged();
        }

        public RelayCommand SubscribeCommand { get; private set; }

        private bool CanSubscribe()
        {            
            return !string.IsNullOrEmpty(NewTicker);
        }

    }
}
