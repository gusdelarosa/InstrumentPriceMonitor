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
        protected int IntervalInMilliseconds { get; set; } = 2000;

        private List<IObserver<InstrumentMarketData>> _observers = new List<IObserver<InstrumentMarketData>>();        
        private readonly Timer _timer;
        private readonly ITickerRepo _tickerRepo;

        public PricingSourceBase(ITickerRepo tickerRepo)
        {
            _timer = new Timer(PublishPrices, null, Timeout.Infinite, Timeout.Infinite);
            _tickerRepo = tickerRepo;
        }

        public void StartListening()
        {
            _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(IntervalInMilliseconds));
        }

        public void StopListening()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void PublishPrices(object state)
        {
            foreach (var instrumentTicker in _tickerRepo.GetTickers())
            {
                var instrumentData = new InstrumentMarketData(instrumentTicker)
                {
                    Price = GenerateRandomPrice(),
                    LastUpdated = DateTime.Now,
                    Source = SourceName
                };

                foreach (var observer in _observers)
                {
                    observer.OnNext(instrumentData);
                }
            }
        }

        private double GenerateRandomPrice()
        {
            Random random = new Random();
            return Math.Round(random.NextDouble(), 2);
        }

        public IDisposable Subscribe(IObserver<InstrumentMarketData> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<InstrumentMarketData>> _observers;
            private IObserver<InstrumentMarketData> _observer;

            public Unsubscriber(List<IObserver<InstrumentMarketData>> observers, IObserver<InstrumentMarketData> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
