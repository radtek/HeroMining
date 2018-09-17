using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public static class AlgoritmName
    {
        private static List<string> _algorithms = new List<string>();

        static AlgoritmName()
        {
            _algorithms.Add("aergo");
            _algorithms.Add("allium");
            _algorithms.Add("argon2d_dyn");
            _algorithms.Add("balloon");
            _algorithms.Add("bitcore");
            _algorithms.Add("blake2s");
            _algorithms.Add("c11");
            _algorithms.Add("equihash");
            _algorithms.Add("equihash144");
            _algorithms.Add("equihash192");
            _algorithms.Add("equihash96");
            _algorithms.Add("hex");
            _algorithms.Add("keccak");
            _algorithms.Add("keccakc");
            _algorithms.Add("lbry");
            _algorithms.Add("lyra2v2");
            _algorithms.Add("lyra2z");
            _algorithms.Add("m7m");
            _algorithms.Add("myr_gr");
            _algorithms.Add("neoscrypt");
            _algorithms.Add("nist5");
            _algorithms.Add("phi");
            _algorithms.Add("phi2");
            _algorithms.Add("polytimos");
            _algorithms.Add("quark");
            _algorithms.Add("qubit");
            _algorithms.Add("scrypt");
            _algorithms.Add("sha256");
            _algorithms.Add("skein");
            _algorithms.Add("skunk");
            _algorithms.Add("sonoa");
            _algorithms.Add("timetravel");
            _algorithms.Add("tribus");
            _algorithms.Add("x11");
            _algorithms.Add("x11evo");
            _algorithms.Add("x13");
            _algorithms.Add("x16r");
            _algorithms.Add("x16s");
            _algorithms.Add("x17");
            _algorithms.Add("xevan");
            _algorithms.Add("yescrypt");
            _algorithms.Add("yescryptR16");
            _algorithms.Add("yespower");
            _algorithms.Sort();
        }

        public static List<string> Symbols { get { return _algorithms; } }
    }
}
