using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.Interfaces
{
    public interface IPricingSource
    {
        void Start();
        void Stop();
        void Subscribe(string instrumentTicker, IObserver<InstrumentMarketData> observer);
        void Unsubscribe(string instrumentTicker, IObserver<InstrumentMarketData> observer);
    }
}
