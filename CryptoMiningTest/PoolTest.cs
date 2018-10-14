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
    public class PoolTest
    {
        [TestMethod]
        public void TestDeserializeBsod()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // HTTP POST
                client.BaseAddress = new Uri("http://api.bsod.pw");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("/api/currencies").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                    res = res.Replace("24h_blocks", "h24_blocks").Replace("24h_btc", "h24_btc");
                    CryptoCurrency bsod = JsonConvert.DeserializeObject<CryptoCurrency>(res);
                    CryptoCurrency.AEX aex = bsod.Aex;
                    Assert.AreEqual(true, aex.h24_blocks > 0);

                }
            }
        }

        [TestMethod]
        public void TestLoadCurrencyFromBsodPool()
        {
            BsodAPI api = new BsodAPI();
            CryptoCurrency currencies = api.LoadCurrency();
            Assert.AreEqual(true, currencies.Aex != null);
        }

        [TestMethod]
        public void TestLoadCurrencyFromGosPool()
        {
            GosAPI api = new GosAPI();
            CryptoCurrency currencies = api.LoadCurrency();
            Assert.AreEqual(true, currencies.Mano != null);
        }

        [TestMethod]
        public void TestLoadCurrencyFromZergPool()
        {
            ZergAPI api = new ZergAPI();
            CryptoCurrency currencies = api.LoadCurrency();
            Assert.AreEqual(true, currencies.Mano != null);
        }

        [TestMethod]
        public void TestLoadAlgorithmFromZergPool()
        {
            ZergAPI api = new ZergAPI();
            Algorithm algor = api.LoadAlgorithm();
            Assert.AreEqual(true, algor.lyra2z != null);
        }

        [TestMethod]
        public void TestGetBtcMiningPerDayFromZergPool()
        {
            long myHashRate = 79000000L;
            MiningCalculator calc = new MiningCalculator();
            calc.MyHashRate = myHashRate;
            double btcPerDay = calc.GetTotalBtcMiningPerday("lyra2z", PoolName.Zerg, true);
            Assert.AreEqual(true, btcPerDay > 0);
        }


        [TestMethod]
        public void TestLoadAlgorithmFromPhiPhiPool()
        {
            PhiPhiAPI api = new PhiPhiAPI();
            Algorithm algor = api.LoadAlgorithm();
            Assert.AreEqual(true, algor.lyra2z != null);
        }


        [TestMethod]
        public void TestLoadAlgorithmFromZpool()
        {
            ZpoolAPI api = new ZpoolAPI();
            Algorithm algor = api.LoadAlgorithm();
            Assert.AreEqual(true, algor.lyra2z != null);
        }


        [TestMethod]
        public void TestLoadAlgorithmFromAhashPool()
        {
            AhashPoolAPI api = new AhashPoolAPI();
            Algorithm algor = api.LoadAlgorithm();
            Assert.AreEqual(true, algor.lyra2z != null);
        }



        [TestMethod]
        public void TestGetMiningManoCoinFromBsodPerday()
        {
            long myHashRate = 120000000L;
            BsodAPI api = new BsodAPI();
            CryptoCurrency currencies = api.LoadCurrency();
            CryptoCurrency.MANO manoCoin = currencies.Mano;
            double rewardPerBlock = double.Parse(manoCoin.reward);
            int blockAllDay = manoCoin.h24_blocks_shared;
            long poolHashRate = manoCoin.hashrate ?? 0;
            if (manoCoin.hashrate_shared != null && manoCoin.hashrate_shared != 0)
            {
                poolHashRate = manoCoin.hashrate_shared ?? 0;
            }
            double receiveCoinPerDay = (rewardPerBlock / (double)poolHashRate) * myHashRate * blockAllDay;
            Assert.AreEqual(true, receiveCoinPerDay > 0);
        }


        [TestMethod]
        public void TestGetMiningManoCoinFromGosPerday()
        {
            long myHashRate = 120000000L;
            GosAPI api = new GosAPI();
            CryptoCurrency currencies = api.LoadCurrency();
            CryptoCurrency.MANO manoCoin = currencies.Mano;
            double rewardPerBlock = double.Parse(manoCoin.reward);
            int blockAllDay = manoCoin.h24_blocks;
            long poolHashRate = manoCoin.hashrate ?? 0;
            if (manoCoin.hashrate_shared != null && manoCoin.hashrate_shared != 0)
            {
                poolHashRate = manoCoin.hashrate_shared ?? 0;
            }
            double receiveCoinPerDay = (rewardPerBlock / (double)poolHashRate) * myHashRate * blockAllDay;
            Assert.AreEqual(true, receiveCoinPerDay > 0);
        }


        [TestMethod]
        public void TestGetMiningGinCoinFromZergPerday()
        {
            long myHashRate = 120000000L;
            ZergAPI api = new ZergAPI();
            CryptoCurrency currencies = api.LoadCurrency();
            CryptoCurrency.GIN ginCoin = currencies.Gin;
            double rewardPerBlock = double.Parse(ginCoin.reward);
            int blockAllDay = ginCoin.h24_blocks;
            long poolHashRate = ginCoin.hashrate ?? 0;
            if (ginCoin.hashrate_shared != null && ginCoin.hashrate_shared != 0)
            {
                poolHashRate = ginCoin.hashrate_shared ?? 0;
            }
            double receiveCoinPerDay = (rewardPerBlock / (double)poolHashRate) * myHashRate * blockAllDay;
            Assert.AreEqual(true, receiveCoinPerDay > 0);
        }

        [TestMethod]
        public void TestCompareHashRateFromZergPool()
        {
            ZergAPI api = new ZergAPI();
            CryptoCurrency currencies = api.LoadCurrency();
            CryptoCurrency.GIN ginCoin = currencies.Gin;
            CryptoCurrency.MANO manoCoin = currencies.Mano;
            Assert.AreNotEqual(manoCoin.hashrate, ginCoin.hashrate);
        }

        [TestMethod]
        public void TestGetMiningGinCoinFromZergPerday10Rounds()
        {
            long myHashRate = 120000000L;
            ZergAPI api = new ZergAPI();

            for (int i = 0; i < 10; i++)
            {
                CryptoCurrency currencies = api.LoadCurrency();
                CryptoCurrency.GIN ginCoin = currencies.Gin;
                double rewardPerBlock = double.Parse(ginCoin.reward);
                int blockAllDay = ginCoin.h24_blocks;
                long poolHashRate = ginCoin.hashrate ?? 0;
                if (ginCoin.hashrate_shared != null && ginCoin.hashrate_shared != 0)
                {
                    poolHashRate = ginCoin.hashrate_shared ?? 0;
                }
                double receiveCoinPerDay = (rewardPerBlock / (double)poolHashRate) * myHashRate * blockAllDay;
                System.Diagnostics.Debug.WriteLine(string.Format("round {0} GIN pool hashrate: {1} \t coin per day: {2}", i, poolHashRate, receiveCoinPerDay));
                System.Threading.Thread.Sleep(5000);
            }
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestGetMiningZcoinCoinFromBsodPerday10Rounds()
        {
            long myHashRate = 120000000L;
            BsodAPI api = new BsodAPI();
            double receiveCoinsPerDay = 0;

            CryptoCurrency currencies = api.LoadCurrency();
            CryptoCurrency.XZC coin = currencies.Xzc;
            if (coin != null)
            {
                int blockAllDay = coin.h24_blocks;
                long poolHashRate = coin.hashrate ?? 0;
                if (coin.h24_blocks_shared != -1 )
                {
                    blockAllDay = coin.h24_blocks_shared;
                }
                if (coin.hashrate_shared != null && coin.hashrate_shared != 0)
                {
                    poolHashRate = coin.hashrate_shared ?? 0;
                }
                if (poolHashRate == 0)
                {
                    Debug.WriteLine(string.Format("Can not calculate num of coin per day because nobody mining {0} coin.", coin.symbol));
                }
                if (coin.hashRateDiscountPercent > 0)  // กรณีเหรียญที่แรงแกว่งมากๆ ให้ discount จำนวนที่ขุดได้ลง
                    poolHashRate = poolHashRate + (poolHashRate * coin.hashRateDiscountPercent / 100);
                double rewardPerBlock = PoolReward.GetPoolOverrideReward(PoolName.Bsod, coin.symbol);
                if (rewardPerBlock == -1)
                {
                    rewardPerBlock = double.Parse(coin.reward);
                }

                if (myHashRate > poolHashRate) // test กรณี แรงเรามากกว่าแรง pool
                {
                    receiveCoinsPerDay = (rewardPerBlock * blockAllDay) * (1 - (poolHashRate / myHashRate));
                }
                else
                {
                    // receiveCoinsPerDay = (rewardPerBlock / poolHashRate) * MyHashRate * blockAllDay;
                    receiveCoinsPerDay = (myHashRate / (double)poolHashRate) * (blockAllDay * rewardPerBlock);
                }
            }

            Assert.AreEqual(true, receiveCoinsPerDay > -1);
        }


    }
}
