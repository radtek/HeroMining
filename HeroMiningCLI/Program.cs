using CryptoMining.ApplicationCore;
using CryptoMining.ApplicationCore.Exchange;
using CryptoMining.ApplicationCore.Miner;
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
        private static int _keepMoreThan = -1;
        private static bool _needMonitor = false;
        private static string _monitorCoin = "";
        private static FiatCurrency _fiat = FiatCurrency.THB;
        private static List<CryptoCurrencyResult> _coinsResult = new List<CryptoCurrencyResult>();
        private static List<AlgorithmResult> _algorsResult = new List<AlgorithmResult>();
        private static bool _needDig = false;
        private static MinerModel _miners;

        static void ParseArgument(string[] args)
        {
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
                            _keepMoreThan = int.Parse(args[i + 1]);
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
                        _needMonitor = true;
                        try
                        {
                            _monitorCoin = args[i + 1];
                            _monitorCoin = _monitorCoin.ToUpper();
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
                    else if (args[i] == "-c")
                    {
                        try
                        {
                            string fiatCurrency = args[i + 1].ToLower();
                            _fiat = (fiatCurrency == "usd") ? FiatCurrency.USD : FiatCurrency.THB;
                        }
                        catch
                        {
                            Console.WriteLine("please specify a valid parameter -c eg. -c usd or -c baht");
                        }
                    }
                    else if (args[i] == "-dig")
                    {
                        string minerConfig = "";
                        try
                        {
                            minerConfig = args[i + 1].ToLower();
                            string json = System.IO.File.ReadAllText(minerConfig);
                            _miners = JsonConvert.DeserializeObject<MinerModel>(json);
                            _needDig = true;
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine(string.Format("Invalid configuration please verify file {0}. Reason: {1}", minerConfig, err.Message));
                            Console.WriteLine("Press Enter to exit.");
                            Console.ReadLine();
                            System.Environment.Exit(-1);
                        }
                    }
                }
            }
        }

        private static void ExploreMiningDetail(string algorithmName, PoolName pool)
        {
            double btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, pool, true, _fiat);
            double btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, pool, false, _fiat);
            if (btc24HoursPerDay > _keepMoreThan || btcCurrentPerDay > _keepMoreThan)
            {
                AlgorithmResult algorAtBlockMaster = new AlgorithmResult();
                algorAtBlockMaster.name = algorithmName;
                algorAtBlockMaster.Pool = pool;
                algorAtBlockMaster.estimate_current = btcCurrentPerDay;
                algorAtBlockMaster.estimate_last24h = btc24HoursPerDay;
                _algorsResult.Add(algorAtBlockMaster);
            }
        }

        private static void DoMining()
        {
            string bestAlgor = "";
            string bestCoin = "";
            double bestAlgorPrice = 0;
            double bestCoinPrice = 0;
            try
            {
                (bestAlgor, bestAlgorPrice) = MinerControl.FindBestPrice(_algorsResult, true, _miners);
                (bestCoin, bestCoinPrice) = MinerControl.FindBestPrice(_coinsResult, _miners);
                if (bestAlgorPrice > bestCoinPrice)
                {

                    if (MinerControl.DoMining(bestAlgor, _miners.Path))
                    {
                        Console.WriteLine($"Start mining {bestAlgor} estimate {bestAlgorPrice} per day.");
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Found best algor {bestAlgor} but not found miner config.");
                    }
                }
                else
                {

                    if (MinerControl.DoMining(bestCoin, _miners.Path))
                    {
                        Console.WriteLine($"Start mining {bestCoin} estimate {bestCoinPrice} per day.");
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Found best coin {bestCoin} but not found miner config.");
                    }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine($"Error : Can not start mining {bestAlgor} estimate {bestAlgorPrice} per day.");
                Console.WriteLine($"Error : {err.Message}. Error detail : {err.InnerException.Message}");
                Console.WriteLine();
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
                System.Environment.Exit(-1);
            }
        }

        private static void ExploreMiningDetail(CryptoCurrency coins, string symbol, PoolName pool, ExchangeName exchange)
        {
            double moneyPerDay = GetMiningFiatPerDay(symbol, coins[symbol].algo, pool, exchange);
            if (moneyPerDay > _keepMoreThan)
            {
                _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, coins[symbol].algo, pool, exchange, moneyPerDay));
                CryptoCurrencyResult coin = new CryptoCurrencyResult();
                coin.symbol = symbol;
                coin.h24_btc = moneyPerDay;
                coin.Pool = pool;
                coin.algo = coins[symbol].algo;
                coin.Exchange = exchange;
                _coinsResult.Add(coin);
            }
        }

        private static void ExploreMining(CryptoCurrency coins, string symbol, PoolName pool)
        {
            string coinAtPool = string.Format("{0}@{1}", symbol, pool);
            if (coins != null && coins[symbol] != null && !ExcludeCoinAtPool.ExcludeCoins.Contains(coinAtPool))
            {
                ExploreMiningDetail(coins, symbol, pool, ExchangeName.CryptoBridge);
                ExploreMiningDetail(coins, symbol, pool, ExchangeName.Crex24);
                ExploreMiningDetail(coins, symbol, pool, ExchangeName.Cryptopia);
                ExploreMiningDetail(coins, symbol, pool, ExchangeName.Binance);
                ExploreMiningDetail(coins, symbol, pool, ExchangeName.CoinExchange);
                if (_needToShowCoinsNumPerDay)
                    ShowNumOfCoinMiningPerDay(symbol, pool);
            }
        }

        static void Main(string[] args)
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
            BsodAPI bsod = new BsodAPI();
            GosAPI gos = new GosAPI();

            Console.WriteLine("Start loading pool data. Please wait ...");
            _calc = new MiningCalculator();

            ParseArgument(args);

            Rig myRig = ReadRigConfig("myrig.json");
            HashPower.SetupHardware(myRig);

            string input = "";
            while (input != Environment.NewLine && input != "q")
            {

                CryptoCurrency bsodCoins = _calc.PoolCoins[0];
                CryptoCurrency gosCoins = _calc.PoolCoins[1];
                CryptoCurrency iceCoins = _calc.PoolCoins[2];
                Algorithm zergAlgorithm = _calc.PoolAlgorithms[0];
                Algorithm phiAlgorithm = _calc.PoolAlgorithms[1];
                Algorithm zpoolAlgorithm = _calc.PoolAlgorithms[2];
                Algorithm ahashAlgorithm = _calc.PoolAlgorithms[3];
                Algorithm blockMasterAlgorithm = _calc.PoolAlgorithms[4];

                Console.WriteLine("Analyzing ...");

                #region monitor mode
                if (_needMonitor) // monitor mode
                {
                    foreach (GpuInfo gpu in myRig.Chipsets)
                    {
                        Console.WriteLine(string.Format("{0} x {1} ", gpu.Name, gpu.Count));
                    }
                    string algorithmName = _monitorCoin.ToLower();

                    Console.WriteLine("Monitoring " + zpoolAlgorithm[algorithmName] != null ? algorithmName : _monitorCoin);
                    Console.WriteLine();

                    string symbol = _monitorCoin;

                    _coinsResult.Clear();

                    ExploreMining(bsodCoins, symbol, PoolName.Bsod);
                    ExploreMining(gosCoins, symbol, PoolName.Gos);
                    ExploreMining(iceCoins, symbol, PoolName.IceMining);

                    // display result
                    _coinsResult.Sort();
                    foreach (CryptoCurrencyResult coin in _coinsResult)
                    {
                        string line = string.Format("{0} {1}(24hr)\t{2}@{3} \tsale@{4}", coin.h24_btc.ToString("N2"), _fiat, coin.symbol, coin.Pool, coin.Exchange);
                        Console.WriteLine(line);
                    }

                    if (zergAlgorithm[algorithmName] != null)
                    {
                        _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                        double btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zerg, true, _fiat);
                        double btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zerg, false, _fiat);

                        AlgorithmResult algorAtZerg = new AlgorithmResult();
                        algorAtZerg.name = algorithmName;
                        algorAtZerg.Pool = PoolName.Zerg;
                        algorAtZerg.estimate_current = btcCurrentPerDay;
                        algorAtZerg.estimate_last24h = btc24HoursPerDay;
                        _algorsResult.Add(algorAtZerg);

                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.Zerg);
                        }
                    }


                    if (phiAlgorithm[algorithmName] != null)
                    {
                        _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                        double btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.PhiPhi, true, _fiat);
                        double btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.PhiPhi, false, _fiat);

                        AlgorithmResult algorAtPhi = new AlgorithmResult();
                        algorAtPhi.name = algorithmName;
                        algorAtPhi.Pool = PoolName.PhiPhi;
                        algorAtPhi.estimate_current = btcCurrentPerDay;
                        algorAtPhi.estimate_last24h = btc24HoursPerDay;
                        _algorsResult.Add(algorAtPhi);

                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.PhiPhi);
                        }
                    }


                    if (ahashAlgorithm[algorithmName] != null)
                    {
                        _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                        double btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.AhashPool, true, _fiat);
                        double btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.AhashPool, false, _fiat);

                        AlgorithmResult algorAtAhash = new AlgorithmResult();
                        algorAtAhash.name = algorithmName;
                        algorAtAhash.Pool = PoolName.AhashPool;
                        algorAtAhash.estimate_current = btcCurrentPerDay;
                        algorAtAhash.estimate_last24h = btc24HoursPerDay;
                        _algorsResult.Add(algorAtAhash);

                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.AhashPool);
                        }
                    }

                    if (blockMasterAlgorithm[algorithmName] != null)
                    {
                        _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                        double btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.BlockMaster, true, _fiat);
                        double btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.BlockMaster, false, _fiat);

                        AlgorithmResult algorAtBlockMaster = new AlgorithmResult();
                        algorAtBlockMaster.name = algorithmName;
                        algorAtBlockMaster.Pool = PoolName.BlockMaster;
                        algorAtBlockMaster.estimate_current = btcCurrentPerDay;
                        algorAtBlockMaster.estimate_last24h = btc24HoursPerDay;
                        _algorsResult.Add(algorAtBlockMaster);

                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.BlockMaster);
                        }
                    }

                    if (zpoolAlgorithm[algorithmName] != null)
                    {
                        _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                        double btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zpool, true, _fiat);
                        double btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zpool, false, _fiat);

                        AlgorithmResult algorAtZpool = new AlgorithmResult();
                        algorAtZpool.name = algorithmName;
                        algorAtZpool.Pool = PoolName.Zpool;
                        algorAtZpool.estimate_current = btcCurrentPerDay;
                        algorAtZpool.estimate_last24h = btc24HoursPerDay;
                        _algorsResult.Add(algorAtZpool);

                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.Zpool);
                        }
                    }

                    _algorsResult.Sort();
                    foreach (AlgorithmResult algor in _algorsResult)
                    {
                        string line = string.Format("{0} {1}(24hr)\t{2} {3}(current) \t{4}@{5}", algor.estimate_last24h.ToString("N2"), _fiat, algor.estimate_current.ToString("N2"), _fiat, algor.name, algor.Pool);
                        Console.WriteLine(line);
                        _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", algor.name, algor.Pool, "bx", algor.estimate_last24h.ToString("F2"), algor.estimate_current.ToString("F2")));

                    }


                    _calc.RefreshPool();


                    if (_needWriteFile)
                    {
                        System.IO.File.WriteAllText(_filename, _result.ToString());
                        Console.WriteLine("File {0} saved.", _filename);
                    }
                }
                #endregion monitor mode

                #region normal mode
                else // normal mode
                {
                    foreach (string symbol in CurrencyName.Symbols)
                    {

                        ExploreMining(bsodCoins, symbol, PoolName.Bsod);
                        ExploreMining(gosCoins, symbol, PoolName.Gos);
                        ExploreMining(iceCoins, symbol, PoolName.IceMining);
                    }

                    _coinsResult.Sort();

                    // display

                    Console.WriteLine();
                    Console.WriteLine("Analyzing gpu ...");
                    foreach (GpuInfo gpu in myRig.Chipsets)
                    {
                        Console.WriteLine(string.Format("{0} x {1} ", gpu.Name, gpu.Count));
                    }
                    Console.WriteLine();

                    foreach (CryptoCurrencyResult coin in _coinsResult)
                    {
                        string line = string.Format("{0} {1}(24hr)\t{2}@{3} \tsale@{4}", coin.h24_btc.ToString("N2"), _fiat, coin.symbol, coin.Pool, coin.Exchange);
                        _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", coin.symbol, coin.algo, coin.Pool, coin.Exchange, coin.h24_btc.ToString("F2")));
                        Console.WriteLine(line);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Analyzing auto btc pool ...");
                    Console.WriteLine();

                    foreach (string algorithmName in AlgoritmName.Symbols)
                    {
                        _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                        ExploreMiningDetail(algorithmName, PoolName.Zerg);
                        ExploreMiningDetail(algorithmName, PoolName.PhiPhi);
                        ExploreMiningDetail(algorithmName, PoolName.AhashPool);
                        ExploreMiningDetail(algorithmName, PoolName.Zpool);
                        ExploreMiningDetail(algorithmName, PoolName.BlockMaster);
                    }

                    _algorsResult.Sort();
                    foreach (AlgorithmResult algor in _algorsResult)
                    {
                        string line = string.Format("{0} {1}(24hr)  \t{2} {3}(current) \t{4}@{5}", algor.estimate_last24h.ToString("N2"), _fiat, algor.estimate_current.ToString("N2"), _fiat, algor.name, algor.Pool);
                        Console.WriteLine(line);
                        _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", algor.name, algor.Pool, "bx", algor.estimate_last24h.ToString("F2"), algor.estimate_current.ToString("F2")));
                    }


                    if (_needWriteFile)
                    {
                        System.IO.File.WriteAllText(_filename, _result.ToString());
                        Console.WriteLine("File {0} saved.", _filename);
                    }

                }
                #endregion normal mode

                #region dig mode
                if (_needDig)
                {
                    Console.WriteLine();
                    DoMining();
                }
                #endregion dig mode

                if (_miners != null)
                {
                    Console.WriteLine();
                    Console.WriteLine(string.Format("Next watch in {0} hour.", _miners.SwapTime));
                    int sleepTime = _miners.SwapTime > int.MaxValue ? 3600000 : (int)(_miners.SwapTime * 3600000);
                    System.Threading.Thread.Sleep(sleepTime);

                    Console.WriteLine("Start reloading pool data. Please wait ...");
                    _calc.RefreshPool();
                    _calc.RefreshPrice();
                }
                else
                {
                    Console.ReadLine();
                    return;
                }
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
            Console.WriteLine("-c         \tcalculate fiat currency revenue to usd or baht. example -c usd or -c baht");
            Console.WriteLine("-dig       \tRun miner with the best price. example -dig miner.json");
            Console.WriteLine("-debug     \tshow debug message.");
        }

        private static void ShowNumOfCoinMiningPerDay(string coinSymbol, PoolName poolName)
        {
            double coinsPerDay = _calc.GetNumOfCoinMiningPerDay(coinSymbol, poolName);
            Console.WriteLine(string.Format("{2}@{0} will receive {1} {2} per day.", poolName.ToString(), coinsPerDay, coinSymbol));
        }

        private static void ShowNumOfBtcMiningPerDay(string algorithmName, PoolName poolName)
        {
            double btcPerDay = _calc.GetTotalBtcMiningPerday(algorithmName, poolName, false);
            Console.WriteLine(string.Format("{0} mining@{1} will receive {2} btc per day.", algorithmName, poolName.ToString(), btcPerDay));
        }

        private static double GetMiningFiatPerDay(string coinSymbol, string algorithm, PoolName pool, ExchangeName exchangeName)
        {
            double hashRate = HashPower.GetAlgorithmHashRate(algorithm);
            _calc.MyHashRate = hashRate;
            if (_isDebug)
                Console.WriteLine(string.Format("My hashrate of algor {0} = {1} ", algorithm, _calc.MyHashRate));
            double moneyPerDay = _calc.GetTotalFiatMoneyMiningPerday(coinSymbol, pool, exchangeName, _fiat);
            if (_isDebug)
                Console.WriteLine(string.Format("Total money per day at {0} of {1} ==========> {2} {3} ", exchangeName, coinSymbol, moneyPerDay, _fiat));
            return moneyPerDay;
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
                    Console.WriteLine(string.Format("Invalid configuration please verify file {0}", filename));
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
