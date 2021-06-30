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
    public class InstrumentPriceMonitorEngineRunner : IInstrumentPriceMonitorEngine, IObserver<InstrumentMarketData>
    {
        private Dictionary<string, List<IObserver<InstrumentMarketData>>> _subscriptions = new Dictionary<string, List<IObserver<InstrumentMarketData>>>();
        private static ICollection<IPricingSource> _pricingSources;
        private static ICollection<IDisposable> _priceSourceSubscribers = new List<IDisposable>();

        public InstrumentPriceMonitorEngineRunner(ICollection<IPricingSource> pricingSources)
        {
            _pricingSources = pricingSources;
        }

        public void StartEngine()
        {
            SubscribeToPriceSources();
        }

        public void StopEngine()
        {
            UnsubscribeToPriceSources();
        }

        private void SubscribeToPriceSources()
        {
            foreach (var pricingSource in _pricingSources)
            {
                pricingSource.StartListening();
                var priceSourceSubscriber = pricingSource.Subscribe(this);
                _priceSourceSubscribers.Add(priceSourceSubscriber);
            }
        }

        private void UnsubscribeToPriceSources()
        {
            foreach (var pricingSource in _pricingSources)
            {
                pricingSource.StopListening();
            }

            foreach (var priceSourceSubscriber in _priceSourceSubscribers)
            {
                priceSourceSubscriber.Dispose();
            }
        }

        public void SubscribeToTicker(string instrumentTicker, IObserver<InstrumentMarketData> observer)
        {
            if (_subscriptions.TryGetValue(instrumentTicker, out List<IObserver<InstrumentMarketData>> observers))
            {
                observers.Add(observer);
            }
            else
            {
                _subscriptions[instrumentTicker] = new List<IObserver<InstrumentMarketData>> { observer };
            }
        }

        public void UnsubscribeToTicker(string instrumentTicker, IObserver<InstrumentMarketData> observer)
        {
            if (_subscriptions.TryGetValue(instrumentTicker, out List<IObserver<InstrumentMarketData>> observers))
            {
                observers.Remove(observer);
            }
        }

        public void OnNext(InstrumentMarketData value)
        {
            if (_subscriptions.TryGetValue(value.Ticker, out List<IObserver<InstrumentMarketData>> observers))
            {
                foreach (var observer in observers)
                {
                    observer.OnNext(value);
                }
            }
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
    }
}
