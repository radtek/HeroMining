using CryptoMining.ApplicationCore.Log;
using CryptoMining.ApplicationCore.Pool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CryptoMining.ApplicationCore.Miner
{
    public static class MinerControl
    {
        private static Process _runningMinerProcess;

        private static bool IsInConfigured(string bestName, List<KeyValuePair<string, string>> paths)
        {
            foreach (KeyValuePair<string, string> path in paths)
            {
                if (path.Key.ToLower() == bestName)
                    return true;
            }
            return false;
        }


        private static bool IsExclude(string name, List<string> exclude)
        {
            foreach (string item in exclude)
            {
                if (0 == String.Compare(name, item, true))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Find the best price and return algor@pool name with best price
        /// </summary>
        /// <param name="algors">List of algorithm</param>
        /// <param name="seeCurrent">Field to compare is estimate_current if specify true, otherwise use field estimate_last24h to compare.</param>
        /// <returns>algor@pool with best price</returns>
        public static (string, double) FindBestPrice(List<AlgorithmResult> algors, bool seeCurrent, MinerModel miners)
        {
            double bestPrice = 0;
            string bestName = "";
            Dictionary<string, double> priceList = new Dictionary<string, double>();
            foreach (AlgorithmResult algor in algors)
            {
                string algorAtPool = algor.name + "@" + algor.Pool.ToString();
                if (seeCurrent)
                {
                    if (algor.estimate_current > bestPrice && !IsExclude(algorAtPool, miners.Exclude))
                    {
                        bestPrice = algor.estimate_current;
                        bestName = algorAtPool;
                    }
                    foreach (KeyValuePair<string, string> path in miners.Path)
                    {
                        if (algorAtPool.ToLower() == path.Key.ToLower() && !priceList.ContainsKey(algorAtPool))
                        {
                            priceList.Add(algorAtPool, algor.estimate_current);
                            break;
                        }
                    }
                }
                else
                {
                    if (algor.estimate_last24h > bestPrice && !IsExclude(algorAtPool, miners.Exclude))
                    {
                        bestPrice = algor.estimate_last24h;
                        bestName = algorAtPool;
                    }
                    foreach (KeyValuePair<string, string> path in miners.Path)
                    {
                        if (algorAtPool.ToLower() == path.Key.ToLower() && !priceList.ContainsKey(algorAtPool))
                        {
                            priceList.Add(algorAtPool, algor.estimate_last24h);
                            break;
                        }
                    }
                }
            }

            if (!IsInConfigured(bestName, miners.Path))
            {
                string bestNameInUserConfig = "";
                double bestPriceInUserConfig = 0;
                foreach (KeyValuePair<string, double> path in priceList)
                {
                    if (path.Value > bestPriceInUserConfig)
                    {
                        bestNameInUserConfig = path.Key;
                        bestPriceInUserConfig = path.Value;
                    }
                }
                bestName = bestNameInUserConfig;
                bestPrice = bestPriceInUserConfig;
            }
            return (bestName, bestPrice);
        }

        /// <summary>
        ///  Find the best price and return coin@pool name with best price
        /// </summary>
        /// <param name="coins">List of coin</param>
        /// <returns>coin@pool with best price</returns>
        public static (string, double) FindBestPrice(List<CryptoCurrencyResult> coins, MinerModel miners)
        {
            double bestPrice = 0;
            string bestName = "";
            Dictionary<string, double> priceList = new Dictionary<string, double>();
            foreach (CryptoCurrencyResult coin in coins)
            {
                string coinAtPool = coin.symbol + "@" + coin.Pool.ToString().ToLower();

                if (coin.h24_btc > bestPrice && !IsExclude(coinAtPool, miners.Exclude))
                {
                    bestPrice = coin.h24_btc;
                    bestName = coinAtPool;
                }
                foreach (KeyValuePair<string, string> path in miners.Path)
                {
                    if (coinAtPool.ToLower() == path.Key.ToLower() && !priceList.ContainsKey(coinAtPool))
                    {
                        priceList.Add(coinAtPool, coin.h24_btc);
                        break;
                    }
                }
            }

            if (!IsInConfigured(bestName, miners.Path))
            {
                string bestNameInUserConfig = "";
                double bestPriceInUserConfig = 0;
                foreach (KeyValuePair<string, double> path in priceList)
                {
                    if (path.Value > bestPriceInUserConfig)
                    {
                        bestNameInUserConfig = path.Key;
                        bestPriceInUserConfig = path.Value;
                    }
                }
                bestName = bestNameInUserConfig;
                bestPrice = bestPriceInUserConfig;
            }

            return (bestName, bestPrice);
        }


        /// <summary>
        /// Do mining
        /// </summary>
        /// <param name="key">coin@pool or algor@pool.</param>
        /// <param name="paths">List of path.</param>
        /// <returns>true if found miner config, otherwise not found config miner.</returns>
        public static bool DoMining(string key, List<KeyValuePair<string, string>> paths)
        {
            foreach (KeyValuePair<string, string> item in paths)
            {
                if (0 == String.Compare(key, item.Key, true))
                {
                    CloseExistsMiner();
                    RunMiner(item.Value);
                    return true;
                }
            }
            return false;
        }


        private static void RunMiner(string path)
        {
            try
            {
                var startInfo = new ProcessStartInfo();
                startInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = true;
                //        startInfo.RedirectStandardOutput = false;
                startInfo.FileName = path;
                _runningMinerProcess = System.Diagnostics.Process.Start(startInfo);
                string message = DateTime.Now.ToString("yyyy-MM-dd HH':'mm") + " start " + path;
                EasyLog.Log("minerlog.txt", message);
            }
            catch (Exception err)
            {
                throw new Exception("Please verify the configuration file [miner.json] or your .bat file  has invalid config.", err);
            }

        }

        private static void CloseExistsMiner()
        {
            if (_runningMinerProcess != null)
            {
                KillProcessAndChildrens(_runningMinerProcess.Id);
                string message = DateTime.Now.ToString("yyyy-MM-dd HH':'mm") + " stop " + _runningMinerProcess.StartInfo.FileName;
                EasyLog.Log("minerlog.txt", message);
                _runningMinerProcess = null;
            }
        }


        public static void KillProcessAndChildrens(int processId)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("taskkill", "/F /T /PID " + processId)
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                Process.Start(processStartInfo);
            }
            catch(Exception err)
            {
                throw err;
            }

        }

    }
}
