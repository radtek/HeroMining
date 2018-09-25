using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class CoinExchangeCurrency : ExchangeCurrency
    {
        private string _marketId; 

        public string MarketID
        {
            get { return _marketId; }
            set
            {
                _marketId = value;
                CoinExchangeMarket asset = CoinExchangeAsset.GetAsset(value);
                if (asset != null)
                {
                    symbol = asset.MarketAssetCode + "-" + asset.BaseCurrencyCode;
                }
            }
        }
        public string LastPrice { get; set; }
        public string Change { get; set; }
        public double? HighPrice { get; set; }
        public double? LowPrice { get; set; }
        public double Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        public string BTCVolume { get; set; }
        public string TradeCount { get; set; }
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
        public string BuyOrderCount { get; set; }
        public string SellOrderCount { get; set; }
    }

}
