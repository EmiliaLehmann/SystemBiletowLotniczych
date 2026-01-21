using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BiletMiedzykrajowy: Bilet
    {
        private double dodatkoweOplaty = 50;
        public BiletMiedzykrajowy() : base()
        {
        }
        public BiletMiedzykrajowy( string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, string miastoPrzylotu, string miastoWylotu)
            : base( imiePasazera, nazwiskoPasazera, cena, dataWylotu, godzinaWylotu, klasa, miastoPrzylotu,  miastoWylotu)
        {
        }

        public double DodatkoweOplaty { get => dodatkoweOplaty; set => dodatkoweOplaty = value; }

        public override double stawkaPodatkowa => 0.15;
        public override double ObliczCeneKoncowa()
        {
            double cenabazowa = base.ObliczCeneKoncowa();
            return (cenabazowa + DodatkoweOplaty) * (1+ stawkaPodatkowa);
        }
        public override string ToString()
        {
            return base.ToString() + $", Dodatkowa opłata: {DodatkoweOplaty:P}";
        }
    }
}
