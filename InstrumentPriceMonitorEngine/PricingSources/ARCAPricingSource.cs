using InstrumentPriceMonitorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.PricingSources
{
    public class ARCAPricingSource : PricingSourceBase
    {
        public override string SourceName => "ARCA";

        public ARCAPricingSource(ITickerRepo tickerRepo) : base(tickerRepo)
        {
            IntervalInMilliseconds = 200;
        }
    }
}
