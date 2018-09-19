using CryptoMining.ApplicationCore.Exchange;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public class CryptoCurrencyResult: CurrencyBase, IComparable<CurrencyBase>
    {
        public PoolName Pool { get; set; }
        public ExchangeName Exchange { get; set; }

        public int CompareTo(CurrencyBase other)
        {
            return other.h24_btc.CompareTo(this.h24_btc);
        }
    }
}
