using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BiletKrajowy : Bilet
    {
        public double StawkaPodatkowa = 0.08;

        public BiletKrajowy() : base()
        {
        }

        public BiletKrajowy(string numerBiletu, string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, DateTime dataRezerwacji, string miastoWylotu, string miastoPrzylotu)
            : base(numerBiletu, imiePasazera, nazwiskoPasazera, cena, dataWylotu, godzinaWylotu, klasa, miastoPrzylotu)
        {
        }
        public override double ObliczCeneKoncowa()
        {
            double cenabazowa = base.ObliczCeneKoncowa();

            return cenabazowa * (1 + StawkaPodatkowa);
        }

        public override string ToString()
        {
            return base.ToString() + $", StawkaPodatkowa: {StawkaPodatkowa:P}";

        }
    }
}
