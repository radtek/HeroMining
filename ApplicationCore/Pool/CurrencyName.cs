using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore.Pool
{
    public static class CurrencyName
    {
        private static List<string> _coins = new List<string>();

        static CurrencyName()
        {
            _coins.Add("AEX");
            _coins.Add("AKN");
            _coins.Add("ALPS");
            _coins.Add("AOP");
            _coins.Add("ARGO");
            _coins.Add("AXS");
            _coins.Add("AZART");
            _coins.Add("BTNX");
            _coins.Add("BTX");
            _coins.Add("BZL");
            _coins.Add("CBS");
            _coins.Add("CPR");
            _coins.Add("CRC");
            _coins.Add("CREB");
            _coins.Add("CRS");
            _coins.Add("DIN");
            _coins.Add("DNR");
            _coins.Add("ELLI");
            _coins.Add("EXPO");
            _coins.Add("FLM");
            _coins.Add("FNO");
            _coins.Add("FRM");
            _coins.Add("FxTC");
            _coins.Add("FxTCscrypt");
            _coins.Add("FxTCsha256d");
            _coins.Add("FxTCx11");
            _coins.Add("FxTCx16r");
            _coins.Add("GBX");
            _coins.Add("GIN");
            _coins.Add("GIO");
            _coins.Add("GRV");
            _coins.Add("GTM");
            _coins.Add("HASH");
            _coins.Add("HLX");
            _coins.Add("HOLD");
            _coins.Add("HTH");
            _coins.Add("IFX");
            _coins.Add("IQ");
            _coins.Add("JADE");
            _coins.Add("JLG");
            _coins.Add("LEX");
            _coins.Add("LINC");
            _coins.Add("LUX");
            _coins.Add("MANO");
            _coins.Add("MCT");
            _coins.Add("MERI");
            _coins.Add("MOG");
            _coins.Add("MONA");
            _coins.Add("NNC");
            _coins.Add("ORE");
            _coins.Add("PAC");
            _coins.Add("PGN");
            _coins.Add("QBIC");
            _coins.Add("QUANS");
            _coins.Add("QUAR");
            _coins.Add("RAP");
            _coins.Add("REDN");
            _coins.Add("RESQ");
            _coins.Add("RESS");
            _coins.Add("RVN");
            _coins.Add("SCRIV");
            _coins.Add("SECI");
            _coins.Add("SERA");
            _coins.Add("SMN");
            _coins.Add("SOV");
            _coins.Add("SPK");
            _coins.Add("SPLB");
            _coins.Add("STN");
            _coins.Add("TIMEC");
            _coins.Add("UFO");
            _coins.Add("VLM");
            _coins.Add("VTL");
            _coins.Add("XCG");
            _coins.Add("XDNA");
            _coins.Add("XGCS");
            _coins.Add("XMN");
            _coins.Add("XVG");
            _coins.Add("XZC");
            _coins.Add("ZAAP");
            _coins.Add("ZOO");
            _coins.Add("ABS");
            _coins.Add("COG");
            _coins.Add("CSG");
            _coins.Add("DRV");
            _coins.Add("ELP");
            _coins.Add("FIG");
            _coins.Add("GLYNO");
            _coins.Add("LINX");
            _coins.Add("MDEX");
            _coins.Add("PRIV");
            _coins.Add("TLM");
            _coins.Add("TRAID");
            _coins.Add("ULTRA");
            _coins.Add("USERV");
            _coins.Add("VIVO");
            _coins.Add("VTAR");
            _coins.Add("XZX");
            _coins.Sort();
        }

        public static List<string> Symbols { get { return _coins; } }
    }
}
