using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public interface IUslugowy
    {

        double CenaUslugDodatkowych { get; set; }
        void DodajPosilek(string nazwaPosilku);
        void WyswietlUslugi();
    }


}
