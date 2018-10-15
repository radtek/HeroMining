using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMining.ApplicationCore.Pool
{
    public class IceMiningAPI
    {
        /// <summary>
        /// Load coin mining data from bsod pool
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Get bsod data from bsod web api failed.</exception>
        public CryptoCurrency LoadCurrency()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("https://icemining.ca");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("/api/currencies").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        res = res.Replace("24h_blocks", "h24_blocks").Replace("24h_btc", "h24_btc");
                        CryptoCurrency coins = JsonConvert.DeserializeObject<CryptoCurrency>(res);
                        return coins;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Get data from bsod pool web api failed.", err);
            }
        }
    }
}
