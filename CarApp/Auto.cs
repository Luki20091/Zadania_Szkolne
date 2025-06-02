using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CarApp
{
    public enum RodzajNapedu
    {
        benzynowy,
        diesel,
        hybrydowy,
        elektryczny
    }

    public class Auto : INotifyPropertyChanged
    {
        private string _marka;
        public string Marka
        {
            get => _marka;
            set
            {
                if (_marka != value)
                {
                    _marka = value;
                    OnPropertyChanged(nameof(Marka));
                }
            }
        }

        private string _model;
        public string Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged(nameof(Model));
                }
            }
        }

        private RodzajNapedu _rodzajNapedu;
        public RodzajNapedu RodzajNapedu
        {
            get => _rodzajNapedu;
            set
            {
                if (_rodzajNapedu != value)
                {
                    _rodzajNapedu = value;
                    OnPropertyChanged(nameof(RodzajNapedu));
                }
            }
        }

        private int _rok;
        public int Rok
        {
            get => _rok;
            set
            {
                if (_rok != value)
                {
                    _rok = value;
                    OnPropertyChanged(nameof(Rok));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
