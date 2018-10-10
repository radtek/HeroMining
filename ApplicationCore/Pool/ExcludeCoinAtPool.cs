using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public static class ExcludeCoinAtPool
    {
        public static List<string> ExcludeCoins;
        static ExcludeCoinAtPool()
        {
            ExcludeCoins = new List<string>();
            ExcludeCoins.Add("RVN@Gos");
            ExcludeCoins.Add("MDEX@Gos");
        }
    }
}
