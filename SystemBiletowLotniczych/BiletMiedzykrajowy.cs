using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Reprezentuje bilet międzynarodowy (międzykrajowy),
    /// który uwzględnia stałe dodatkowe opłaty doliczane do ceny bazowej.
    /// </summary>
    public class BiletMiedzykrajowy : Bilet
    {
        private double dodatkoweOplaty = 50;

        /// <summary>
        /// Tworzy nową instancję biletu międzykrajowego
        /// z domyślnymi wartościami.
        /// </summary>
        public BiletMiedzykrajowy() : base()
        {
        }

        /// <summary>
        /// Tworzy nową instancję biletu międzykrajowego
        /// z określonymi danymi pasażera oraz lotu.
        /// </summary>
        public BiletMiedzykrajowy(string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, string miastoPrzylotu, string miastoWylotu)
            : base(imiePasazera, nazwiskoPasazera, cena, dataWylotu, godzinaWylotu, klasa, miastoPrzylotu, miastoWylotu)
        {
        }

        public double DodatkoweOplaty { get => dodatkoweOplaty; set => dodatkoweOplaty = value; }

        /// <summary>
        /// Oblicza końcową cenę biletu międzykrajowego,
        /// doliczając stałe dodatkowe opłaty do ceny bazowej.
        /// </summary>
        /// <returns>Końcowa cena biletu.</returns>
        public override double ObliczCeneKoncowa()
        {
            double cenabazowa = base.ObliczCeneKoncowa();
            return cenabazowa + DodatkoweOplaty;
        }

        /// <summary>
        /// Zwraca tekstową reprezentację biletu międzykrajowego,
        /// uwzględniając dodatkowe opłaty.
        /// </summary>
        /// <returns>Opis biletu międzykrajowego.</returns>
        public override string ToString()
        {
            return base.ToString() + $"Dodatkowa opłata: {DodatkoweOplaty:C}";
        }
    }
}
