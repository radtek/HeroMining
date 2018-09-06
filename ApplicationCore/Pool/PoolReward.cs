using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public static class PoolReward
    {
        private static Dictionary<string, double> _reward = new Dictionary<string, double>();

        static PoolReward()
        {
            _reward.Add("bsod_GTM", 10.0d);
            _reward.Add("bsod_IFX", 5.0d);
            _reward.Add("bsod_XDNA", 2.8d);

        }


        public static double GetPoolOverrideReward(PoolName poolName, string coinSymbol)
        {
            string key = poolName.ToString() + "_" + coinSymbol;
            if (_reward.ContainsKey(key))
            {
                return _reward[key];
            }
            return -1;
        }
    }
}
