using InstrumentPriceMonitorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine
{
    public class ARCAPricingSource : PricingSourceBase
    {
        public override string SourceName => "ARCA";

        public ARCAPricingSource(ITickerRepo tickerRepo) : base(tickerRepo)
        {
            Interval = 200;
        }
    }
}
