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
        private double _swapTime;

        /// <summary>
        /// Swap hour time eg: 24 = 1 day.
        /// </summary>
        public double SwapTime
        {
            get { return _swapTime; }
            set { _swapTime = value; }
        }

        /// <summary>
        /// True if use current estimate else use 24 hour to compare best price
        /// </summary>
        public bool UseCurrent { get; set; }

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
