using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class CryptopiaCurrency : ExchangeCurrency
    {
        private string _label;

        public int TradePairId { get; set; }
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                if (!String.IsNullOrEmpty(value))
                    symbol = value.Replace("/BTC", "-BTC");
            }
        }
        public double? AskPrice
        {
            get { return ask; }
            set { ask = value; }
        }
        public double? BidPrice
        {
            get { return bid; }
            set { bid = value; }
        }

        public double? Low { get; set; }
        public double? High { get; set; }
        public double Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        public double LastPrice { get; set; }
        public double BuyVolume { get; set; }
        public double SellVolume { get; set; }
        public double Change { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double BaseVolume { get; set; }
        public double BuyBaseVolume { get; set; }
        public double SellBaseVolume { get; set; }
    }

}
