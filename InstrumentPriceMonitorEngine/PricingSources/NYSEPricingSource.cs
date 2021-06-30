using InstrumentPriceMonitorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.PricingSources
{
    public class NYSEPricingSource : PricingSourceBase
    {
        public override string SourceName => "NYSE";

        public NYSEPricingSource(ITickerRepo tickerRepo) : base(tickerRepo)
        {
            IntervalInMilliseconds = 700;
        }
    }
}
