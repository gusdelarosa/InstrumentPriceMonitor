using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine
{
    public class InstrumentDataObserver : IObserver<InstrumentMarketData>
    {
        public event EventHandler<InstrumentMarketData> OnInstrumentDataChange;

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(InstrumentMarketData value)
        {
            if (OnInstrumentDataChange == null)
            {
                return;
            }

            OnInstrumentDataChange(this, value);
        }
    }
}
