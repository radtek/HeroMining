using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoMining.ApplicationCore.Pool;
using CryptoMining.ApplicationCore.Exchange;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoMining.ApplicationCore;
using System.Diagnostics;
using System.Text;
using CryptoMining.ApplicationCore.Miner;

namespace CryptoMiningTest
{
    [TestClass]
    public class MistTest
    {
        [TestMethod]
        public void TestStringCompare()
        {
            int i = String.Compare("AAA", "aaa", true);
            Assert.AreEqual(0,i);
            i = String.Compare("bbb", "aaa", true);
            Assert.AreEqual(true, i > 0);
            i = String.Compare("aAa", "aaa", true);
            Assert.AreEqual(0, i);
        }

        [TestMethod]
        public void TestDigitCasting()
        {
            double d = 0.001;
            int x = (int)(d * 3600000);
            Assert.AreEqual(0, x > 0);
        }


        
      

    }
}