using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class CoinExchangeAPI 
    {
        public CoinExchangeAPI()
        {
            try
            {
                List<CoinExchangeMarket> assets = LoadMarket();
                CoinExchangeAsset.Add(assets);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Error: CoinExchangeAPI constructor detail ==> " + err.Message);
            }
        }
        public List<CoinExchangeMarket> LoadMarket()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("https://www.coinexchange.io");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("api/v1/getmarkets").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        // replace header and footer
                        int startIndex = res.IndexOf('[');
                        int endIndex = res.IndexOf(']');
                        res = res.Substring(startIndex, endIndex - startIndex + 1);
                        List<CoinExchangeMarket> assets = JsonConvert.DeserializeObject<List<CoinExchangeMarket>>(res);
                        return assets;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Can't load asset from coinexchange.io exchange.", err);
            }
        }

        public List<CoinExchangeCurrency> LoadPrice()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("https://www.coinexchange.io");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("/api/v1/getmarketsummaries").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        // replace header and footer
                        int startIndex = res.IndexOf('[');
                        int endIndex = res.IndexOf(']');
                        res = res.Substring(startIndex, endIndex - startIndex + 1);
                        List<CoinExchangeCurrency> priceList = JsonConvert.DeserializeObject<List<CoinExchangeCurrency>>(res);
                        return priceList;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Can't load coin price from coinexchange.io exchange.", err);
            }
        }


    }
}
