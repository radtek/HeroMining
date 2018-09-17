using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public class Algorithm
    {
        public class Aergo : AlgorithmBase
        {

        }

        public class Allium : AlgorithmBase
        {


        }

        public class Argon2dDyn : AlgorithmBase
        {

        }

        public class Balloon : AlgorithmBase
        {

        }

        public class Bitcore : AlgorithmBase
        {

        }

        public class Blake2s : AlgorithmBase
        {

        }

        public class C11 : AlgorithmBase
        {


        }

        public class Equihash : AlgorithmBase
        {


        }

        public class Equihash144 : AlgorithmBase
        {

        }

        public class Equihash192 : AlgorithmBase
        {


        }

        public class Equihash96 : AlgorithmBase
        {



        }

        public class Hex : AlgorithmBase
        {



        }

        public class Hmq1725 : AlgorithmBase
        {


        }

        public class Keccak : AlgorithmBase
        {


        }

        public class Keccakc : AlgorithmBase
        {

        }

        public class Lbry : AlgorithmBase
        {

        }

        public class Lyra2v2 : AlgorithmBase
        {

        }

        public class Lyra2z : AlgorithmBase
        {

        }

        public class M7m : AlgorithmBase
        {

        }

        public class MyrGr : AlgorithmBase
        {

        }

        public class Neoscrypt : AlgorithmBase
        {


        }

        public class Nist5 : AlgorithmBase
        {

        }

        public class Phi : AlgorithmBase
        {

        }

        public class Phi2 : AlgorithmBase
        {

        }

        public class Polytimos : AlgorithmBase
        {

        }

        public class Quark : AlgorithmBase
        {


        }

        public class Qubit : AlgorithmBase
        {


        }

        public class Scrypt : AlgorithmBase
        {


        }

        public class Sha256 : AlgorithmBase
        {


        }

        public class Skein : AlgorithmBase
        {


        }

        public class Skunk : AlgorithmBase
        {


        }

        public class Sonoa : AlgorithmBase
        {


        }

        public class Timetravel : AlgorithmBase
        {


        }

        public class Tribus : AlgorithmBase
        {


        }

        public class X11 : AlgorithmBase
        {


        }

        public class X11evo : AlgorithmBase
        {

        }

        public class X13 : AlgorithmBase
        {


        }

        public class X16r : AlgorithmBase
        {


        }

        public class X16s : AlgorithmBase
        {


        }

        public class X17 : AlgorithmBase
        {


        }

        public class Xevan : AlgorithmBase
        {

        }

        public class Yescrypt : AlgorithmBase
        {


        }

        public class YescryptR16 : AlgorithmBase
        {


        }

        public class Yespower : AlgorithmBase
        {


        }

        [JsonProperty("aergo")]
        public Aergo aergo { get; set; }

        [JsonProperty("allium")]
        public Allium allium { get; set; }

        [JsonProperty("argon2d_dyn")]
        public Argon2dDyn argon2d_dyn { get; set; }

        [JsonProperty("balloon")]
        public Balloon balloon { get; set; }

        [JsonProperty("bitcore")]
        public Bitcore bitcore { get; set; }

        [JsonProperty("blake2s")]
        public Blake2s blake2s { get; set; }

        [JsonProperty("c11")]
        public C11 c11 { get; set; }

        [JsonProperty("equihash")]
        public Equihash equihash { get; set; }

        [JsonProperty("equihash144")]
        public Equihash144 equihash144 { get; set; }

        [JsonProperty("equihash192")]
        public Equihash192 equihash192 { get; set; }

        [JsonProperty("equihash96")]
        public Equihash96 equihash96 { get; set; }

        [JsonProperty("hex")]
        public Hex hex { get; set; }

        [JsonProperty("hmq1725")]
        public Hmq1725 hmq1725 { get; set; }

        [JsonProperty("keccak")]
        public Keccak keccak { get; set; }

        [JsonProperty("keccakc")]
        public Keccakc keccakc { get; set; }

        [JsonProperty("lbry")]
        public Lbry lbry { get; set; }

        [JsonProperty("lyra2v2")]
        public Lyra2v2 lyra2v2 { get; set; }

        [JsonProperty("lyra2z")]
        public Lyra2z lyra2z { get; set; }

        [JsonProperty("m7m")]
        public M7m m7m { get; set; }

        [JsonProperty("myr_gr")]
        public MyrGr myr_gr { get; set; }

        [JsonProperty("neoscrypt")]
        public Neoscrypt neoscrypt { get; set; }

        [JsonProperty("nist5")]
        public Nist5 nist5 { get; set; }

        [JsonProperty("phi")]
        public Phi phi { get; set; }

        [JsonProperty("phi2")]
        public Phi2 phi2 { get; set; }

        [JsonProperty("polytimos")]
        public Polytimos polytimos { get; set; }

        [JsonProperty("quark")]
        public Quark quark { get; set; }

        [JsonProperty("qubit")]
        public Qubit qubit { get; set; }

        [JsonProperty("scrypt")]
        public Scrypt scrypt { get; set; }

        [JsonProperty("sha256")]
        public Sha256 sha256 { get; set; }

        [JsonProperty("skein")]
        public Skein skein { get; set; }

        [JsonProperty("skunk")]
        public Skunk skunk { get; set; }

        [JsonProperty("sonoa")]
        public Sonoa sonoa { get; set; }

        [JsonProperty("timetravel")]
        public Timetravel timetravel { get; set; }

        [JsonProperty("tribus")]
        public Tribus tribus { get; set; }

        [JsonProperty("x11")]
        public X11 x11 { get; set; }

        [JsonProperty("x11evo")]
        public X11evo x11evo { get; set; }

        [JsonProperty("x13")]
        public X13 x13 { get; set; }

        [JsonProperty("x16r")]
        public X16r x16r { get; set; }

        [JsonProperty("x16s")]
        public X16s x16s { get; set; }

        [JsonProperty("x17")]
        public X17 x17 { get; set; }

        [JsonProperty("xevan")]
        public Xevan xevan { get; set; }

        [JsonProperty("yescrypt")]
        public Yescrypt yescrypt { get; set; }

        [JsonProperty("yescryptR16")]
        public YescryptR16 yescryptR16 { get; set; }

        [JsonProperty("yespower")]
        public Yespower yespower { get; set; }


        public AlgorithmBase this[string algorithmName]
        {
            get
            {
                switch (algorithmName)
                {
                    case "aergo":
                        return aergo;
                    case "allium":
                        return allium;
                    case "argon2d_dyn":
                        return argon2d_dyn;
                    case "balloon":
                        return balloon;
                    case "bitcore":
                        return bitcore;
                    case "blake2s":
                        return blake2s;
                    case "c11":
                        return c11;
                    case "equihash":
                        return equihash;
                    case "equihash144":
                        return equihash144;
                    case "equihash192":
                        return equihash192;
                    case "equihash96":
                        return equihash96;
                    case "hex":
                        return hex;
                    case "hmq1725":
                        return hmq1725;
                    case "keccak":
                        return keccak;
                    case "keccakc":
                        return keccakc;
                    case "lbry":
                        return lbry;
                    case "lyra2v2":
                        return lyra2v2;
                    case "lyra2z":
                        return lyra2z;
                    case "m7m":
                        return m7m;
                    case "myr_gr":
                        return myr_gr;
                    case "neoscrypt":
                        return neoscrypt;
                    case "nist5":
                        return nist5;
                    case "phi":
                        return phi;
                    case "phi2":
                        return phi2;
                    case "polytimos":
                        return polytimos;
                    case "quark":
                        return quark;
                    case "qubit":
                        return qubit;
                    case "scrypt":
                        return scrypt;
                    case "sha256":
                        return sha256;
                    case "skein":
                        return skein;
                    case "skunk":
                        return skunk;
                    case "sonoa":
                        return sonoa;
                    case "timetravel":
                        return sonoa;
                    case "tribus":
                        return tribus;
                    case "x11":
                        return x11;
                    case "x11evo":
                        return x11evo;
                    case "x13":
                        return x13;
                    case "x16r":
                        return x16r;
                    case "x16s":
                        return x16s;
                    case "x17":
                        return x17;
                    case "xevan":
                        return xevan;
                    case "yescrypt":
                        return yescrypt;
                    case "yescryptr16":
                        return yescryptR16;
                    case "yespower":
                        return yespower;
                    default:
                        return null;
                }
            }
        }

    }

}
