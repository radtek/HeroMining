using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class Crex24Currency : ExchangeCurrency
    {
        public string instrument { get { return symbol; } set { symbol = value; } }
        public double? percentChange { get; set; }
        public double? low { get; set; }
        public double? high { get; set; }
        public double baseVolume { get; set; }
        public double quoteVolume { get; set; }
        public double volumeInBtc { get { return volume; } set { volume = value; } }
        public double volumeInUsd { get; set; }
        public DateTime timestamp { get; set; }

    }
}
