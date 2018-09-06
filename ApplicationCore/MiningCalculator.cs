using CryptoMining.ApplicationCore.Pool;
using CryptoMining.ApplicationCore.Exchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CryptoMining.ApplicationCore
{
    public class MiningCalculator
    {
        private CryptoBridgeAPI _cbAPI = new CryptoBridgeAPI();
        private Crex24API _crexAPI = new Crex24API();
        private BsodAPI _bsodAPI = new BsodAPI();
        private GosAPI _gosAPI = new GosAPI();
        private BxAPI _bxAPI = new BxAPI();
        private List<CryptoBridgeCurrency> _cryptoBridgeCoinsPrice;
        private List<Crex24Currency> _crex24CoinsPrice;
        private CryptoCurrency _bsodCurrencies;
        private CryptoCurrency _gosCurrencies;
        private double _thaiBahtPerBTC = 0;

        public MiningCalculator()
        {
            MyHashRate = -1;
            _cryptoBridgeCoinsPrice = _cbAPI.LoadPrice();
            _crex24CoinsPrice = _crexAPI.LoadPrice();
            _bsodCurrencies = _bsodAPI.LoadCurrency();
            _gosCurrencies = _gosAPI.LoadCurrency();
            _thaiBahtPerBTC = double.Parse(_bxAPI.LoadThaiBahtBtcPrice().bids[0][0]);
        }

        public double MyHashRate { get; set; }

        public List<CryptoCurrency> PoolCoins
        {
            get
            {
                return new List<CryptoCurrency> { _bsodCurrencies, _gosCurrencies };
            }
        }

        /// <summary>
        /// Get bid price from exchange.
        /// </summary>
        /// <param name="pairSymbol"></param>
        /// <param name="exchangeName"></param>
        /// <returns></returns>
        private double GetBidPrice(string pairSymbol, ExchangeName exchangeName)
        {
            if (exchangeName == ExchangeName.CryptoBridge)
            {
                foreach (ExchangeCurrency coin in _cryptoBridgeCoinsPrice)
                {
                    if (coin.symbol == pairSymbol)
                    {
                        return GetBidPrice(coin);
                    }
                }
                Debug.WriteLine(string.Format("Can't find price of {0} on {1} exchange.", pairSymbol, exchangeName));
                return 0;
            }
            else
            {
                foreach (ExchangeCurrency coin in _crex24CoinsPrice)
                {
                    if (coin.symbol == pairSymbol)
                    {
                        return GetBidPrice(coin);
                    }
                }
                Debug.WriteLine(string.Format("Can't find price of {0} on {1} exchange.", pairSymbol, exchangeName));
                return 0;
            }
        }

        private static double GetBidPrice(ExchangeCurrency coin)
        {
            return coin.bid ?? 0;
        }

        public void RefreshPrice()
        {
            _cryptoBridgeCoinsPrice = _cbAPI.LoadPrice();
            _crex24CoinsPrice = _crexAPI.LoadPrice();
            _thaiBahtPerBTC = double.Parse(_bxAPI.LoadThaiBahtBtcPrice().bids[0][0]);
        }

        public void RefreshPool()
        {
            _bsodCurrencies = _bsodAPI.LoadCurrency();
            _gosCurrencies = _gosAPI.LoadCurrency();
        }

        public double GetNumOfCoinMiningPerDay(string coinSymbol, PoolName poolName)
        {
            if (MyHashRate == -1)
                throw new Exception("Invaid MyHashRate property. Please specify MyHashRate property before calling GetNumOfCoinMiningPerDay method.");
            CurrencyBase coin = null;
            if (poolName == PoolName.Bsod)
            {
                coin = _bsodCurrencies[coinSymbol];
            }
            else if (poolName == PoolName.Gos)
            {
                coin = _gosCurrencies[coinSymbol];
            }

            if (coin != null)
            {
                long poolHashRate = coin.hashrate ?? 0;
                if (poolHashRate == 0)
                {
                    Debug.WriteLine(string.Format("Can not calculate num of coin per day because nobody mining {0} coin.", coinSymbol));
                    return 0;
                }
                if (coin.hashRateDiscountPercent > 0)  // กรณีเหรียญที่แรงแกว่งมากๆ ให้ discount จำนวนที่ขุดได้ลง
                    poolHashRate = poolHashRate + (poolHashRate * coin.hashRateDiscountPercent / 100);
                double rewardPerBlock = PoolReward.GetPoolOverrideReward(poolName, coinSymbol);
                if (rewardPerBlock == -1)
                    rewardPerBlock = double.Parse(coin.reward);
                int blockAllDay = coin.h24_blocks;
                double receiveCoinsPerDay = 0;
                if (MyHashRate > poolHashRate) // test กรณี แรงเรามากกว่าแรง pool
                {
                    receiveCoinsPerDay = (rewardPerBlock * blockAllDay) * (1 - (poolHashRate / MyHashRate));
                }
                else
                {
                    // receiveCoinsPerDay = (rewardPerBlock / poolHashRate) * MyHashRate * blockAllDay;
                    receiveCoinsPerDay = (MyHashRate / (double)poolHashRate) * (blockAllDay * rewardPerBlock);
                }
                return receiveCoinsPerDay;
            }
            else
            {
                Debug.WriteLine(string.Format("Can not load {0} information from {1} pool.", coinSymbol, poolName));
                return 0;
            }
        }

        /// <summary>
        /// Get a coin price in btc per day if we mining it.
        /// </summary>
        /// <param name="coinSymbol"></param>
        /// <param name="exchangeName"></param>
        /// <returns>btc</returns>
        public double GetTotalBtcMiningPerday(string coinSymbol, PoolName poolName, ExchangeName exchangeName)
        {
            double coinsPerDay = GetNumOfCoinMiningPerDay(coinSymbol, poolName);
            Console.WriteLine(string.Format("{0} mining per day {1} coins at pool {2}", coinSymbol, coinsPerDay, poolName));
            string pairSymbol = coinSymbol + "-BTC";
            double bidPrice = GetBidPrice(pairSymbol, exchangeName);
            return coinsPerDay * bidPrice;
        }


        /// <summary>
        /// Get a coin price in thai baht per day if we mining it.
        /// </summary>
        /// <param name="coinSymbol"></param>
        /// <param name="poolName"></param>
        /// <param name="exchangeName"></param>
        /// <returns></returns>
        public double GetTotalBahtMiningPerday(string coinSymbol, PoolName poolName, ExchangeName exchangeName)
        {
            double totalBtcPerDay = GetTotalBtcMiningPerday(coinSymbol, poolName, exchangeName);
            return totalBtcPerDay * _thaiBahtPerBTC;
        }


    }
}
