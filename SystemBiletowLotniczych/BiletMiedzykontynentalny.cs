using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BiletMiedzykontynentalny : Bilet, IUslugowy
    {
        private bool wizaWymagana;
        private List<string> wybranePosilki = new List<string>();
        private double cenaUslugDodatkowych;

        public bool WizaWymagana { get => wizaWymagana; set => wizaWymagana = value; }
        public double CenaUslugDodatkowych { get => cenaUslugDodatkowych; set => cenaUslugDodatkowych = value; }

        public BiletMiedzykontynentalny() : base()
        {
            WizaWymagana = false;
        }

        public BiletMiedzykontynentalny( string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, string miastoPrzylotu, string miastoWylotu, bool wizaWymagana, double dodatkowaOplataZapaliwo)
            : base( imiePasazera, nazwiskoPasazera, cena, dataWylotu, godzinaWylotu, klasa, miastoPrzylotu,  miastoWylotu)
        {
            WizaWymagana = wizaWymagana;
        }

        public override double ObliczCeneKoncowa()
        {
            double cenabazowa = base.ObliczCeneKoncowa();
            if (WizaWymagana)
            {
                cenabazowa += 200; 
            }
            return cenabazowa + CenaUslugDodatkowych;
        }

        public override string ToString()
        {
            return base.ToString() + $", Wiza wymagana: {WizaWymagana}.\n"+ 
                  $"Cena usług dodatkowych : {CenaUslugDodatkowych:C}." ;
        }

        public void DodajPosilek(string nazwaPosilku)
        {
            wybranePosilki.Add(nazwaPosilku);
            CenaUslugDodatkowych += 50.0;
        }

        public void WyswietlUslugi()
        {
            Console.WriteLine("Dodatkowe posiłki: " + string.Join(", ", wybranePosilki));
        }
    }
}
