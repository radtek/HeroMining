using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Exchange
{
    /// <summary>
    /// A listed coin on exchange.
    /// </summary>
    public static class CoinListed
    {
        private static Dictionary<string, List<ExchangeName>> _coins = new Dictionary<string, List<ExchangeName>>();

        static CoinListed()
        {
            _coins.Add("GTM", new List<ExchangeName>() { ExchangeName.CryptoBridge });
            _coins.Add("MANO", new List<ExchangeName>() { ExchangeName.CryptoBridge });
            _coins.Add("IFX", new List<ExchangeName>() { ExchangeName.CryptoBridge });
            _coins.Add("GIN", new List<ExchangeName>() { ExchangeName.CryptoBridge });
            _coins.Add("VTL", new List<ExchangeName>() { ExchangeName.CryptoBridge });
            _coins.Add("MCT", new List<ExchangeName>() { ExchangeName.CryptoBridge });

        }

        /// <summary>
        /// List of exchange that coin listed.
        /// </summary>
        /// <param name="coinSymbol"></param>
        /// <returns>List of exchange.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static List<ExchangeName> GetCoinExchange(string coinSymbol)
        {
            if (_coins.ContainsKey(coinSymbol))
                return _coins[coinSymbol];
            return null;
        }

    }
}
