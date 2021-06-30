using InstrumentPriceMonitorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine
{
    public class TickerRepo : ITickerRepo
    {
        private List<string> _supportedTickers;

        public TickerRepo()
        {
            _supportedTickers = new List<string>() { "FSR", "HYG", "AAPL", "SPY" };
        }

        public void AddTickerSupportIfDoesNotExist(string ticker)
        {
            var tickerExists = _supportedTickers.Any(t => t == ticker.ToUpperInvariant());
            if (!tickerExists)
            {
                _supportedTickers.Add(ticker.ToUpperInvariant());
            }
        }

        public IEnumerable<string> GetTickers()
        {
            return _supportedTickers;
        }
    }
}
