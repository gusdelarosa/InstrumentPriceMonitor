using InstrumentPriceMonitorEngine;
using InstrumentPriceMonitorEngine.Interfaces;
using InstrumentPriceMonitorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceConsoleMonitor
{
    class Program
    {
        static void Main(string[] args)
        {

            var nasdaq = new NSDQPricingSource();
            var arca = new ARCAPricingSource();
            var sources = new List<IPricingSource> { nasdaq, arca };

            var engine = new InstrumentPriceMonitorEngineRunner(sources);

            engine.Start();

            var instrumentDataObserver = new InstrumentDataObserver();
            instrumentDataObserver.OnInstrumentDataChange += OnInstrumentDataChange;
            engine.Subscribe("FSR", instrumentDataObserver);
            Console.WriteLine("started");

            string newTicker = Console.ReadLine();

            engine.Subscribe(newTicker, instrumentDataObserver);



            Console.ReadLine();

        }

        private static void OnInstrumentDataChange(object sender, InstrumentMarketData e)
        {
            Console.WriteLine(e);
        }
    }
}
