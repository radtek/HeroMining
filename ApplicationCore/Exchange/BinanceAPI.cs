using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class BinanceAPI
    {
        private double _usdPerBTC = 0.0d;
        public List<BinanceCurrency> LoadPrice()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("https://api.binance.com");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("api/v1/ticker/24hr").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        List<BinanceCurrency> priceList = JsonConvert.DeserializeObject<List<BinanceCurrency>>(res);
                        foreach (ExchangeCurrency coin in priceList)
                        {
                            if (coin.symbol == "BTCUSDT")
                            {
                                _usdPerBTC = (coin.bid ?? 0);
                            }
                        }
                        return priceList;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Can't load coin price from Binance exchange.", err);
            }
        }


        public double GetUsdPerBTCPrice()
        {
           return _usdPerBTC;
        }
    }
}
