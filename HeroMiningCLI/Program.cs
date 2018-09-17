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
        private static StringBuilder _result = new StringBuilder();
        private static bool _needWriteFile = false;
        private static string _filename = "";
        private static MiningCalculator _calc = null;
        private static bool _needToShowCoinsNumPerDay = false;

        static void Main(string[] args)
        {

            Console.CancelKeyPress += Console_CancelKeyPress;
            bool needMonitor = false;
            string monitorCoin = "";
            int keepMoreThan = -1;

           
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
                            _needWriteFile = true;
                            _filename = args[i + 1];
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
                            _needWriteFile = true;
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
                    else if (args[i] == "-m")
                    {
                        needMonitor = true;
                        try
                        {
                            monitorCoin = args[i + 1];
                            monitorCoin = monitorCoin.ToUpper();
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("please specify a valid coin symbol for example -m MANO");
                            return;
                        }
                    }
                    else if (args[i] == "-t")
                    {
                        _needToShowCoinsNumPerDay = true;
                    }
                }
            }

            BsodAPI bsod = new BsodAPI();
            GosAPI gos = new GosAPI();

            Rig myRig = ReadRigConfig("myrig.json");

            HashPower.SetupHardware(myRig);

            Console.WriteLine("Start loading pool data. Please wait ...");
            _calc = new MiningCalculator();
            CryptoCurrency bsodCoins = _calc.PoolCoins[0];
            CryptoCurrency gosCoins = _calc.PoolCoins[1];
            Algorithm zergAlgorithm = _calc.PoolAlgorithms[0];
            Algorithm phiAlgorithm = _calc.PoolAlgorithms[1];

            SortedDictionary<string, double> prices = new SortedDictionary<string, double>();
            

            Console.WriteLine("Analyzing ...");

            if (needMonitor)
            {
                foreach (GpuInfo gpu in myRig.Chipsets)
                {
                    Console.WriteLine(string.Format("{0} x {1} ", gpu.Name, gpu.Count));
                }
                string algorithmName = monitorCoin.ToLower();

                Console.WriteLine("Monitoring " + zergAlgorithm[algorithmName] != null ? algorithmName : monitorCoin);
                Console.WriteLine();
                string input = "";
                string symbol = monitorCoin;
                while (input != Environment.NewLine && input != "q")
                {
                    if (bsodCoins != null && bsodCoins[symbol] != null)
                    {
                        double bahtPerDay = GetMiningBahtPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.CryptoBridge);
                        if (bahtPerDay > keepMoreThan)
                        {
                            prices.Add(symbol + " (" + bsodCoins[symbol].algo + ") mining@bsod sale@crypto-bridge ", bahtPerDay);
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "crypto-bridge", bahtPerDay));
                        }

                        bahtPerDay = GetMiningBahtPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Crex24);
                        if (bahtPerDay > keepMoreThan)
                        {
                            prices.Add(symbol + " (" + bsodCoins[symbol].algo + ") mining@bsod sale@crex24", bahtPerDay);
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "crex24", bahtPerDay));
                        }

                        if (_needToShowCoinsNumPerDay)
                            ShowNumOfCoinMiningPerDay(symbol, PoolName.Bsod);
                    }

                    if (gosCoins != null && gosCoins[symbol] != null)
                    {

                        double bahtPerDay = GetMiningBahtPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.CryptoBridge);
                        if (bahtPerDay > keepMoreThan)
                        {
                            prices.Add(symbol + " (" + gosCoins[symbol].algo + ") mining@gos sale@crypto-bridge", bahtPerDay);
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "crypto-bridge", bahtPerDay));
                        }

                        bahtPerDay = GetMiningBahtPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Crex24);
                        if (bahtPerDay > keepMoreThan)
                        {
                            prices.Add(symbol + " (" + gosCoins[symbol].algo + ") mining@gos sale@crex24", bahtPerDay);
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "crex24", bahtPerDay));
                        }
                        if (_needToShowCoinsNumPerDay)
                            ShowNumOfCoinMiningPerDay(symbol, PoolName.Gos);
                    }


                    foreach (KeyValuePair<string, double> item in prices)
                    {
                        if (item.Value > 0)
                        {
                            string line = string.Format("{0} = {1} ", item.Key, item.Value);
                            Console.WriteLine(line);
                        }
                    }

                    if (zergAlgorithm[algorithmName] != null)
                    {
                        _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                        double btcCurrentPerDay = _calc.GetTotalBahtMiningPerday(algorithmName, PoolName.Zerg, true);
                        double btc24HoursPerDay = _calc.GetTotalBahtMiningPerday(algorithmName, PoolName.Zerg, false);
                        string line = string.Format("{0} mining@zergpool sale_btc@bx estimate_current = {1} baht estimate_24hours = {2} baht ", algorithmName, btcCurrentPerDay, btc24HoursPerDay);
                        _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", algorithmName, "zergpool", "bx", btcCurrentPerDay.ToString("F2"), btc24HoursPerDay.ToString("F2")));
                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.Zerg);
                        }
                        Console.WriteLine(line);
                    }


                    if (phiAlgorithm[algorithmName] != null)
                    {
                        _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                        double btcCurrentPerDay = _calc.GetTotalBahtMiningPerday(algorithmName, PoolName.PhiPhi, true);
                        double btc24HoursPerDay = _calc.GetTotalBahtMiningPerday(algorithmName, PoolName.PhiPhi, false);
                        string line = string.Format("{0} mining@phi-phi-pool sale_btc@bx estimate_current = {1} baht estimate_24hours = {2} baht ", algorithmName, btcCurrentPerDay, btc24HoursPerDay);
                        _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", algorithmName, "zergpool", "bx", btcCurrentPerDay.ToString("F2"), btc24HoursPerDay.ToString("F2")));
                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.PhiPhi);
                        }
                        Console.WriteLine(line);
                    }


                    Console.WriteLine();
                    Console.WriteLine("Press Control + C if u need to exit.");
                    Console.WriteLine("Re calculate. Please wait 1 minutes ...");
                    prices.Clear();
                    System.Threading.Thread.Sleep(60000);
                    _calc.RefreshPool();
                }

                if (_needWriteFile)
                {
                    System.IO.File.WriteAllText(_filename, _result.ToString());
                    Console.WriteLine("File {0} saved.", _filename);
                }
            }
            else // normal mode
            {

                foreach (string symbol in CurrencyName.Symbols)
                {
                    if (bsodCoins != null && bsodCoins[symbol] != null)
                    {
                        double bahtPerDay = GetMiningBahtPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.CryptoBridge);
                        if (bahtPerDay > keepMoreThan)
                        {
                            prices.Add(symbol + " (" + bsodCoins[symbol].algo + ") mining@bsod sale@crypto-bridge ", bahtPerDay);
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "crypto-bridge", bahtPerDay));
                        }

                        bahtPerDay = GetMiningBahtPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Crex24);
                        if (bahtPerDay > keepMoreThan)
                        {
                            prices.Add(symbol + " (" + bsodCoins[symbol].algo + ") mining@bsod sale@crex24", bahtPerDay);
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "crex24", bahtPerDay));
                        }
                        if (_needToShowCoinsNumPerDay)
                            ShowNumOfCoinMiningPerDay(symbol, PoolName.Bsod);
                    }

                    if (gosCoins != null && gosCoins[symbol] != null)
                    {

                        double bahtPerDay = GetMiningBahtPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.CryptoBridge);
                        if (bahtPerDay > keepMoreThan)
                        {
                            prices.Add(symbol + " (" + gosCoins[symbol].algo + ") mining@gos sale@crypto-bridge", bahtPerDay);
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "crypto-bridge", bahtPerDay));
                        }

                        bahtPerDay = GetMiningBahtPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Crex24);
                        if (bahtPerDay > keepMoreThan)
                        {
                            prices.Add(symbol + " (" + gosCoins[symbol].algo + ") mining@gos sale@crex24", bahtPerDay);
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "crex24", bahtPerDay));
                        }
                        if (_needToShowCoinsNumPerDay)
                            ShowNumOfCoinMiningPerDay(symbol, PoolName.Gos);
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
                        string line = string.Format("{0} = {1} baht", item.Key, item.Value);
                        Console.WriteLine(line);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Analyzing auto btc pool ...");
                Console.WriteLine();

                foreach (string algorithmName in AlgoritmName.Symbols)
                {
                    _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                    double btcCurrentPerDay = _calc.GetTotalBahtMiningPerday(algorithmName, PoolName.Zerg, true);
                    double btc24HoursPerDay = _calc.GetTotalBahtMiningPerday(algorithmName, PoolName.Zerg, false);
                    string line = string.Format("{0} mining@Zerg sale_btc@bx current = {1} baht 24hours = {2} baht ", algorithmName, btcCurrentPerDay, btc24HoursPerDay);
                    Console.WriteLine(line);
                   _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", algorithmName, "Zerg", "bx", btcCurrentPerDay.ToString("F2"), btc24HoursPerDay.ToString("F2")));

                    btcCurrentPerDay = _calc.GetTotalBahtMiningPerday(algorithmName, PoolName.PhiPhi, true);
                    btc24HoursPerDay = _calc.GetTotalBahtMiningPerday(algorithmName, PoolName.PhiPhi, false);
                    line = string.Format("{0} mining@Phi-Phi sale_btc@bx current = {1} baht 24hours = {2} baht ", algorithmName, btcCurrentPerDay, btc24HoursPerDay);
                    Console.WriteLine(line);
                    _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", algorithmName, "Phi-Phi", "bx", btcCurrentPerDay.ToString("F2"), btc24HoursPerDay.ToString("F2")));

                }

                if (_needWriteFile)
                {
                    System.IO.File.WriteAllText(_filename, _result.ToString());
                    Console.WriteLine("File {0} saved.", _filename);
                }
                Console.ReadLine();
            }

        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (_needWriteFile)
            {
                System.IO.File.WriteAllText(_filename, _result.ToString());
                Console.WriteLine("File {0} saved.", _filename);
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("-help or -h\tshow usage command.");
            Console.WriteLine("-f         \tsave output to file. example -f output.csv.");
            Console.WriteLine("-g         \tShow only record mining more than a number specify. example -g 100");
            Console.WriteLine("-m         \tMonitor a specific coin. example -m rvn");
            Console.WriteLine("-t         \tShow number of coin that will receive mining per day");
            Console.WriteLine("-debug     \tshow debug message.");
        }

        private static void ShowNumOfCoinMiningPerDay(string coinSymbol, PoolName  poolName)
        {
            double coinsPerDay = _calc.GetNumOfCoinMiningPerDay(coinSymbol, poolName);
            Console.WriteLine(string.Format("mining@{0} will receive {1} {2} per day.",poolName.ToString(), coinsPerDay, coinSymbol));
        }

        private static void ShowNumOfBtcMiningPerDay(string algorithmName, PoolName poolName)
        {
            double btcPerDay = _calc.GetTotalBtcMiningPerday(algorithmName, poolName, false);
            Console.WriteLine(string.Format("{0} mining@{1} will receive {2} btc per day.", algorithmName , poolName.ToString(), btcPerDay ));
        }

        private static double GetMiningBahtPerDay(string coinSymbol, string algorithm, PoolName pool, ExchangeName exchangeName)
        {
            double hashRate = HashPower.GetAlgorithmHashRate(algorithm);
            _calc.MyHashRate = hashRate;
            if (_isDebug)
                Console.WriteLine(string.Format("My hashrate of algor {0} = {1} ", algorithm, _calc.MyHashRate));
            double bahtPerDay = _calc.GetTotalBahtMiningPerday(coinSymbol, pool, exchangeName);
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
