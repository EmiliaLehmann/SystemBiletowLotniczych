using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public abstract class Bilet
    {
        private double cena;
        public string NumerBiletu;
        public string ImiePasazera;
        public string NazwiskoPasazera;
        public double Cena
        {
            get { return cena; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Cena nie moze byc ujemna.");
                cena = value;
            }
        }




    }
}
