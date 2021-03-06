﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMining.ApplicationCore.Pool
{
    public class ZergAPI
    {
        /// <summary>
        /// Load coin mining data from zerg pool
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Get data from zergpool web api failed.</exception>
        public CryptoCurrency LoadCurrency()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("http://api.zergpool.com:8080");
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
                throw new Exception("Get data from zergpool web api failed.", err);
            }
        }


        /// <summary>
        /// Load algorithm status from zerg pool
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Get data from zergpool web api failed.</exception>
        public Algorithm LoadAlgorithm()
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // HTTP POST
                    client.BaseAddress = new Uri("http://api.zergpool.com:8080");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("/api/status").Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        res = res.Replace("argon2d-dyn", "argon2d_dyn").Replace("myr-gr", "myr_gr").Replace("scrypt-ld", "scrypt_ld");
                        Algorithm algor = JsonConvert.DeserializeObject<Algorithm>(res);
                        return algor;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("Get algorithm status from zergpool web api failed. [http://api.zergpool.com:8080/api/status]", err);
            }
        }
    }
}
