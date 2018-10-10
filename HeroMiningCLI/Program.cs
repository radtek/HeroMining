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
        private static int _keepMoreThan = -1;
        private static bool _needMonitor = false;
        private static string _monitorCoin = "";
        private static FiatCurrency _fiat = FiatCurrency.Baht;

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
                            _needWriteFile = true;
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
                            _fiat = (fiatCurrency == "usd") ? FiatCurrency.Usd : FiatCurrency.Baht;
                        }
                        catch
                        {
                            Console.WriteLine("please specify a valid parameter -c eg. -c usd or -c baht");
                        }
                    }
                }
            }


        }

        static void Main(string[] args)
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
            BsodAPI bsod = new BsodAPI();
            GosAPI gos = new GosAPI();
            List<CryptoCurrencyResult> coinsResult = new List<CryptoCurrencyResult>();
            List<AlgorithmResult> algorResult = new List<AlgorithmResult>();

            ParseArgument(args);
         

            Rig myRig = ReadRigConfig("myrig.json");

            HashPower.SetupHardware(myRig);

            Console.WriteLine("Start loading pool data. Please wait ...");
            _calc = new MiningCalculator();
            CryptoCurrency bsodCoins = _calc.PoolCoins[0];
            CryptoCurrency gosCoins = _calc.PoolCoins[1];
            Algorithm zergAlgorithm = _calc.PoolAlgorithms[0];
            Algorithm phiAlgorithm = _calc.PoolAlgorithms[1];
            Algorithm zpoolAlgorithm = _calc.PoolAlgorithms[2];
            Algorithm ahashAlgorithm = _calc.PoolAlgorithms[3];


            Console.WriteLine("Analyzing ...");

            if (_needMonitor) // monitor mode
            {
                foreach (GpuInfo gpu in myRig.Chipsets)
                {
                    Console.WriteLine(string.Format("{0} x {1} ", gpu.Name, gpu.Count));
                }
                string algorithmName = _monitorCoin.ToLower();

                Console.WriteLine("Monitoring " + zergAlgorithm[algorithmName] != null ? algorithmName : _monitorCoin);
                Console.WriteLine();
                string input = "";
                string symbol = _monitorCoin;
                while (input != Environment.NewLine && input != "q")
                {
                    coinsResult.Clear();
                    if (bsodCoins != null && bsodCoins[symbol] != null)
                    {
                        double moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.CryptoBridge);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "crypto-bridge", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.CryptoBridge;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Crex24);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "crex24", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Crex24;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Cryptopia);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "cryptopia", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Cryptopia;
                            coinsResult.Add(coin);
                        }


                        moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Binance);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "binance", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Binance;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.CoinExchange);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, bsodCoins[symbol].algo, "bsod", "coinexchange", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.CoinExchange;
                            coinsResult.Add(coin);
                        }

                        if (_needToShowCoinsNumPerDay)
                            ShowNumOfCoinMiningPerDay(symbol, PoolName.Bsod);
                    }

                    if (gosCoins != null && gosCoins[symbol] != null)
                    {

                        double moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.CryptoBridge);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "crypto-bridge", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.CryptoBridge;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Crex24);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "crex24", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Crex24;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Cryptopia);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "cryptopia", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Cryptopia;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Binance);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "binance", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Binance;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.CoinExchange);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", symbol, gosCoins[symbol].algo, "gos", "coinexchange", moneyPerDay));
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.CoinExchange;
                            coinsResult.Add(coin);
                        }

                        if (_needToShowCoinsNumPerDay)
                            ShowNumOfCoinMiningPerDay(symbol, PoolName.Gos);
                    }

                    coinsResult.Sort();
                    foreach (CryptoCurrencyResult coin in coinsResult)
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
                        algorResult.Add(algorAtZerg);

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
                        algorResult.Add(algorAtPhi);

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
                        algorResult.Add(algorAtAhash);

                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.AhashPool);
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
                        algorResult.Add(algorAtZpool);

                        if (_needToShowCoinsNumPerDay)
                        {
                            ShowNumOfBtcMiningPerDay(algorithmName, PoolName.Zpool);
                        }
                    }

                    algorResult.Sort();
                    foreach (AlgorithmResult algor in algorResult)
                    {
                        string line = string.Format("{0} {1}(24hr)\t{2} {3}(current) \t{4}@{5}", algor.estimate_last24h.ToString("N2"), _fiat, algor.estimate_current.ToString("N2"),_fiat, algor.name, algor.Pool);
                        Console.WriteLine(line);
                        _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", algor.name, algor.Pool, "bx", algor.estimate_last24h.ToString("F2"), algor.estimate_current.ToString("F2")));

                    }


                    Console.WriteLine();
                    Console.WriteLine("Press Control + C if u need to exit.");
                    Console.WriteLine("Re calculate. Please wait 1 minutes ...");
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
                    string coinAtPool = string.Format("{0}@{1}", symbol, PoolName.Bsod);
                    if (bsodCoins != null && bsodCoins[symbol] != null && !ExcludeCoinAtPool.ExcludeCoins.Contains(coinAtPool))
                    {
                        double moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.CryptoBridge);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.CryptoBridge;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Crex24);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Crex24;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Cryptopia);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Cryptopia;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.Binance);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Binance;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, bsodCoins[symbol].algo, PoolName.Bsod, ExchangeName.CoinExchange);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Bsod;
                            coin.algo = bsodCoins[symbol].algo;
                            coin.Exchange = ExchangeName.CoinExchange;
                            coinsResult.Add(coin);
                        }

                        if (_needToShowCoinsNumPerDay)
                            ShowNumOfCoinMiningPerDay(symbol, PoolName.Bsod);
                    }

                    coinAtPool = string.Format("{0}@{1}", symbol, PoolName.Gos);
                    if (gosCoins != null && gosCoins[symbol] != null && !ExcludeCoinAtPool.ExcludeCoins.Contains(coinAtPool))
                    {

                        double moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.CryptoBridge);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.CryptoBridge;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Crex24);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Crex24;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Cryptopia);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Cryptopia;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.Binance);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.Binance;
                            coinsResult.Add(coin);
                        }

                        moneyPerDay = GetMiningFiatPerDay(symbol, gosCoins[symbol].algo, PoolName.Gos, ExchangeName.CoinExchange);
                        if (moneyPerDay > _keepMoreThan)
                        {
                            CryptoCurrencyResult coin = new CryptoCurrencyResult();
                            coin.symbol = symbol;
                            coin.h24_btc = moneyPerDay;
                            coin.Pool = PoolName.Gos;
                            coin.algo = gosCoins[symbol].algo;
                            coin.Exchange = ExchangeName.CoinExchange;
                            coinsResult.Add(coin);
                        }

                        if (_needToShowCoinsNumPerDay)
                            ShowNumOfCoinMiningPerDay(symbol, PoolName.Gos);
                    }
                }

                coinsResult.Sort();

                Console.WriteLine();
                Console.WriteLine("Analyzing gpu ...");
                foreach (GpuInfo gpu in myRig.Chipsets)
                {
                    Console.WriteLine(string.Format("{0} x {1} ", gpu.Name, gpu.Count));
                }
                Console.WriteLine();

                foreach (CryptoCurrencyResult coin in coinsResult)
                {
                    string line = string.Format("{0} {1}(24hr)\t{2}@{3} \tsale@{4}", coin.h24_btc.ToString("N2"), _fiat ,coin.symbol, coin.Pool, coin.Exchange);
                    _result.AppendLine(string.Format("{0},{1},{2},{3},{4}", coin.symbol, coin.algo,coin.Pool, coin.Exchange, coin.h24_btc.ToString("F2")));
                    Console.WriteLine(line);
                }
    
                Console.WriteLine();
                Console.WriteLine("Analyzing auto btc pool ...");
                Console.WriteLine();

                foreach (string algorithmName in AlgoritmName.Symbols)
                {
                    _calc.MyHashRate = HashPower.GetAlgorithmHashRate(algorithmName);
                    double btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zerg, true, _fiat);
                    double btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zerg, false, _fiat);
                    if (btc24HoursPerDay > _keepMoreThan || btcCurrentPerDay > _keepMoreThan)
                    {
                        AlgorithmResult algorAtZerg = new AlgorithmResult();
                        algorAtZerg.name = algorithmName;
                        algorAtZerg.Pool = PoolName.Zerg;
                        algorAtZerg.estimate_current = btcCurrentPerDay;
                        algorAtZerg.estimate_last24h = btc24HoursPerDay;
                        algorResult.Add(algorAtZerg);
                    }

                    btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.PhiPhi, true, _fiat);
                    btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.PhiPhi, false, _fiat);
                    if (btc24HoursPerDay > _keepMoreThan || btcCurrentPerDay > _keepMoreThan)
                    {
                        AlgorithmResult algorAtPhi = new AlgorithmResult();
                        algorAtPhi.name = algorithmName;
                        algorAtPhi.Pool = PoolName.PhiPhi;
                        algorAtPhi.estimate_current = btcCurrentPerDay;
                        algorAtPhi.estimate_last24h = btc24HoursPerDay;
                        algorResult.Add(algorAtPhi);
                    }

                    btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.AhashPool, true, _fiat);
                    btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.AhashPool, false, _fiat);
                    if (btc24HoursPerDay > _keepMoreThan || btcCurrentPerDay > _keepMoreThan)
                    {
                        AlgorithmResult algorAtAhash = new AlgorithmResult();
                        algorAtAhash.name = algorithmName;
                        algorAtAhash.Pool = PoolName.AhashPool;
                        algorAtAhash.estimate_current = btcCurrentPerDay;
                        algorAtAhash.estimate_last24h = btc24HoursPerDay;
                        algorResult.Add(algorAtAhash);
                    }

                    btcCurrentPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zpool, true, _fiat);
                    btc24HoursPerDay = _calc.GetTotalFiatMoneyMiningPerday(algorithmName, PoolName.Zpool, false, _fiat);
                    if (btc24HoursPerDay > _keepMoreThan || btcCurrentPerDay > _keepMoreThan)
                    {
                        AlgorithmResult algorAtZpool = new AlgorithmResult();
                        algorAtZpool.name = algorithmName;
                        algorAtZpool.Pool = PoolName.Zpool;
                        algorAtZpool.estimate_current = btcCurrentPerDay;
                        algorAtZpool.estimate_last24h = btc24HoursPerDay;
                        algorResult.Add(algorAtZpool);
                    }

                }

                algorResult.Sort();
                foreach (AlgorithmResult algor in algorResult)
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
            Console.WriteLine("-c         \tcalculate fiat currency revenue to usd or baht. example -c usd or -c baht");
            Console.WriteLine("-debug     \tshow debug message.");
        }

        private static void ShowNumOfCoinMiningPerDay(string coinSymbol, PoolName poolName)
        {
            double coinsPerDay = _calc.GetNumOfCoinMiningPerDay(coinSymbol, poolName);
            Console.WriteLine(string.Format("mining@{0} will receive {1} {2} per day.", poolName.ToString(), coinsPerDay, coinSymbol));
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
