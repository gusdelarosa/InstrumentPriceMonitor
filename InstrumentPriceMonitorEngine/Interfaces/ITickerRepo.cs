using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitorEngine.Interfaces
{
    public interface ITickerRepo
    {
        IEnumerable<string> GetTickers();
        void AddTickerSupportIfDoesNotExist(string ticker);
    }
}
