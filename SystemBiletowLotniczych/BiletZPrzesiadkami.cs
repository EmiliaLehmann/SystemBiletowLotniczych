using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SystemBiletowLotniczych
{
    [NotMapped]

    /// <summary>
    /// Reprezentuje typ biletu obsługujący podróże wieloetapowe
    /// </summary>
    public class BiletZPrzesiadkami : Bilet
    {
        private List<Bilet> EtapyPodrozy;
        private string miastoKoncowe;
        private double znizka = 0.1;

        public List<Bilet> EtapyPodrozy1 { get => EtapyPodrozy; set => EtapyPodrozy = value; }
        public string MiastoKoncowe { get => miastoKoncowe; set => miastoKoncowe = value; }
        public double Znizka { get => znizka; set => znizka = value; }

        public BiletZPrzesiadkami() : base()
        {
            EtapyPodrozy = new List<Bilet>();
        }
        /// <summary>
        /// Oblicza cene koncowa biletu z przesiadkami, z uwzglednieniem zniżki.
        /// </summary>
        /// <returns> Cena końcowa biletu.</returns>

        public override double ObliczCeneKoncowa()
            {
                double suma = 0;
                foreach (var bilet in EtapyPodrozy)
                {
                    suma += bilet.ObliczCeneKoncowa();
                }

                return suma * (1 - znizka);
            }

        /// <summary>
        /// Generuje numer trasy na podstawie numerów lotów w etapach podróży.
        /// </summary>
        /// <returns> Numer trasy. </returns>
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






