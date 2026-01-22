using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Reprezentuje bilet krajowy, który dziedziczy po klasie Bilet
    /// i uwzględnia krajową stawkę podatkową.
    /// </summary>
    public class BiletKrajowy : Bilet
    {
        private double stawkaPodatkowa = 0.08;

        /// <summary>
        /// Tworzy nową instancję biletu krajowego z domyślnymi wartościami.
        /// </summary>
        public BiletKrajowy() : base()
        {
        }

        /// <summary>
        /// Tworzy nową instancję biletu krajowego z określonymi danymi pasażera,
        /// lotu oraz ceną biletu.
        /// </summary>
        public BiletKrajowy(string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, DateTime dataRezerwacji, string miastoWylotu, string miastoPrzylotu)
            : base(imiePasazera, nazwiskoPasazera, cena, dataWylotu, godzinaWylotu, klasa, miastoPrzylotu, miastoWylotu)
        {
        }

        public double StawkaPodatkowa { get => stawkaPodatkowa; set => stawkaPodatkowa = value; }

        /// <summary>
        /// Oblicza końcową cenę biletu krajowego, uwzględniając stawkę podatkową.
        /// </summary>
        /// <returns>Końcowa cena biletu po doliczeniu podatku.</returns>
        public override double ObliczCeneKoncowa()
        {
            double cenabazowa = base.ObliczCeneKoncowa();

            return cenabazowa * (1 + StawkaPodatkowa);
        }

        /// <summary>
        /// Zwraca tekstową reprezentację biletu krajowego,
        /// rozszerzoną o informację o stawce podatkowej.
        /// </summary>
        /// <returns>Opis biletu krajowego.</returns>
        public override string ToString()
        {
            return base.ToString() + $"StawkaPodatkowa: {StawkaPodatkowa:P}";

        }
    }
}
