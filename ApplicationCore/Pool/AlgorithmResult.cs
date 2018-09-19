using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public class AlgorithmResult: AlgorithmBase, IComparable<AlgorithmBase>
    {
        public PoolName Pool { get; set; }

        public int CompareTo(AlgorithmBase other)
        {
            return other.estimate_last24h.CompareTo(this.estimate_last24h);
        }
    }
}
