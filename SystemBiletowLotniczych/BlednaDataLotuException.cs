using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public class BlednaDataLotuException: Exception
    {

        public BlednaDataLotuException(string message) : base(message)
        {
        }
    }
}
