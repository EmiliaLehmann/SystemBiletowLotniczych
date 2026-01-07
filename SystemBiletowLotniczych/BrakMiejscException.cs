using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BrakMiejscException:Exception
    {

        public BrakMiejscException(string wiadomosc) : base(wiadomosc) { }

    }
}
