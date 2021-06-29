using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.Models
{
    public class InstrumentMarketData
    {
        public string Ticker { get; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Source { get; set; }

        public InstrumentMarketData(string ticker)
        {
            Ticker = ticker;
            Price = 0;
            LastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Ticker}: {Price} From: {Source} at {LastUpdated} ";
        }
    }
}
