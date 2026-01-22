using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Wyjątek zgłaszany w przypadku podania niepoprawnej nazwy miasta,
    /// np. zbyt krótkiej lub pustej.
    /// </summary>
    public class BledneMiastoException : Exception
    {
        /// <summary>
        /// Inicjalizuje nową instancję wyjątku BledneMiastoException
        /// z określonym komunikatem błędu.
        /// </summary>
        /// <param name="wiadomosc">Treść komunikatu błędu.</param>
        public BledneMiastoException(string wiadomosc) : base(wiadomosc) { }

    }
}
