using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BiletKrajowy bk1 = new BiletKrajowy("Olga","Redwan",250,new DateTime(2026,12,12),new TimeOnly(15,20),EnumKlasa.Ekonomiczna,DateTime.Now,"Kopenhaga","Krakow");
            BiletKrajowy bk2 = new BiletKrajowy("Natan", "Wojcik", 200, new DateTime(2026, 12, 12), new TimeOnly(15, 20), EnumKlasa.Ekonomiczna, DateTime.Now, "Kopenhaga", "Krakow");
            
        }
    }
}
