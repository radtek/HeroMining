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
using CryptoMining.ApplicationCore.Miner;
using CryptoMining.ApplicationCore.Log;

namespace CryptoMiningTest
{
    [TestClass]
    public class MinerTest
    {
        [TestMethod]
        public void TestSerializeMinerControl()
        {
            MinerModel miners = new MinerModel();
            List<KeyValuePair<string, string>> path = new List<KeyValuePair<string, string>>();
            List<string> exclude = new List<string>();
            path.Add(new KeyValuePair<string, string>("skein@zpool", @"C:\SoftwareMiner\CryptoDredge_0.9.2\start-BTC-skein-zpool.bat"));
            path.Add(new KeyValuePair<string, string>("x17@zpool", @"C:\SoftwareMiner\z-enemy.1-16-cuda9.2_x64\start-BTC-x17-zpool.bat"));
            exclude.Add("x16r@zerg");
            exclude.Add("x17@zerg");
            miners.SwapTime = 1;
            miners.UseCurrent = true;
            miners.Path = path;
            miners.Exclude = exclude;
            string json = JsonConvert.SerializeObject(miners);
            System.IO.File.WriteAllText("miner.json", json);
            Assert.AreEqual(true, json.Length > 0);
        }


        [TestMethod]
        public void TestDeserializeMinerControl()
        {
            string json = System.IO.File.ReadAllText("miner.json");
            MinerModel miners = JsonConvert.DeserializeObject<MinerModel>(json);
            Assert.AreEqual(true, miners.SwapTime > 0);
        }


        [TestMethod]
        public void TestGetBestAlgorPrice()
        {
            string json = System.IO.File.ReadAllText("miner.json");
            MinerModel miners = JsonConvert.DeserializeObject<MinerModel>(json);
            List<AlgorithmResult> algors = new List<AlgorithmResult>();
            List<KeyValuePair<string, string>> paths = new List<KeyValuePair<string, string>>();
            AlgorithmResult algorX17 = new AlgorithmResult();
            algorX17.name = "x17";
            algorX17.Pool = PoolName.Zpool;
            algorX17.estimate_current = 600;
            algorX17.estimate_last24h = 700;
            algors.Add(algorX17);

            AlgorithmResult algorSkein = new AlgorithmResult();
            algorSkein.name = "skein";
            algorSkein.Pool = PoolName.Zpool;
            algorSkein.estimate_current = 500;
            algorSkein.estimate_last24h = 400;
            algors.Add(algorSkein);

            paths.Add(new KeyValuePair<string, string>("x17@zpool", "c:\\miner\\start-t-rex.bat"));
            miners.Path = paths;
            string algorAtPool;
            double price;
            (algorAtPool, price) = MinerControl.FindBestPrice(algors, true, miners);
            Assert.AreEqual(0, String.Compare("x17@zpool", algorAtPool, true));

        }


        [TestMethod]
        public void TestDoMining()
        {
            List<AlgorithmResult> algors = new List<AlgorithmResult>();
            List<KeyValuePair<string, string>> paths = new List<KeyValuePair<string, string>>();
            string json = System.IO.File.ReadAllText("miner.json");
            MinerModel miners = JsonConvert.DeserializeObject<MinerModel>(json);
            AlgorithmResult algorX17 = new AlgorithmResult();
            algorX17.name = "x17";
            algorX17.Pool = PoolName.Zpool;
            algorX17.estimate_current = 600;
            algorX17.estimate_last24h = 700;
            algors.Add(algorX17);

            AlgorithmResult algorSkein = new AlgorithmResult();
            algorSkein.name = "skein";
            algorSkein.Pool = PoolName.Zpool;
            algorSkein.estimate_current = 500;
            algorSkein.estimate_last24h = 400;
            algors.Add(algorSkein);

            paths.Add(new KeyValuePair<string, string>("x17@zpool", "C:\\SoftwareMiner\\CryptoDredge_0.9.3\\start-BCD-bcd-zpool.bat"));
            miners.Path = paths;
            string bestAlgorAtPool;
            double bestPrice;
            (bestAlgorAtPool, bestPrice) = MinerControl.FindBestPrice(algors, true, miners);


            MinerControl.DoMining(bestAlgorAtPool, miners.Path);

            System.Threading.Thread.Sleep(12000);

            AlgorithmResult algorNewGreater = new AlgorithmResult();
            algorNewGreater.name = "skein";
            algorNewGreater.Pool = PoolName.Zpool;
            algorNewGreater.estimate_current = 1000;
            algorNewGreater.estimate_last24h = 1000;
            algors.Add(algorNewGreater);

            (bestAlgorAtPool, bestPrice) = MinerControl.FindBestPrice(algors, true, miners);
            MinerControl.DoMining(bestAlgorAtPool, miners.Path);

            Assert.AreEqual(true, miners.SwapTime > 0);
        }

        [TestMethod]
        public void TestKillProcess()
        {
            string path = "C:\\SoftwareMiner\\CryptoDredge_0.9.3\\start-BCD-bcd-zpool.bat";
            Process _runningMinerProcess;
            var startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            //        startInfo.RedirectStandardOutput = false;
            startInfo.FileName = path;
            _runningMinerProcess = System.Diagnostics.Process.Start(startInfo);
            string message = DateTime.Now.ToString("yyyy-MM-dd HH':'mm") + " start " + path;
            EasyLog.Log(@"c:\\Log\\minerlog.txt", message);
            System.Threading.Thread.Sleep(10000);
            if (_runningMinerProcess != null)
            {
                MinerControl.KillProcessAndChildrens(_runningMinerProcess.Id);
                message = DateTime.Now.ToString("yyyy-MM-dd HH':'mm") + " stop " + _runningMinerProcess.StartInfo.FileName;
                EasyLog.Log(@"c:\\Log\\minerlog.txt", message);
                _runningMinerProcess = null;
            }
            Assert.AreEqual(null, _runningMinerProcess);
        }
    }
}