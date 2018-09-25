using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class ExchangeCurrency
    {
        private string _symbol;

        /// <summary>
        /// format XXX-BTC
        /// </summary>
        public string symbol
        {
            get { return _symbol; }
            set { _symbol = value != null ? value.Replace('_', '-') : null; }
        }

        public double? last { get; set; }
        public double volume { get; set; }
        public double? ask { get; set; }
        public double? bid { get; set; }
    }
}
