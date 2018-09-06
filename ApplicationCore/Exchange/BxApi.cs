using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMining.ApplicationCore.Exchange
{
    public class BxAPI
    {
        public BxThbBtcOrderBook LoadThaiBahtBtcPrice()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("https://bx.in.th");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("/api/orderbook/?pairing=1").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        BxThbBtcOrderBook priceList = JsonConvert.DeserializeObject<BxThbBtcOrderBook>(res);
                        return priceList;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Can't load thai baht per btc price from bx.in.th exchange.", err);
            }
        }
    }
}
