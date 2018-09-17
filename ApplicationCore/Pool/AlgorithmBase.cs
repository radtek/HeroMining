using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public class AlgorithmBase
    {
        public string name { get; set; }
        public int port { get; set; }
        public int coins { get; set; }
        public double fees { get; set; }
        public Int64? hashrate { get; set; }
        public int workers { get; set; }
        public double estimate_current { get; set; }
        public double estimate_last24h { get; set; }
        public double actual_last24h { get; set; }
        public double mbtc_mh_factor { get; set; }
        public Int64? hashrate_last24h { get; set; }
    }
}
