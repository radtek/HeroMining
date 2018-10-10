using CryptoMining.ApplicationCore.Pool;
using CryptoMining.ApplicationCore.Exchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;

namespace CryptoMining.ApplicationCore
{
    public class MiningCalculator
    {
        private BinanceAPI _bnAPI = new BinanceAPI();
        private CryptoBridgeAPI _cbAPI = new CryptoBridgeAPI();
        private CoinExchangeAPI _ceAPI = new CoinExchangeAPI();
        private CryptopiaAPI _cpAPI = new CryptopiaAPI();
        private Crex24API _crexAPI = new Crex24API();
        private BsodAPI _bsodAPI = new BsodAPI();
        private GosAPI _gosAPI = new GosAPI();
        private ZergAPI _zergAPI = new ZergAPI();
        private PhiPhiAPI _phiAPI = new PhiPhiAPI();
        private AhashPoolAPI _ahashAPI = new AhashPoolAPI();
        private ZpoolAPI _zpoolAPI = new ZpoolAPI();
        private BxAPI _bxAPI = new BxAPI();
        private List<CryptoBridgeCurrency> _cryptoBridgeCoinsPrice = new List<CryptoBridgeCurrency>();
        private List<Crex24Currency> _crex24CoinsPrice = new List<Crex24Currency>();
        private List<CryptopiaCurrency> _cryptopiaCoinsPrice = new List<CryptopiaCurrency>();
        private List<BinanceCurrency> _binanceCoinsPrice = new List<BinanceCurrency>();
        private List<CoinExchangeCurrency> _coinExchangeCoinsPrice = new List<CoinExchangeCurrency>();
        private CryptoCurrency _bsodCurrencies = new CryptoCurrency();
        private CryptoCurrency _gosCurrencies = new CryptoCurrency();
        private Algorithm _zergAlgorithm = new Algorithm();
        private Algorithm _phiAlgorithm = new Algorithm();
        private Algorithm _zpoolAlgorithm = new Algorithm();
        private Algorithm _ahashAlgorithm = new Algorithm();


        private double _thaiBahtPerBTC = 0;
        private double _usdPerBTC = 0;

