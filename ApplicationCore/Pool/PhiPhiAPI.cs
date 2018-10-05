using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMining.ApplicationCore.Pool
{
    public class PhiPhiAPI
    {
        /// <summary>
        /// Load algorithm status from phi-phi-pool.com pool
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Get data from phi-phi-pool.com web api failed.</exception>
        public Algorithm LoadAlgorithm()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("http://pool1.phi-phi-pool.com");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("/api/status").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        res = res.Replace("argon2d-dyn", "argon2d_dyn").Replace("myr-gr", "myr_gr");
                        Algorithm algor = JsonConvert.DeserializeObject<Algorithm>(res);
                        return algor;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Get algorithm status from zergpool web api failed. [http://www.phi-phi-pool.com/api/status]", err);
            }
        }
    }
}
