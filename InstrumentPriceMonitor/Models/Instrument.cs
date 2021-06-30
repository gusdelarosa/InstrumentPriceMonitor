using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentPriceMonitor.Models
{
    public class Instrument : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Ticker { get; private set; }

        private string _sourceName;

        public string SourceName
        {
            get { return _sourceName; }
            set
            {
                _sourceName = value;
                NotifyPropertyChanged();
            }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _lastUpdated;

        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                _lastUpdated = value;
                NotifyPropertyChanged();
            }
        }

        public Instrument(string ticker)
        {
            Ticker = ticker;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
