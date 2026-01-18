using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BiletPoDacieComparer: IComparer<Bilet>
    {
        public int Compare(Bilet x, Bilet y)
        {
            if (x == null && y == null) return 0;
            if(x == null) return -1;
            if(y == null) return 1;

            return x.DataWylotu.CompareTo(y.DataWylotu);
        }
    }

}