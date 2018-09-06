using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class Crex24API 
    {
        public List<Crex24Currency> LoadPrice()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("https://api.crex24.com/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("/v2/public/tickers").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        List<Crex24Currency> priceList = JsonConvert.DeserializeObject<List<Crex24Currency>>(res);
                        return priceList;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Can't load coin price from crypto-bridge exchange.", err);
            }
        }

       
    
    }
}
