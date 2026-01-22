using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Reprezentuje bilet z przesiadkami, składający się z wielu etapów podróży,
    /// dla których obliczana jest łączna cena z uwzględnieniem zniżki.
    /// </summary>
    public class BiletZPrzesiadkami : Bilet
    {
        private List<Bilet> EtapyPodrozy;
        private string miastoKoncowe;
        private double znizka = 0.1;

        public List<Bilet> EtapyPodrozy1 { get => EtapyPodrozy; set => EtapyPodrozy = value; }
        public string MiastoKoncowe { get => miastoKoncowe; set => miastoKoncowe = value; }
        public double Znizka { get => znizka; set => znizka = value; }

        /// <summary>
        /// Tworzy nową instancję biletu z przesiadkami
        /// oraz inicjalizuje listę etapów podróży.
        /// </summary>
        public BiletZPrzesiadkami() : base()
        {
            EtapyPodrozy = new List<Bilet>();
        }

        /// <summary>
        /// Oblicza końcową cenę biletu z przesiadkami
        /// jako sumę cen poszczególnych etapów z uwzględnieniem zniżki.
        /// </summary>
        /// <returns>Końcowa cena biletu z przesiadkami.</returns>
        public new double ObliczCeneKoncowa()
        {
            double suma = 0;
            foreach (var bilet in EtapyPodrozy)
            {
                suma += bilet.ObliczCeneKoncowa();
            }

            return suma * (1 - znizka);
        }

        /// <summary>
        /// Generuje numer trasy na podstawie numerów lotów
        /// poszczególnych etapów podróży.
        /// </summary>
        /// <returns>Wygenerowany numer trasy.</returns>
        public string GenerujNumerTrasy()
        {
            if (EtapyPodrozy.Count == 0) return "BRAK-LOTOW";

            string numerTrasy = string.Empty;
            foreach (var bilet in EtapyPodrozy)
            {
                numerTrasy += bilet.NumerLotu.Substring(0, Math.Min(3, bilet.NumerLotu.Length)) + "-";
            }

            return $"{numerTrasy}NR-{NumerMiejsca:D3}";
        }
    }
}
