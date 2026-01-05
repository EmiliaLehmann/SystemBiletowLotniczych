using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    internal class ZlaCenaException : Exception
    {
        public ZlaCenaException(string message) : base(message)
        {
            Console.WriteLine(message);
        }

    }
}
