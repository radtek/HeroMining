using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore
{
    public class GpuInfo
    {
        public GpuInfo()
        {

        }

        public GpuInfo(string name, int count)
        {
            Name = name;
            Count = count;
        }

        /// <summary>
        /// Chipset name ex. 1080ti 1070ti 1070
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Number of chipset
        /// </summary>
        public int Count { get; set; }
    }
}
