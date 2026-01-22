using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Wyjątek zgłaszany w przypadku podania nieprawidłowej daty lotu,
    /// np. daty wcześniejszej niż bieżąca.
    /// </summary>
    public class BlednaDataLotuException : Exception
    {
        /// <summary>
        /// Inicjalizuje nową instancję wyjątku BlednaDataLotuException
        /// z określonym komunikatem błędu.
        /// </summary>
        /// <param name="message">Treść komunikatu błędu.</param>
        public BlednaDataLotuException(string message) : base(message)
        {
        }
    }
}
