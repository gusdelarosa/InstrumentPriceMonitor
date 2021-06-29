using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.Models
{
    public class Instrument
    {
        public string Ticker { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Source { get; set; }
    }
}
