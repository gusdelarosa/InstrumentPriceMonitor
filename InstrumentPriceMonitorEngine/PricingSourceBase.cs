using InstrumentPriceMonitorEngine.Interfaces;
using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine
{
    public abstract class PricingSourceBase : IPricingSource
    {
        public abstract string SourceName { get; }
        protected int Interval { get; set; } = 2000;

        private Dictionary<string, List<IObserver<InstrumentMarketData>>> _subscriptions = new Dictionary<string, List<IObserver<InstrumentMarketData>>>();
        private List<InstrumentMarketData> _instruments = new List<InstrumentMarketData>();
        private readonly Timer _timer;

        public PricingSourceBase()
        {
            _timer = new Timer(PublishPrices, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start()
        {
            _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(Interval));
        }

        private void PublishPrices(object state)
        {
            foreach (var subscribedTicker in _subscriptions.Keys)
            {
                InstrumentMarketData instrument = _instruments.FirstOrDefault(i => i.Ticker == subscribedTicker);

                if (instrument == null)
                {
                    continue;
                }

                instrument.Price = GenerateRandomPrice();
                instrument.LastUpdated = DateTime.Now;
                instrument.Source = SourceName;

                var observers = _subscriptions[subscribedTicker];
                foreach (var observer in observers)
                {
                    observer.OnNext(instrument);
                }
            }
        }

        private double GenerateRandomPrice()
        {
            Random random = new Random();
            return random.NextDouble();
        }

        public void Stop()
        {
            _timer.Dispose();
        }

        public void Subscribe(string instrumentTicker, IObserver<InstrumentMarketData> observer)
        {
            //add to instrument list 
            InstrumentMarketData instrumentMarketData = _instruments.FirstOrDefault(i => i.Ticker == instrumentTicker);

            if (instrumentMarketData == null)
            {
                instrumentMarketData = new InstrumentMarketData(instrumentTicker);

                _instruments.Add(instrumentMarketData);
                _subscriptions[instrumentTicker] = new List<IObserver<InstrumentMarketData>> { observer };
                return;
            }
            else
            {
                List<IObserver<InstrumentMarketData>> instrumentObservers = _subscriptions[instrumentTicker];

                if (!instrumentObservers.Contains(observer))
                {
                    instrumentObservers.Add(observer);
                }
            }
            //TODO: Dispose

        }

        public void Unsubscribe(string instrumentTicker, IObserver<InstrumentMarketData> observer)
        {
            if (_subscriptions.ContainsKey(instrumentTicker))
            {
                _subscriptions[instrumentTicker].Remove(observer);
            }
        }

        public IDisposable Subscribe(IObserver<InstrumentMarketData> observer)
        {
            throw new NotImplementedException();
        }
    }
}
