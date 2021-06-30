using InstrumentPriceMonitorEngine.Interfaces;
using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine
{
    public class NSDQPricingSource : PricingSourceBase
    {
        public override string SourceName => "NSDQ";

        public NSDQPricingSource()
        {
            Interval = 500;
        }
    }
}
