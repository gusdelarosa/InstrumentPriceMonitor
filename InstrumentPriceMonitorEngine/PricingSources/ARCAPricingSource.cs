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

        public ARCAPricingSource()
        {
            Interval = 200;
        }
    }
}