        private void LoadCryptopiaPrice()
        {
            try
            {
                _cryptopiaCoinsPrice = _cpAPI.LoadPrice();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadBinancePrice()
        {
            try
            {
                _binanceCoinsPrice = _bnAPI.LoadPrice();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadCryptoBridgePrice()
        {
            try
            {
                _cryptoBridgeCoinsPrice = _cbAPI.LoadPrice();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadCoinExchangePrice()
        {
            try
            {
                _coinExchangeCoinsPrice = _ceAPI.LoadPrice();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }


        private void LoadCrex2Price()
        {
            try
            {
                _crex24CoinsPrice = _crexAPI.LoadPrice();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadBsodCurrencies()
        {
            try
            {
                _bsodCurrencies = _bsodAPI.LoadCurrency();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadGosCurrencies()
        {
            try
            {
                _gosCurrencies = _gosAPI.LoadCurrency();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadZergAlgorithm()
        {
            try
            {
                _zergAlgorithm = _zergAPI.LoadAlgorithm();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadZpoolAlgorithm()
        {
            try
            {
                _zpoolAlgorithm = _zpoolAPI.LoadAlgorithm();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadAhashAlgorithm()
        {
            try
            {
                _ahashAlgorithm = _ahashAPI.LoadAlgorithm();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        private void LoadPhiPhiAlgorithm()
        {
            try
            {
                _phiAlgorithm = _phiAPI.LoadAlgorithm();
            }
            catch (Exception err)
            {
                Debug.WriteLine("Warning: " + err.Message);
            }
        }

        public MiningCalculator()
        {
            MyHashRate = -1;
            Parallel.Invoke(
                () => LoadCoinExchangePrice(),
                () => LoadCryptopiaPrice(),
                () => LoadCryptoBridgePrice(),
                () => LoadBinancePrice(),
                () => LoadCrex2Price(),
                () => LoadBsodCurrencies(),
                () => LoadGosCurrencies(),
                () => LoadZergAlgorithm(),
                () => LoadPhiPhiAlgorithm(),
                () => LoadZpoolAlgorithm(),
                () => LoadAhashAlgorithm());
                
            _thaiBahtPerBTC = double.Parse(_bxAPI.LoadThaiBahtBtcPrice().bids[0][0]);
            _usdPerBTC = _bnAPI.GetUsdPerBTCPrice();
        }

        public double MyHashRate { get; set; }

        public List<CryptoCurrency> PoolCoins
        {
            get
            {
                return new List<CryptoCurrency> { _bsodCurrencies, _gosCurrencies };
            }
        }


        public List<Algorithm> PoolAlgorithms
        {
            get
            {
                return new List<Algorithm> { _zergAlgorithm, _phiAlgorithm, _zpoolAlgorithm, _ahashAlgorithm };
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
            else if (exchangeName == ExchangeName.Cryptopia)
            {
                foreach (ExchangeCurrency coin in _cryptopiaCoinsPrice)
                {
                    if (coin.symbol == pairSymbol)
                    {
                        return GetBidPrice(coin);
                    }
                }
                Debug.WriteLine(string.Format("Can't find price of {0} on {1} exchange.", pairSymbol, exchangeName));
                return 0;
            }
            else if (exchangeName == ExchangeName.Binance)
            {
                foreach (ExchangeCurrency coin in _binanceCoinsPrice)
                {
                    if (coin.symbol == pairSymbol)
                    {
                        return GetBidPrice(coin);
                    }
                }
                Debug.WriteLine(string.Format("Can't find price of {0} on {1} exchange.", pairSymbol, exchangeName));
                return 0;
            }
            else if (exchangeName == ExchangeName.CoinExchange)
            {
                foreach (ExchangeCurrency coin in _coinExchangeCoinsPrice)
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
            Parallel.Invoke(
               () => LoadCryptoBridgePrice(),
               () => LoadCryptopiaPrice(),
               () => LoadCrex2Price());
            _thaiBahtPerBTC = double.Parse(_bxAPI.LoadThaiBahtBtcPrice().bids[0][0]);
            _usdPerBTC = _bnAPI.GetUsdPerBTCPrice();
        }

        public void RefreshPool()
        {
            Parallel.Invoke(
                () => LoadBsodCurrencies(),
                () => LoadGosCurrencies(),
                () => LoadAhashAlgorithm(),
                () => LoadZergAlgorithm(),
                () => LoadZpoolAlgorithm());
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
            else if (poolName == PoolName.Zerg || poolName == PoolName.PhiPhi)
            {
                throw new ArgumentException("Invalid argument zerg pool not implement.", "poolName");
            }

            if (coin != null)
            {
                long poolHashRate = coin.hashrate ?? 0;
                if (coin.hashrate_shared != null && coin.hashrate_shared != 0)
                {
                    poolHashRate = coin.hashrate_shared ?? 0;
                }
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
                if (coin.h24_blocks_shared != 0)
                {
                    blockAllDay = coin.h24_blocks_shared;
                }
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
            Debug.WriteLine(string.Format("{0} mining per day {1} {0} at pool {2}", coinSymbol, coinsPerDay, poolName));
            string pairSymbol = coinSymbol + "-BTC";
            double bidPrice = GetBidPrice(pairSymbol, exchangeName);
            return coinsPerDay * bidPrice;
        }


        /// <summary>
        /// Get total btc per day if we mining at auto btc pool.
        /// </summary>
        /// <param name="algorithmName">algorithm name ex. lyra2z</param>
        /// <param name="poolName">PoolName enum</param>
        /// <param name="current">true if current estimmate, false if 24 hours estimate</param>
        /// <returns>btc</returns>
        public double GetTotalBtcMiningPerday(string algorithmName, PoolName poolName, bool estimateCurrent)
        {
            double btcPerDay = 0;
            if (MyHashRate == -1)
                throw new Exception("Invaid MyHashRate property. Please specify MyHashRate property before calling GetTotalBtcMiningPerday method.");

            AlgorithmBase algor = null;
            if (poolName == PoolName.Zerg)
            {
                algor = _zergAlgorithm[algorithmName];
            }
            else if (poolName == PoolName.PhiPhi)
            {
                algor = _phiAlgorithm[algorithmName];
            }
            else if (poolName == PoolName.AhashPool)
            {
                algor = _ahashAlgorithm[algorithmName];
            }
            else if (poolName == PoolName.Zpool)
            {
                algor = _zpoolAlgorithm[algorithmName];
            }
            if (algor != null)
            {
                if (estimateCurrent)
                    btcPerDay = GetBtcPerDay(algor.estimate_current, algor.mbtc_mh_factor, false);
                else
                    btcPerDay = GetBtcPerDay(algor.actual_last24h, algor.mbtc_mh_factor, true);
            }
            else
            {
                Debug.WriteLine(string.Format("WARNING: Not found algorithm {0} at {1} pool.", algorithmName, poolName));

            }
            return btcPerDay;
        }

        private double GetBtcPerDay(double btc, double factor, bool estimate24Hours)
        {
            double result = btc * (MyHashRate / 1000000D);
            if (factor > 1)
                result = result / factor;
            if (estimate24Hours)
                result = result * 0.001;
            return result;
        }


        /// <summary>
        /// Get a coin price in thai baht per day if we mining it.
        /// </summary>
        /// <param name="coinSymbol"></param>
        /// <param name="poolName"></param>
        /// <param name="exchangeName"></param>
        /// <returns></returns>
        public double GetTotalFiatMoneyMiningPerday(string coinSymbol, PoolName poolName, ExchangeName exchangeName, FiatCurrency fiat)
        {
            double totalBtcPerDay = GetTotalBtcMiningPerday(coinSymbol, poolName, exchangeName);
            double fiatPerBTC = (fiat == FiatCurrency.Baht) ? _thaiBahtPerBTC : _usdPerBTC;
            return totalBtcPerDay * fiatPerBTC;
        }




        /// <summary>
        /// Get algorithm price in thai baht per day if we mining in auto btc pool.
        /// </summary>
        /// <param name="algorithmName">algorithm name ex. lyra2z</param>
        /// <param name="poolName">PoolName enum</param>
        /// <returns></returns>
        public double GetTotalFiatMoneyMiningPerday(string algorithmName, PoolName poolName, bool estimateCurrent, FiatCurrency fiat)
        {
            double totalBtcPerDay = GetTotalBtcMiningPerday(algorithmName, poolName, estimateCurrent);
            double fiatPerBTC = (fiat == FiatCurrency.Baht) ? _thaiBahtPerBTC : _usdPerBTC; 
            return totalBtcPerDay * fiatPerBTC;
        }


    }
}
