using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class BinanceCurrency : ExchangeCurrency
    {
        public new string symbol
        {
            get { return base.symbol; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (value.EndsWith("BTC"))
                        base.symbol = value.Replace("BTC", "-BTC");
                    else
                        base.symbol = value;
                }
                else
                {
                    base.symbol = value;
                }
            }
        }

        public string priceChange { get; set; }
        public string priceChangePercent { get; set; }
        public string weightedAvgPrice { get; set; }
        public string prevClosePrice { get; set; }
        public double? lastPrice
        {
            get { return last; }
            set { last = value; }
        }
        public string lastQty { get; set; }
        public double? bidPrice
        {
            get { return bid; }
            set { bid = value; }
        }
        public string bidQty { get; set; }
        public double? askPrice
        {
            get { return ask; }
            set { ask = value; }
        }
        public double? askQty { get; set; }
        public string openPrice { get; set; }
        public string highPrice { get; set; }
        public string lowPrice { get; set; }
        public string quoteVolume { get; set; }
        public object openTime { get; set; }
        public object closeTime { get; set; }
        public int firstId { get; set; }
        public int lastId { get; set; }
        public int count { get; set; }

    }

}
