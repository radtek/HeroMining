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

        public static string FindBestPrice(List<AlgorithmResult> algors, bool seeCurrent)
        {
            double bestPrice = 0;
            string bestName = "";
            foreach (AlgorithmResult item in algors)
            {
                if (seeCurrent)
                {
                    if (item.estimate_current > bestPrice)
                    {
                        bestPrice = item.estimate_current;
                        bestName = item.name + "@" + item.Pool.ToString();
                    }
                }
                else
                {
                    if (item.estimate_last24h > bestPrice)
                    {
                        bestPrice = item.estimate_last24h;
                        bestName = item.name + "@" + item.Pool.ToString();
                    }
                }
            }
            return bestName;
        }

        public static string FindBestPrice(List<CryptoCurrencyResult> coins)
        {
            double bestPrice = 0;
            string bestName = "";
            foreach (CryptoCurrencyResult item in coins)
            {
                if (item.h24_btc > bestPrice)
                {
                    bestPrice = item.h24_btc;
                    bestName = item.name + "@" + item.Pool.ToString().ToLower();
                }
            }
            return bestName;
        }


        public static void DoMining(string key, List<KeyValuePair<string, string>> paths)
        {
            foreach (KeyValuePair<string, string> item in paths)
            {
                if (0 == String.Compare(key, item.Key, true))
                {
                    CloseExistsMiner();
                    RunMiner(item.Value);
                }
            }
        }


        private static void RunMiner(string path)
        {
            try
            {
                var startInfo = new ProcessStartInfo();
                startInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
                startInfo.FileName = path;
                _runningMinerProcess = System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private static void CloseExistsMiner()
        {
            if (_runningMinerProcess != null)
            {
                if (!_runningMinerProcess.HasExited)
                {
                    _runningMinerProcess.Kill();
                    // wait for process has killed.
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

    }
}
