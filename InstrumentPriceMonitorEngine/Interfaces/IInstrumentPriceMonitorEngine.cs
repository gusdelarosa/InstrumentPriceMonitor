using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.Interfaces
{
    public interface IInstrumentPriceMonitorEngine
    {
        void StartEngine();
        void StopEngine();
        void SubscribeToTicker(string instrumentTicker, IObserver<InstrumentMarketData> observer);
        void UnsubscribeToTicker(string instrumentTicker, IObserver<InstrumentMarketData> observer);
    }
}
