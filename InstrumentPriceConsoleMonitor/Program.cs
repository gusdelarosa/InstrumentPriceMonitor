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

            engine.StartEngine();

            var instrumentDataObserver = new InstrumentDataObserver();
            instrumentDataObserver.OnInstrumentDataChange += OnInstrumentDataChange;
            engine.SubscribeToTicker("FSR", instrumentDataObserver);
            engine.SubscribeToTicker("HYG", instrumentDataObserver);
            Console.WriteLine("started");


            while(true)
            {

                string newTicker = Console.ReadLine();
                if(newTicker == "exit")
                {
                    engine.StopEngine();
                    break;
                }
                
                engine.SubscribeToTicker(newTicker, instrumentDataObserver);
            }


            Console.ReadLine();

            engine.StopEngine();
        }

        private static void OnInstrumentDataChange(object sender, InstrumentMarketData e)
        {
            Console.WriteLine(e);
        }
    }
}
