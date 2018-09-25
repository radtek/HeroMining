using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    public static class CoinExchangeAsset
    {
        static private SortedDictionary<string, CoinExchangeMarket> _asset = new SortedDictionary<string, CoinExchangeMarket>();

        static public void Add(string id, CoinExchangeMarket asset)
        {
            _asset.Add(id, asset);
        }


        static public void Add(List<CoinExchangeMarket> assets)
        {
            foreach (CoinExchangeMarket asset in assets)
            {
                Add(asset.MarketID, asset);
            }
        }

        static public CoinExchangeMarket GetAsset(string id)
        {
            if (_asset.ContainsKey(id))
                return _asset[id];
            else
                return null;
        }
    }
}
