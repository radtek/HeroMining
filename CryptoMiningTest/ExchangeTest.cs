using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoMining.ApplicationCore.Pool;
using CryptoMining.ApplicationCore.Exchange;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoMining.ApplicationCore;
using System.Diagnostics;
using System.Text;

namespace CryptoMiningTest
{
    [TestClass]
    public class ExchangeTest
    {
        [TestMethod]
        public void TestDeserializeCryptopia()
        {
            CryptopiaAPI api = new CryptopiaAPI();
            List<CryptopiaCurrency> priceList = api.LoadPrice();
            Assert.AreEqual(true, priceList.Count > 0);
        }

        [TestMethod]
        public void TestLoadThaiBahtBtcPrice()
        {
            BxAPI api = new BxAPI();
            BxThbBtcOrderBook orderbook = api.LoadThaiBahtBtcPrice();
            double bahtPerBtcPrice = double.Parse(orderbook.bids[0][0]); // bid price
            Assert.AreEqual(true, bahtPerBtcPrice > 0);

        }

        [TestMethod]
        public void TestLoadCurrencyFromCryptoBridge()
        {
            CryptoBridgeAPI api = new CryptoBridgeAPI();
            List<CryptoBridgeCurrency> coins = api.LoadPrice();
            foreach (ExchangeCurrency coin in coins)
            {
                Debug.WriteLine(string.Format("{0} bid={1} ask={2} last={3} volume={4} ", coin.symbol, coin.bid, coin.ask, coin.last, coin.volume));
            }
            Assert.AreEqual(true, coins.Count > 0);
        }

        [TestMethod]
        public void TestLoadCurrencyFromCryptopia()
        {
            CryptopiaAPI api = new CryptopiaAPI();
            List<CryptopiaCurrency> coins = api.LoadPrice();
            foreach (ExchangeCurrency coin in coins)
            {
                Debug.WriteLine(string.Format("{0} bid={1} ask={2} last={3} volume={4} ", coin.symbol, coin.bid, coin.ask, coin.last, coin.volume));
            }
            Assert.AreEqual(true, coins.Count > 0);
        }


        [TestMethod]
        public void TestLoadCurrencyFromBinance()
        {
            BinanceAPI api = new BinanceAPI();
            List<BinanceCurrency> coins = api.LoadPrice();
            foreach (ExchangeCurrency coin in coins)
            {
                Debug.WriteLine(string.Format("{0} bid={1} ask={2} last={3} volume={4} ", coin.symbol, coin.bid, coin.ask, coin.last, coin.volume));
            }
            Assert.AreEqual(true, coins.Count > 0);
        }


        [TestMethod]
        public void TestLoadCurrencyFromCoinExchangeIO()
        {
            CoinExchangeAPI api = new CoinExchangeAPI();
            List<CoinExchangeCurrency> coins = api.LoadPrice();
            foreach (ExchangeCurrency coin in coins)
            {
                Debug.WriteLine(string.Format("{0} bid={1} ask={2} last={3} volume={4} ", coin.symbol, coin.bid, coin.ask, coin.last, coin.volume));
            }
            Assert.AreEqual(true, coins.Count > 0);
        }


        [TestMethod]
        public void TestUSDTFromBinance()
        {
            BinanceAPI api = new BinanceAPI();
            List<BinanceCurrency> coins = api.LoadPrice();
            foreach (ExchangeCurrency coin in coins)
            {
                if (coin.symbol == "BTCUSDT")
                {
                    Debug.WriteLine(string.Format("{0} bid={1} ask={2} last={3} volume={4} ", coin.symbol, coin.bid, coin.ask, coin.last, coin.volume));
                    double btc = 0.005;
                    double btcusd = (coin.bid ?? 0);
                    double usd = btcusd * btc;
                    Debug.WriteLine($"usd of {btc} btc = {usd}");
                }
            }
            Assert.AreEqual(true, coins.Count > 0);
        }
    }
}