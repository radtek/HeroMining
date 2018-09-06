using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class CryptoBridgeCurrency : ExchangeCurrency
    {
        public string id { get { return symbol; } set { symbol = value; } }

    }
}
