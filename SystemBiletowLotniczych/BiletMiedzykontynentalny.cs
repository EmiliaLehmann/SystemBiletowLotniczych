using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BiletMiedzykontynentalny : Bilet
    {
        private bool wizaWymagana;
        private double dodatkowaOplataZapaliwo;

        public bool WizaWymagana { get => wizaWymagana; set => wizaWymagana = value; }
        public double DodatkowaOplataZapaliwo { get => dodatkowaOplataZapaliwo; set => dodatkowaOplataZapaliwo = value; }

        public BiletMiedzykontynentalny() : base()
        {
            WizaWymagana = false;
            DodatkowaOplataZapaliwo = 300;
        }

        public BiletMiedzykontynentalny( string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, string miastoPrzylotu, bool wizaWymagana, double dodatkowaOplataZapaliwo)
            : base( imiePasazera, nazwiskoPasazera, cena, dataWylotu, godzinaWylotu, klasa, miastoPrzylotu)
        {
            WizaWymagana = wizaWymagana;
            DodatkowaOplataZapaliwo = dodatkowaOplataZapaliwo;
        }

        public override double ObliczCeneKoncowa()
        {
            double cenabazowa = base.ObliczCeneKoncowa();
            if (WizaWymagana)
            {
                cenabazowa += 200; 
            }
            return cenabazowa+DodatkowaOplataZapaliwo;
        }

        public override string ToString()
        {
            return base.ToString() + $", Wiza wymagana: {WizaWymagana}, Dodatkowa opłata za paliwo: {DodatkowaOplataZapaliwo:C}";
        }

    }
}
