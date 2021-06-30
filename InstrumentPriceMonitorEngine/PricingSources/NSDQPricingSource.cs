using InstrumentPriceMonitorEngine.Interfaces;
using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.PricingSources
{
    public class NSDQPricingSource : PricingSourceBase
    {
        public override string SourceName => "NASDAQ";

        public NSDQPricingSource(ITickerRepo tickerRepo) : base(tickerRepo)
        {
            IntervalInMilliseconds = 500;
        }
    }
}
