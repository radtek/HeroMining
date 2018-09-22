using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class CryptopiaAPI 
    {
        public List<CryptopiaCurrency> LoadPrice()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("https://www.cryptopia.co.nz");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("/api/GetMarkets").Result;
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
                        List<CryptopiaCurrency> priceList = JsonConvert.DeserializeObject<List<CryptopiaCurrency>>(res);
                        return priceList;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Can't load coin price from cryptopia.com exchange.", err);
            }
        }

       
    
    }
}
