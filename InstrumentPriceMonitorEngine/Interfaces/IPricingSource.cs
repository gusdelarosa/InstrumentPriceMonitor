using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.Interfaces
{
    public interface IPricingSource : IObservable<InstrumentMarketData>
    {
        void StartListening();
        void StopListening();
    }
}
