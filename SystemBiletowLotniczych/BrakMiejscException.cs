using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Wyjątek zgłaszany w przypadku braku wolnych miejsc na dany lot.
    /// </summary>
    public class BrakMiejscException : Exception
    {
        /// <summary>
        /// Inicjalizuje nową instancję wyjątku BrakMiejscException
        /// z określonym komunikatem błędu.
        /// </summary>
        /// <param name="wiadomosc">Treść komunikatu błędu.</param>
        public BrakMiejscException(string wiadomosc) : base(wiadomosc) { }

    }
}
