﻿using System;
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
    public class ApplicationCoreTest
    {
       
        [TestMethod]
        public void TestLoadCurrencyFromCrex24()
        {
            Crex24API api = new Crex24API();
            List<Crex24Currency> coins = api.LoadPrice();
            foreach (ExchangeCurrency coin in coins)
            {
                Debug.WriteLine(string.Format("{0} bid={1} ask={2} last={3} volume={4} ", coin.symbol, coin.bid, coin.ask, coin.last, coin.volume));
            }
            Assert.AreEqual(true, coins.Count > 0);
        }





   
        [TestMethod]
        public void TestLoadCryptoBridgeCoinPrice()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // HTTP POST
                client.BaseAddress = new Uri("https://api.crypto-bridge.org");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("/api/v1/ticker").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                    List<CryptoBridgeCurrency> priceList = JsonConvert.DeserializeObject<List<CryptoBridgeCurrency>>(res);

                    Assert.AreEqual(true, priceList.Count > 0);

                }
            }


        }

        [TestMethod]
        public void TestGetCoinPriceMiningPerday()
        {
            MiningCalculator calc = new MiningCalculator();
            calc.MyHashRate = 100000000L;
            double miningManoBtcPerDay = calc.GetTotalBtcMiningPerday("MANO", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningIfxBtcPerDay = calc.GetTotalBtcMiningPerday("IFX", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningGinBtcPerDay = calc.GetTotalBtcMiningPerday("GIN", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningVtlBtcPerDay = calc.GetTotalBtcMiningPerday("VTL", PoolName.Bsod, ExchangeName.CryptoBridge);

            Assert.AreEqual(true, miningManoBtcPerDay > 0);
        }




        [TestMethod]
        public void TestGetTotalBahtMiningFromBsodPerday()
        {

            MiningCalculator calc = new MiningCalculator();
            calc.MyHashRate = 120000000L;
            double miningManoBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("MANO", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningIfxBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("IFX", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningGinBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("GIN", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningVtlBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("VTL", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningMctBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("MCT", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningGtmBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("GTM", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningMeriBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("MERI", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningFxTCBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("FXTC", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningAlpsBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("ALPS", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningCrsBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("CRS", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);

            calc.MyHashRate = 350000000L;
            double miningXdnaBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("XDNA", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);


            System.Diagnostics.Debug.WriteLine(string.Format("MANO = {0}", miningManoBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("IFX = {0}", miningIfxBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("GIN = {0}", miningGinBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("VTL = {0}", miningVtlBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("MCT = {0}", miningMctBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("GTM = {0}", miningGtmBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("MERI = {0}", miningMeriBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("FxTC = {0}", miningFxTCBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("APLS = {0}", miningAlpsBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("CRS = {0}", miningCrsBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("XDNA = {0}", miningXdnaBahtPerDay));


            Assert.AreEqual(true, miningManoBahtPerDay > 0);
        }

        [TestMethod]
        public void TestGetTotalBahtMiningFromGosPerday()
        {

            MiningCalculator calc = new MiningCalculator();
            calc.MyHashRate = 120000000L;
            double miningManoBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("MANO", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningIfxBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("IFX", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningGinBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("GIN", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningVtlBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("VTL", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningMctBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("MCT", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningGtmBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("GTM", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningMeriBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("MERI", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningFxTCBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("FXTC", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningAlpsBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("ALPS", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);
            double miningCrsBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("CRS", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);

            calc.MyHashRate = 350000000L;
            double miningXdnaBahtPerDay = calc.GetTotalFiatMoneyMiningPerday("XDNA", PoolName.Gos, ExchangeName.CryptoBridge, FiatCurrency.THB);

            System.Diagnostics.Debug.WriteLine("Mining by gos.cx");
            System.Diagnostics.Debug.WriteLine(string.Format("MANO = {0}", miningManoBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("IFX = {0}", miningIfxBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("GIN = {0}", miningGinBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("VTL = {0}", miningVtlBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("MCT = {0}", miningMctBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("GTM = {0}", miningGtmBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("MERI = {0}", miningMeriBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("FxTC = {0}", miningFxTCBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("APLS = {0}", miningAlpsBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("CRS = {0}", miningCrsBahtPerDay));
            System.Diagnostics.Debug.WriteLine(string.Format("XDNA = {0}", miningXdnaBahtPerDay));


            Assert.AreEqual(true, miningManoBahtPerDay > 0);
        }



        [TestMethod]
        public void TestGetHashPowerInfo()
        {
            HashPowerInfo hashPower = HashPower.GetHashPowerInfo("1080ti", "lyra2z");
            Assert.AreEqual(3100000, hashPower.Power);
        }


        [TestMethod]
        public void TestGetCoinPriceMiningPerdayByGpuName()
        {
            MiningCalculator calc = new MiningCalculator();
            double hashRate = HashPower.GetHashRate("1080ti", "lyra2z", 18);
            calc.MyHashRate = hashRate;
            double miningManoBtcPerDay = calc.GetTotalBtcMiningPerday("MANO", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningIfxBtcPerDay = calc.GetTotalBtcMiningPerday("IFX", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningGinBtcPerDay = calc.GetTotalBtcMiningPerday("GIN", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningVtlBtcPerDay = calc.GetTotalBtcMiningPerday("VTL", PoolName.Bsod, ExchangeName.CryptoBridge);

            Assert.AreEqual(true, miningManoBtcPerDay > 0);
        }


        [TestMethod]
        public void TestGetMiningPriceAtCryptoBridgePerdayByMultiGpu()
        {
            BsodAPI api = new BsodAPI();
            HashPower.SetupHardware("1080ti", 18);
            HashPower.SetupHardware("1070ti", 6);
            HashPower.SetupHardware("1070", 7);

            CryptoCurrency coins = api.LoadCurrency();

            MiningCalculator calc = new MiningCalculator();

            SortedDictionary<string, double> prices = new SortedDictionary<string, double>();

            foreach (string symbol in CurrencyName.Symbols)
            {
                if (coins[symbol] != null)
                {
                    double bahtPerDay = GetMiningBahtPerDay(symbol, coins[symbol].algo, PoolName.Bsod, ExchangeName.CryptoBridge, calc);
                    prices.Add(symbol + " (" + coins[symbol].algo + ")", bahtPerDay);
                }
            }

            foreach (KeyValuePair<string, double> item in prices)
            {
                Debug.WriteLine(string.Format("{0} = {1} ", item.Key, item.Value));
            }

            Assert.AreEqual(0, 0);

        }


        [TestMethod]
        public void TestGetMiningPriceAtCryptoBridgePerdayByMultiGpu1060x6GB()
        {
            BsodAPI api = new BsodAPI();
            HashPower.SetupHardware("1060_6GB", 6);

            CryptoCurrency coins = api.LoadCurrency();

            MiningCalculator calc = new MiningCalculator();

            SortedDictionary<string, double> prices = new SortedDictionary<string, double>();

            foreach (string symbol in CurrencyName.Symbols)
            {
                if (coins[symbol] != null)
                {
                    double bahtPerDay = GetMiningBahtPerDay(symbol, coins[symbol].algo, PoolName.Bsod, ExchangeName.CryptoBridge, calc);
                    prices.Add(symbol + " (" + coins[symbol].algo + ")", bahtPerDay);
                }
            }

            foreach (KeyValuePair<string, double> item in prices)
            {
                Debug.WriteLine(string.Format("{0} = {1} ", item.Key, item.Value));
            }

            Assert.AreEqual(0, 0);

        }


        [TestMethod]
        public void TestGetMiningPriceAtCrex24PerdayByMultiGpu()
        {
            BsodAPI api = new BsodAPI();
            HashPower.SetupHardware("1080ti", 18);
            HashPower.SetupHardware("1070ti", 6);
            HashPower.SetupHardware("1070", 7);

            CryptoCurrency coins = api.LoadCurrency();

            MiningCalculator calc = new MiningCalculator();

            SortedDictionary<string, double> prices = new SortedDictionary<string, double>();

            foreach (string symbol in CurrencyName.Symbols)
            {
                if (coins[symbol] != null)
                {
                    double bahtPerDay = GetMiningBahtPerDay(symbol, coins[symbol].algo, PoolName.Bsod, ExchangeName.Crex24, calc);
                    prices.Add(symbol + " (" + coins[symbol].algo + ")", bahtPerDay);
                }
            }

            foreach (KeyValuePair<string, double> item in prices)
            {
                Debug.WriteLine(string.Format("{0} = {1} ", item.Key, item.Value));
            }

            Assert.AreEqual(0, 0);

        }


        private static double GetMiningBahtPerDay(string coinSymbol, string algorithm, PoolName pool, ExchangeName exchangeName, MiningCalculator calc)
        {
            double hashRate = HashPower.GetAlgorithmHashRate(algorithm);
            calc.MyHashRate = hashRate;
            System.Diagnostics.Debug.WriteLine(string.Format("My hashrate of algor {0} = {1} ", algorithm, calc.MyHashRate));
            double bahtPerDay = calc.GetTotalFiatMoneyMiningPerday(coinSymbol, pool, exchangeName, FiatCurrency.THB);
            System.Diagnostics.Debug.WriteLine(string.Format("Total baht per day at {0} of {1} ==========> {2} Baht ", exchangeName, coinSymbol, bahtPerDay));
            return bahtPerDay;
        }

        [TestMethod]
        public void TestGetAllCoinPriceMiningPerdayFromBsod()
        {
            MiningCalculator calc = new MiningCalculator();
            double hashRate = HashPower.GetHashRate("1080ti", "lyra2z", 18);
            hashRate += HashPower.GetHashRate("1070ti", "lyra2z", 6);
            hashRate += HashPower.GetHashRate("1070", "lyra2z", 7);
            calc.MyHashRate = hashRate;

            System.Diagnostics.Debug.WriteLine(calc.MyHashRate);

            double miningManoBtcPerDay = calc.GetTotalBtcMiningPerday("MANO", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningIfxBtcPerDay = calc.GetTotalBtcMiningPerday("IFX", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningGinBtcPerDay = calc.GetTotalBtcMiningPerday("GIN", PoolName.Bsod, ExchangeName.CryptoBridge);
            double miningVtlBtcPerDay = calc.GetTotalBtcMiningPerday("VTL", PoolName.Bsod, ExchangeName.CryptoBridge);

            Assert.AreEqual(true, miningManoBtcPerDay > 0);
        }

        [TestMethod]
        public void TestSerializeRig()
        {
            Rig rig = new Rig();
            rig.Chipsets.Add(new GpuInfo("1080ti", 18));
            rig.Chipsets.Add(new GpuInfo("1070ti", 6));
            rig.Chipsets.Add(new GpuInfo("1070", 7));
            string json = JsonConvert.SerializeObject(rig);
            Debug.WriteLine(json);
            System.IO.File.WriteAllText("myrig.json", json);
            Assert.AreEqual(true, json.Length > 0);
        }


        [TestMethod]
        public void TestGetNeoScryptPriceMiningPerdayFromBsod()
        {
            MiningCalculator calc = new MiningCalculator();
            double hashRate = HashPower.GetHashRate("1080ti", "lyra2z", 18);
            hashRate += HashPower.GetHashRate("1070ti", "lyra2z", 6);
            hashRate += HashPower.GetHashRate("1070", "lyra2z", 7);
            calc.MyHashRate = hashRate;

            System.Diagnostics.Debug.WriteLine(calc.MyHashRate);

            double miningGbxBtcPerDay = calc.GetTotalFiatMoneyMiningPerday("GBX", PoolName.Bsod, ExchangeName.CryptoBridge, FiatCurrency.THB);
      //      double miningLincBtcPerDay = calc.GetTotalBtcMiningPerday("LINC", PoolName.Bsod, ExchangeName.Crex24);
      //      double miningUfoBtcPerDay = calc.GetTotalBtcMiningPerday("UFO", PoolName.Bsod, ExchangeName.CryptoBridge);

            Assert.AreEqual(true, miningGbxBtcPerDay > 0);
        }

        [TestMethod]
        public void TestGetTotalBahtAllPoolAllExchange()
        {
            double keepMoreThan = 0;
            BsodAPI bsod = new BsodAPI();
            GosAPI gos = new GosAPI();

            string json = System.IO.File.ReadAllText("myrig.json");
            Rig myRig = JsonConvert.DeserializeObject<Rig>(json);

            HashPower.SetupHardware(myRig);

            Console.WriteLine("Start loading pool data. Please wait ...");
            MiningCalculator calc = new MiningCalculator();
            CryptoCurrency bsodCoins = calc.PoolCoins[0];
            CryptoCurrency gosCoins = calc.PoolCoins[1];

            Console.WriteLine("Analyzing ...");

            SortedDictionary<string, double> prices = new SortedDictionary<string, double>();
            StringBuilder result = new StringBuilder();
            foreach (string symbol in CurrencyName.Symbols)
            {
                if (bsodCoins != null && bsodCoins[symbol] != null)
                {
                    double bahtPerDay = GetMiningBahtPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.CryptoBridge, calc);
                    if (bahtPerDay > keepMoreThan)
                    {
                        prices.Add(symbol + " (" + bsodCoins[symbol].algo + ") mining@bsod sale@crypto-bridge ", bahtPerDay);
                        result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "crypto-bridge", bahtPerDay));
                    }

                    bahtPerDay = GetMiningBahtPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Crex24, calc);
                    if (bahtPerDay > keepMoreThan)
                    {
                        prices.Add(symbol + " (" + bsodCoins[symbol].algo + ") mining@bsod sale@crex24", bahtPerDay);
                        result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "crex24", bahtPerDay));
                    }
                }

                if (gosCoins != null && gosCoins[symbol] != null)
                {

                    double bahtPerDay = GetMiningBahtPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.CryptoBridge, calc);
                    if (bahtPerDay > keepMoreThan)
                    {
                        prices.Add(symbol + " (" + gosCoins[symbol].algo + ") mining@gos sale@crypto-bridge", bahtPerDay);
                        result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "crypto-bridge", bahtPerDay));
                    }

                    bahtPerDay = GetMiningBahtPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Crex24, calc);
                    if (bahtPerDay > keepMoreThan)
                    {
                        prices.Add(symbol + " (" + gosCoins[symbol].algo + ") mining@gos sale@crex24", bahtPerDay);
                        result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "crex24", bahtPerDay));
                    }
                }
            }

            Console.WriteLine("1080ti x 18 ");
            Console.WriteLine("1070ti x 6");
            Console.WriteLine("1070 x 7 ");


            foreach (KeyValuePair<string, double> item in prices)
            {
                if (item.Value > 0)
                {
                    string line = string.Format("{0} = {1} ", item.Key, item.Value);
                    Console.WriteLine(line);
                }
            }


            Assert.AreEqual(0, 0);

        }



        [TestMethod]
        public void TestGetAutoBtcMiningPerDayFromZergPool()
        {
            string json = System.IO.File.ReadAllText("myrig.json");
            Rig myRig = JsonConvert.DeserializeObject<Rig>(json);

            HashPower.SetupHardware(myRig);

            MiningCalculator calc = new MiningCalculator();

            foreach (string algorithmName in AlgoritmName.Symbols)
            {
                calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                double btcCurrentPerDay = calc.GetTotalBtcMiningPerday(algorithmName, PoolName.Zerg, true);
                double btc24HoursPerDay = calc.GetTotalBtcMiningPerday(algorithmName, PoolName.Zerg, false);
                Debug.WriteLine(string.Format("{0} estimate_current: {1} estimate_24hour: {2}", algorithmName, btcCurrentPerDay.ToString("N8"), btc24HoursPerDay.ToString("N8")));
                Assert.AreEqual(true, btcCurrentPerDay > -1);
            }
        }



        [TestMethod]
        public void TestGetTotalBahtAutoBtcMiningPerDayFromZergPool()
        {
            string json = System.IO.File.ReadAllText("myrig.json");
            Rig myRig = JsonConvert.DeserializeObject<Rig>(json);

            HashPower.SetupHardware(myRig);

            MiningCalculator calc = new MiningCalculator();

            foreach (string algorithmName in AlgoritmName.Symbols)
            {
                calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                double btcCurrentPerDay = calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zerg, true, FiatCurrency.THB);
                double btc24HoursPerDay = calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zerg, false, FiatCurrency.THB);
                Debug.WriteLine(string.Format("{0} estimate_current: {1} baht estimate_24hour: {2} baht ", algorithmName, btcCurrentPerDay.ToString("N2"), btc24HoursPerDay.ToString("N2")));
                Assert.AreEqual(true, btcCurrentPerDay > -1);
            }
        }

    }
}
