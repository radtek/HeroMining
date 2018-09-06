using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class BxThbBtcOrderBook
    {
        public IList<IList<string>> bids { get; set; }
        public IList<IList<string>> asks { get; set; }
    }
}
