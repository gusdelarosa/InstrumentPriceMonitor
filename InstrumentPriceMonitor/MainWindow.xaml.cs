using InstrumentPriceMonitor.ViewModels;
using InstrumentPriceMonitorEngine;
using InstrumentPriceMonitorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstrumentPriceMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // TODO: Set up IOC/dependency injection
            var supportedTickerRepo = new TickerRepo(); //This is used to add support for unknown tickers since we assume any ticker is supported. 
            var nasdaq = new NSDQPricingSource(supportedTickerRepo);
            var arca = new ARCAPricingSource(supportedTickerRepo);
            var sources = new List<IPricingSource> { nasdaq, arca };            
            var engine = new InstrumentPriceMonitorEngineRunner(sources);

            var viewModel = new MainViewModel(engine, supportedTickerRepo);

            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
