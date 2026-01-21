using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BiletKrajowy : Bilet
    {
        
        public BiletKrajowy() : base()
        {
        }

        public BiletKrajowy( string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, DateTime dataRezerwacji, string miastoWylotu, string miastoPrzylotu)
            : base( imiePasazera, nazwiskoPasazera, cena, dataWylotu, godzinaWylotu, klasa, miastoPrzylotu, miastoWylotu)
        {
        }

        public override double stawkaPodatkowa => 0.08;

        public override double ObliczCeneKoncowa()
        {
            double cenabazowa = base.ObliczCeneKoncowa();

            return cenabazowa * (1 + stawkaPodatkowa);
        }

        public override string ToString()
        {
            return base.ToString() + $", StawkaPodatkowa: {stawkaPodatkowa:P}";

        }
    }
}
