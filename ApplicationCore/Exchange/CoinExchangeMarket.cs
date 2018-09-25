using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class CoinExchangeMarket
    {
        public string MarketID { get; set; }
        public string MarketAssetName { get; set; }
        public string MarketAssetCode { get; set; }
        public string MarketAssetID { get; set; }
        public string MarketAssetType { get; set; }
        public string BaseCurrency { get; set; }
        public string BaseCurrencyCode { get; set; }
        public string BaseCurrencyID { get; set; }
        public bool Active { get; set; }
    }

}
