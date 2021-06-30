using InstrumentPriceMonitorEngine;
using InstrumentPriceMonitorEngine.Interfaces;
using InstrumentPriceMonitorEngine.Models;
using InstrumentPriceMonitorEngine.PricingSources;
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
            
            //Dependency Injection
            var tickerRepo = new TickerRepo();
            var nasdaq = new NSDQPricingSource(tickerRepo);
            var arca = new ARCAPricingSource(tickerRepo);
            var sources = new List<IPricingSource> { nasdaq, arca };

            //WPF
            var engine = new InstrumentPriceMonitorEngineRunner(sources);
            engine.StartEngine();

            var instrumentDataObserver = new InstrumentDataObserver();
            instrumentDataObserver.OnInstrumentDataChange += OnInstrumentDataChange;
            //add subscription

            engine.SubscribeToTicker("FSR", instrumentDataObserver);
            engine.SubscribeToTicker("HYG", instrumentDataObserver);
            engine.SubscribeToTicker("GG", instrumentDataObserver);

            Console.ReadLine();
            tickerRepo.AddTickerSupportIfDoesNotExist("GG");



            Console.WriteLine("started");


            while (true)
            {

                string newTicker = Console.ReadLine();
                if (newTicker == "exit")
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
