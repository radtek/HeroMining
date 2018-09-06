using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace CryptoMining.ApplicationCore
{
    [Serializable]
    public class Rig
    {
        public Rig()
        {
            Chipsets = new List<GpuInfo>(5);
        }
        public List<GpuInfo> Chipsets { get; set; }
    }
}
