using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Komparator umożliwiający porównywanie obiektów typu Bilet
    /// na podstawie daty wylotu.
    /// </summary>
    public class BiletPoDacieComparer : IComparer<Bilet>
    {
        /// <summary>
        /// Porównuje dwa bilety na podstawie daty ich wylotu.
        /// </summary>
        /// <param name="x">Pierwszy bilet do porównania.</param>
        /// <param name="y">Drugi bilet do porównania.</param>
        /// <returns>
        /// Wartość mniejsza od zera, zero lub większa od zera,
        /// w zależności od relacji dat wylotu porównywanych biletów.
        /// </returns>
        public int Compare(Bilet x, Bilet y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.DataWylotu.CompareTo(y.DataWylotu);
        }
    }
}
