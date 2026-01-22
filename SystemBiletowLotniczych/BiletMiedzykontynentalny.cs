using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Reprezentuje bilet międzykontynentalny, który może wymagać wizy
    /// oraz obsługuje usługi dodatkowe zgodnie z interfejsem IUslugowy.
    /// </summary>
    public class BiletMiedzykontynentalny : Bilet, IUslugowy
    {
        private bool wizaWymagana;
        private List<string> wybranePosilki = new List<string>();
        private double cenaUslugDodatkowych;

        public bool WizaWymagana { get => wizaWymagana; set => wizaWymagana = value; }
        public double CenaUslugDodatkowych { get => cenaUslugDodatkowych; set => cenaUslugDodatkowych = value; }

        /// <summary>
        /// Tworzy nową instancję biletu międzykontynentalnego
        /// z domyślnymi wartościami.
        /// </summary>
        public BiletMiedzykontynentalny() : base()
        {
            WizaWymagana = false;
        }

        /// <summary>
        /// Tworzy nową instancję biletu międzykontynentalnego
        /// z określonymi danymi pasażera, lotu oraz informacją o wymaganej wizie.
        /// </summary>
        public BiletMiedzykontynentalny(string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, string miastoPrzylotu, string miastoWylotu, bool wizaWymagana, double dodatkowaOplataZapaliwo)
            : base(imiePasazera, nazwiskoPasazera, cena, dataWylotu, godzinaWylotu, klasa, miastoPrzylotu, miastoWylotu)
        {
            WizaWymagana = wizaWymagana;
        }

        /// <summary>
        /// Oblicza końcową cenę biletu międzykontynentalnego,
        /// uwzględniając opłatę wizową oraz usługi dodatkowe.
        /// </summary>
        /// <returns>Końcowa cena biletu.</returns>
        public override double ObliczCeneKoncowa()
        {
            double cenabazowa = base.ObliczCeneKoncowa();
            if (WizaWymagana)
            {
                cenabazowa += 200;
            }
            return cenabazowa + CenaUslugDodatkowych;
        }

        /// <summary>
        /// Zwraca tekstową reprezentację biletu międzykontynentalnego,
        /// zawierającą informacje o wizie oraz usługach dodatkowych.
        /// </summary>
        /// <returns>Opis biletu międzykontynentalnego.</returns>
        public override string ToString()
        {
            return base.ToString() +
                   $"Wiza wymagana: {(WizaWymagana ? "TAK" : "NIE")}\n" +
                   $"Cena usług dodatkowych: {CenaUslugDodatkowych:C}.";
        }

        /// <summary>
        /// Dodaje dodatkowy posiłek do biletu oraz zwiększa cenę usług dodatkowych.
        /// </summary>
        /// <param name="nazwaPosilku">Nazwa wybranego posiłku.</param>
        public void DodajPosilek(string nazwaPosilku)
        {
            wybranePosilki.Add(nazwaPosilku);
            CenaUslugDodatkowych += 50.0;
        }

        /// <summary>
        /// Wyświetla na konsoli listę wybranych usług dodatkowych,
        /// w tym dodatkowe posiłki.
        /// </summary>
        public void WyswietlUslugi()
        {
            Console.WriteLine("Dodatkowe posiłki:");
            foreach (var posilek in wybranePosilki)
            {
                Console.WriteLine(posilek);
            }
        }
    }
}
