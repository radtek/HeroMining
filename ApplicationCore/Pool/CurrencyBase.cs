using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public class CurrencyBase
    {

        public string algo { get; set; }
        public string port { get; set; }
        public string name { get; set; }
        public string reward { get; set; }
        public int height { get; set; }
        public string difficulty { get; set; }
        public int workers { get; set; }
        public int shares { get; set; }
        public Int64? hashrate { get; set; }
        public Int64? hashrate_shared { get; set; }
         public Int64? hashrate_solo { get; set; }
        public string network_hashrate { get; set; }
        public string estimate { get; set; }
        public string percent_blocks { get; set; }
        public int h24_blocks { get; set; }
        public int h24_blocks_shared { get; set; }
        public int h24_blocks_solo { get; set; }
        public double h24_btc { get; set; }
        public string h24_coins { get; set; }
        public int lastblock { get; set; }
        public int timesincelast { get; set; }
        public int hashRateDiscountPercent { get; set; }
        public string symbol { get; set; }
    }
}
