using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Miner
{
    public class MinerModel
    {

        /// <summary>
        /// get or set bot time to swap miner. default 1 hour.
        /// </summary>
        private int _swapTime;

        public int SwapTime
        {
            get { return _swapTime; }
            set { _swapTime = value; }
        }

        /// <summary>
        /// List of key (algor@pool) and value (miner path).
        /// </summary>
        public List<KeyValuePair<string,string>> Path { get; set; }

        /// <summary>
        /// Exclude algor of pool. Bot will overlook this item.
        /// </summary>
        public List<string> Exclude { get; set; }

    }
}
