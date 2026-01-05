using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public abstract class Bilet
    {
        private double cena;
        private string numerBiletu;
        private string imiePasazera;
        private string nazwiskoPasazera;
        private DateTime DataWylotu;
        private TimeOnly godzinaWylotu;


        public double Cena
        {
            get { return cena; }
            set
            {
                if (value < 0)
                    throw new ZlaCenaException("Cena nie moze byc ujemna.");
                cena = value;
            }
        }

        public string NumerBiletu { get => numerBiletu; set => numerBiletu = value; }
        public string ImiePasazera { get => imiePasazera; set => imiePasazera = value; }
        public string NazwiskoPasazera { get => nazwiskoPasazera; set => nazwiskoPasazera = value; }
        public DateTime DataWylotu1 { get => DataWylotu; set => DataWylotu = value; }
        public TimeOnly GodzinaWylotu { get => godzinaWylotu; set => godzinaWylotu = value; }
    }
}
