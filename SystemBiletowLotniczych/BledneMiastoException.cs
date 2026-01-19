using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BledneMiastoException : Exception
    {

        public BledneMiastoException(string wiadomosc) : base(wiadomosc) { }

    }
}