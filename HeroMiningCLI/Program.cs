using CryptoMining.ApplicationCore;
using CryptoMining.ApplicationCore.Exchange;
using CryptoMining.ApplicationCore.Pool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HeroMiningCLI
{
    class Program
    {
        private static bool _isDebug = false;


        static void Main(string[] args)
        {
            bool needWriteFile = false;
            int keepMoreThan = -1;

            string filename = "";
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-debug")
                    {
                        _isDebug = true;
                    }
                    else if (args[i] == "-f")
                    {
                        try
                        {
                            needWriteFile = true;
                            filename = args[i + 1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("please specify a correct filename like -f output.csv");
                        }
                    }
                    else if (args[i] == "-g")
                    {
                        try
                        {
                            needWriteFile = true;
                            keepMoreThan = int.Parse(args[i + 1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("please specify a valid argument for example -g 100");
                            return;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("please specify a valid argument for example -g 100");
                            return;
                        }
                    }
                    else if (args[i] == "-help" || args[i] == "-h")
                    {
                        ShowUsage();
                        return;
                    }
                }
            }

            BsodAPI bsod = new BsodAPI();
            GosAPI gos = new GosAPI();

            Rig myRig = ReadRigConfig("myrig.json");

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

            foreach (GpuInfo gpu in myRig.Chipsets)
            {
                Console.WriteLine(string.Format("{0} x {1} ", gpu.Name, gpu.Count));
            }


            foreach (KeyValuePair<string, double> item in prices)
            {
                if (item.Value > 0)
                {
                    string line = string.Format("{0} = {1} ", item.Key, item.Value);
                    Console.WriteLine(line);
                }
            }

            if (needWriteFile)
            {
                System.IO.File.WriteAllText(filename, result.ToString());
                Console.WriteLine("File {0} saved.", filename);
            }
            Console.ReadLine();


        }

        private static void ShowUsage()
        {
            Console.WriteLine("-help or -h\tshow usage command.");
            Console.WriteLine("-f         \tsave output to file. example -f output.csv.");
            Console.WriteLine("-g         \tShow only record mining more than a number specify. example -g 100");
            Console.WriteLine("-debug     \tshow debug message.");
        }

        private static double GetMiningBahtPerDay(string coinSymbol, string algorithm, PoolName pool, ExchangeName exchangeName, MiningCalculator calc)
        {
            double hashRate = HashPower.GetAlgorithmHashRate(algorithm);
            calc.MyHashRate = hashRate;
            if (_isDebug)
                Console.WriteLine(string.Format("My hashrate of algor {0} = {1} ", algorithm, calc.MyHashRate));
            double bahtPerDay = calc.GetTotalBahtMiningPerday(coinSymbol, pool, exchangeName);
            if (_isDebug)
                Console.WriteLine(string.Format("Total baht per day at {0} of {1} ==========> {2} Baht ", exchangeName, coinSymbol, bahtPerDay));
            return bahtPerDay;
        }


        private static Rig ReadRigConfig(string filename)
        {
            Rig myRig = null;
            if (System.IO.File.Exists(filename))
            {
                try
                {
                    string json = System.IO.File.ReadAllText(filename);
                    myRig = JsonConvert.DeserializeObject<Rig>(json);
                }
                catch (Exception)
                {
                    Console.WriteLine(string.Format("Invalid configuration please verify file {0}" , filename));
                }
            }
            else
            {
                Console.WriteLine(string.Format("Not found configuration file {0}.", filename));
            }
            return myRig;
        }
    }
}
