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
    public class InstrumentPriceMonitorEngineRunner : IInstrumentPriceMonitorEngine
    {
        private static ICollection<IPricingSource> _pricingSources;

        public InstrumentPriceMonitorEngineRunner(ICollection<IPricingSource> pricingSources)
        {
            _pricingSources = pricingSources;
        }

        public void Start()
        {
            foreach (var pricingSource in _pricingSources)
            {
                pricingSource.Start();
            }
        }

        public void Stop()
        {
            foreach (var pricingSource in _pricingSources)
            {
                pricingSource.Stop();
            }
        }

        public void Subscribe(string instrumentTicker, IObserver<InstrumentMarketData> observer)
        {            
            foreach (var pricingSource in _pricingSources)
            {
                pricingSource.Subscribe(instrumentTicker, observer);
            }
        }

        public void Unsubscribe(string instrumentTicker, IObserver<InstrumentMarketData> observer)
        {
            foreach (var pricingSource in _pricingSources)
            {
                pricingSource.Unsubscribe(instrumentTicker, observer);
            }
        }
    }
}
