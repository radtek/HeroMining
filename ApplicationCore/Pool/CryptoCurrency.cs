using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CryptoMining.ApplicationCore.Pool
{
    public class CryptoCurrency
    {

        public class ABS : CurrencyBase
        {

        }

        public class AEX : CurrencyBase
        {

        }

        public class AKN : CurrencyBase
        {

        }

        public class ALPS : CurrencyBase
        {

        }

        public class ANU : CurrencyBase
        {

        }

        public class AOP : CurrencyBase
        {

        }

        public class ARGO : CurrencyBase
        {

        }

        public class AXS : CurrencyBase
        {

        }

        public class AZART : CurrencyBase
        {

        }

        public class BTNX : CurrencyBase
        {

        }

        public class BTX : CurrencyBase
        {

        }

        public class BZL : CurrencyBase
        {

        }

        public class CBS : CurrencyBase
        {

        }

        public class COG : CurrencyBase
        {

        }


        public class CPR : CurrencyBase
        {

        }

        public class CRC : CurrencyBase
        {

        }

        public class FIG : CurrencyBase
        {

        }


        public class GLYNO : CurrencyBase
        {

        }


        public class LINX : CurrencyBase
        {

        }

        public class MDEX : CurrencyBase
        {

        }

        public class PRIV : CurrencyBase
        {

        }

        public class TLM : CurrencyBase
        {

        }

        public class TRAID : CurrencyBase
        {

        }

        public class ULTRA : CurrencyBase
        {

        }

        public class VIVO : CurrencyBase
        {

        }

        public class VTAR : CurrencyBase
        {

        }


        public class XZX : CurrencyBase
        {

        }
        
        public class CREB : CurrencyBase
        {

        }

        public class CRS : CurrencyBase
        {

        }

        public class CSG : CurrencyBase
        {

        }

        public class DIN : CurrencyBase
        {

        }

        public class DNR : CurrencyBase
        {

        }

        public class DRV : CurrencyBase
        {

        }

        public class DUDG : CurrencyBase
        {

        }

        public class ELLI : CurrencyBase
        {

        }

        public class EXPO : CurrencyBase
        {

        }

        public class ELP : CurrencyBase
        {

        }
        

        public class FLM : CurrencyBase
        {

        }


        public class FNO : CurrencyBase
        {

        }


        public class FRM : CurrencyBase
        {

        }

        public class FxTC : CurrencyBase
        {

        }

        public class FxTCscrypt : CurrencyBase
        {

        }

        public class FxTCsha256d : CurrencyBase
        {

        }

        public class FxTCx11 : CurrencyBase
        {

        }

        public class FxTCx16r : CurrencyBase
        {

        }

        public class GBX : CurrencyBase
        {
            public GBX()
            {
                hashRateDiscountPercent = 100;
            }
        }

        public class GIN : CurrencyBase
        {

        }

        public class GIO : CurrencyBase
        {

        }

        public class GOA : CurrencyBase
        {

        }

        public class GRV : CurrencyBase
        {

        }

        public class GTM : CurrencyBase
        {

        }

        public class HASH : CurrencyBase
        {

        }

        public class HLX : CurrencyBase
        {

        }

        public class HOLD : CurrencyBase
        {

        }

        public class HTH : CurrencyBase
        {

        }

        public class IFX : CurrencyBase
        {

        }

        public class INN : CurrencyBase
        {

        }

        public class IQ : CurrencyBase
        {

        }

        public class JADE : CurrencyBase
        {

        }

        public class JLG : CurrencyBase
        {

        }

        public class LEX : CurrencyBase
        {

        }

        public class LINC : CurrencyBase
        {

        }

        public class LUX : CurrencyBase
        {

        }

        public class MANO : CurrencyBase
        {

        }

        public class MCT : CurrencyBase
        {

        }

        public class MERI : CurrencyBase
        {

        }

        public class MOG : CurrencyBase
        {

        }

        public class MONA : CurrencyBase
        {

        }

        public class NNC : CurrencyBase
        {

        }

        public class ORE : CurrencyBase
        {

        }

        public class PAC : CurrencyBase
        {

        }

        public class PGN : CurrencyBase
        {

        }

        public class QBIC : CurrencyBase
        {

        }

        public class QUANS : CurrencyBase
        {

        }

        public class QUAR : CurrencyBase
        {

        }

        public class RAP : CurrencyBase
        {

        }

        public class REDN : CurrencyBase
        {

        }

        public class RESQ : CurrencyBase
        {

        }

        public class RESS : CurrencyBase
        {

        }

        public class RVN : CurrencyBase
        {

        }

        public class SCRIV : CurrencyBase
        {

        }

        public class SECI : CurrencyBase
        {

        }

        public class SERA : CurrencyBase
        {

        }

        public class SMN : CurrencyBase
        {

        }

        public class SNIDE : CurrencyBase
        {

        }

        public class SOV : CurrencyBase
        {

        }

        public class SPK : CurrencyBase
        {

        }

        public class SPLB : CurrencyBase
        {

        }


        public class STN : CurrencyBase
        {

        }

        public class TIMEC : CurrencyBase
        {

        }

        public class UFO : CurrencyBase
        {

        }

        public class VLM : CurrencyBase
        {

        }

        public class VTL : CurrencyBase
        {

        }

        public class XCG : CurrencyBase
        {

        }

        public class XDNA : CurrencyBase
        {

        }

        public class XGCS : CurrencyBase
        {

        }

        public class XMN : CurrencyBase
        {

        }


        public class XSHB : CurrencyBase
        {

        }

        public class XSHX16S : CurrencyBase
        {

        }

        public class XSHX17 : CurrencyBase
        {

        }

        public class XVG : CurrencyBase
        {

        }

        public class XZC : CurrencyBase
        {

        }

        public class ZAAP : CurrencyBase
        {

        }

        public class ZOO : CurrencyBase
        {

        }



        [JsonProperty("AEX")]
        public AEX Aex { get; set; }

        [JsonProperty("AKN")]
        public AKN Akn { get; set; }

        [JsonProperty("ALPS")]
        public ALPS Alps { get; set; }

        [JsonProperty("ANU")]
        public ANU Anu { get; set; }

        [JsonProperty("AOP")]
        public AOP Aop { get; set; }

        [JsonProperty("ARGO")]
        public ARGO Argo { get; set; }

        [JsonProperty("AXS")]
        public AXS Axs { get; set; }

        [JsonProperty("AZART")]
        public AZART Azart { get; set; }

        [JsonProperty("BTNX")]
        public BTNX Btnx { get; set; }

        [JsonProperty("BTX")]
        public BTX Btx { get; set; }

        [JsonProperty("BZL")]
        public BZL Bzl { get; set; }

        [JsonProperty("CBS")]
        public CBS Cbs { get; set; }

        [JsonProperty("CPR")]
        public CPR Cpr { get; set; }

        [JsonProperty("CRC")]
        public CRC Crc { get; set; }

        [JsonProperty("CREB")]
        public CREB Creb { get; set; }

        [JsonProperty("CRS")]
        public CRS Crs { get; set; }

        [JsonProperty("DIN")]
        public DIN Din { get; set; }

        [JsonProperty("DNR")]
        public DNR Dnr { get; set; }

        [JsonProperty("DRV")]
        public DRV Drv { get; set; }

        [JsonProperty("DUDG")]
        public DUDG Dudg { get; set; }

        [JsonProperty("ELLI")]
        public ELLI Elli { get; set; }

        [JsonProperty("EXPO")]
        public EXPO Expo { get; set; }

        [JsonProperty("FLM")]
        public FLM Flm { get; set; }

        [JsonProperty("FNO")]
        public FNO Fno { get; set; }

        [JsonProperty("FRM")]
        public FRM Frm { get; set; }

        [JsonProperty("FxTC")]
        public FxTC Fxtc { get; set; }

        [JsonProperty("FxTCscrypt")]
        public FxTCscrypt Fxtcscrypt { get; set; }

        [JsonProperty("FxTCsha256d")]
        public FxTCsha256d Fxtcsha256d { get; set; }

        [JsonProperty("FxTCx11")]
        public FxTCx11 Fxtcx11 { get; set; }

        [JsonProperty("FxTCx16r")]
        public FxTCx16r Fxtcx16r { get; set; }

        [JsonProperty("GBX")]
        public GBX Gbx { get; set; }

        [JsonProperty("GIN")]
        public GIN Gin { get; set; }

        [JsonProperty("GIO")]
        public GIO Gio { get; set; }

        [JsonProperty("GOA")]
        public GOA Goa { get; set; }

        [JsonProperty("GRV")]
        public GRV Grv { get; set; }

        [JsonProperty("GTM")]
        public GTM Gtm { get; set; }

        [JsonProperty("HASH")]
        public HASH Hash { get; set; }

        [JsonProperty("HLX")]
        public HLX Hlx { get; set; }

        [JsonProperty("HOLD")]
        public HOLD Hold { get; set; }

        [JsonProperty("HTH")]
        public HTH Hth { get; set; }

        [JsonProperty("IFX")]
        public IFX Ifx { get; set; }

        [JsonProperty("INN")]
        public INN Inn { get; set; }

        [JsonProperty("IQ")]
        public IQ Iq { get; set; }

        [JsonProperty("JADE")]
        public JADE Jade { get; set; }

        [JsonProperty("JLG")]
        public JLG Jlg { get; set; }

        [JsonProperty("LEX")]
        public LEX Lex { get; set; }

        [JsonProperty("LINC")]
        public LINC Linc { get; set; }

        [JsonProperty("LUX")]
        public LUX Lux { get; set; }

        [JsonProperty("MANO")]
        public MANO Mano { get; set; }

        [JsonProperty("MCT")]
        public MCT Mct { get; set; }

        [JsonProperty("MERI")]
        public MERI Meri { get; set; }

        [JsonProperty("MOG")]
        public MOG Mog { get; set; }

        [JsonProperty("MONA")]
        public MONA Mona { get; set; }

        [JsonProperty("NNC")]
        public NNC Nnc { get; set; }

        [JsonProperty("ORE")]
        public ORE Ore { get; set; }

        [JsonProperty("PAC")]
        public PAC Pac { get; set; }

        [JsonProperty("PGN")]
        public PGN Pgn { get; set; }

        [JsonProperty("QBIC")]
        public QBIC Qbic { get; set; }

        [JsonProperty("QUANS")]
        public QUANS Quans { get; set; }

        [JsonProperty("QUAR")]
        public QUAR Quar { get; set; }

        [JsonProperty("RAP")]
        public RAP Rap { get; set; }

        [JsonProperty("REDN")]
        public REDN Redn { get; set; }

        [JsonProperty("RESQ")]
        public RESQ Resq { get; set; }

        [JsonProperty("RESS")]
        public RESS Ress { get; set; }

        [JsonProperty("RVN")]
        public RVN Rvn { get; set; }

        [JsonProperty("SCRIV")]
        public SCRIV Scriv { get; set; }

        [JsonProperty("SECI")]
        public SECI Seci { get; set; }

        [JsonProperty("SERA")]
        public SERA Sera { get; set; }

        [JsonProperty("SMN")]
        public SMN Smn { get; set; }

        [JsonProperty("SNIDE")]
        public SNIDE Snide { get; set; }

        [JsonProperty("SOV")]
        public SOV Sov { get; set; }

        [JsonProperty("SPK")]
        public SPK Spk { get; set; }

        [JsonProperty("SPLB")]
        public SPLB Splb { get; set; }

        [JsonProperty("STN")]
        public STN Stn { get; set; }

        [JsonProperty("TIMEC")]
        public TIMEC Timec { get; set; }

        [JsonProperty("UFO")]
        public UFO Ufo { get; set; }

        [JsonProperty("VLM")]
        public VLM Vlm { get; set; }

        [JsonProperty("VTL")]
        public VTL Vtl { get; set; }

        [JsonProperty("XCG")]
        public XCG Xcg { get; set; }

        [JsonProperty("XDNA")]
        public XDNA Xdna { get; set; }

        [JsonProperty("XGCS")]
        public XGCS Xgcs { get; set; }

        [JsonProperty("XMN")]
        public XMN Xmn { get; set; }


        [JsonProperty("XSHB")]
        public XSHB Xshb { get; set; }

        [JsonProperty("XSHX16S")]
        public XSHX16S Xshx16s { get; set; }

        [JsonProperty("XSHX17")]
        public XSHX17 Xshx17 { get; set; }

        [JsonProperty("XVG")]
        public XVG Xvg { get; set; }

        [JsonProperty("XZC")]
        public XZC Xzc { get; set; }

        [JsonProperty("ZAAP")]
        public ZAAP Zaap { get; set; }

        [JsonProperty("ZOO")]
        public ZOO Zoo { get; set; }

        [JsonProperty("ABS")]
        public ABS Abs { get; set; }

        [JsonProperty("COG")]
        public COG Cog { get; set; }

        [JsonProperty("CSG")]
        public CSG Csg { get; set; }

        [JsonProperty("ELP")]
        public ELP Elp { get; set; }

        [JsonProperty("FIG")]
        public FIG Fig { get; set; }

        [JsonProperty("GLYNO")]
        public GLYNO Glyno { get; set; }

        [JsonProperty("Linx")]
        public LINX Linx { get; set; }

        [JsonProperty("MDEX")]
        public MDEX Mdex { get; set; }

        [JsonProperty("PRIV")]
        public PRIV Priv { get; set; }

        [JsonProperty("TLM")]
        public TLM Tlm { get; set; }

        [JsonProperty("TRAID")]
        public TRAID Traid { get; set; }

        [JsonProperty("ULTRA")]
        public ULTRA Ultra { get; set; }

        [JsonProperty("VIVO")]
        public VIVO Vivo { get; set; }

        [JsonProperty("VTAR")]
        public VTAR Vtar { get; set; }

        [JsonProperty("XZX")]
        public XZX Xzx { get; set; }


        public CurrencyBase this[string symbol]
        {
            get
            {
                switch (symbol)
                {
                    case "ABS":
                        return Abs;
                    case "COG":
                        return Cog;
                    case "CSG":
                        return Csg;
                    case "ELP":
                        return Elp;
                    case "FIG":
                        return Fig;
                    case "GLYNO":
                        return Glyno;
                    case "LINX":
                        return Linx;
                    case "MDEX":
                        return Mdex;
                    case "PRIV":
                        return Priv;
                    case "TLM":
                        return Tlm;
                    case "TRAID":
                        return Traid;
                    case "ULTRA":
                        return Ultra;
                    case "VIVO":
                        return Vivo;
                    case "VTAR":
                        return Vtar;
                    case "XZX":
                        return Xzx;
                    case "AEX":
                        return Aex;
                    case "AKN":
                        return Akn;
                    case "ALPS":
                        return Alps;
                    case "ANU":
                        return Anu;
                    case "AOP":
                        return Aop;
                    case "ARGO":
                        return Argo;
                    case "AXS":
                        return Axs;
                    case "AZART":
                        return Azart;
                    case "BTNX":
                        return Btnx;
                    case "BTX":
                        return Btx;
                    case "BZL":
                        return Bzl;
                    case "CBS":
                        return Cbs;
                    case "CPR":
                        return Cpr;
                    case "CRC":
                        return Crc;
                    case "CREB":
                        return Creb;
                    case "CRS":
                        return Crs;
                    case "DIN":
                        return Din;
                    case "DNR":
                        return Dnr;
                    case "DRV":
                        return Drv;
                    case "DUDG":
                        return Dudg;
                    case "ELLI":
                        return Elli;
                    case "EXPO":
                        return Expo;
                    case "FLM":
                        return Flm;
                    case "FNO":
                        return Fno;
                    case "FRM":
                        return Frm;
                    case "FxTC":
                        return Fxtc;
                    case "FxTCscrypt":
                        return Fxtcscrypt;
                    case "FxTCsha256d":
                        return Fxtcsha256d;
                    case "FxTCx11":
                        return Fxtcx11;
                    case "FxTCx16r":
                        return Fxtcx16r;
                    case "GBX":
                        return Gbx;
                    case "GIN":
                        return Gin;
                    case "GOA":
                        return Goa;
                    case "GIO":
                        return Gio;
                    case "GRV":
                        return Grv;
                    case "GTM":
                        return Gtm;
                    case "HASH":
                        return Hash;
                    case "HLX":
                        return Hlx;
                    case "HOLD":
                        return Hold;
                    case "HTH":
                        return Hth;
                    case "IFX":
                        return Ifx;
                    case "INN":
                        return Inn;
                    case "IQ":
                        return Iq;
                    case "JADE":
                        return Jade;
                    case "JLG":
                        return Jlg;
                    case "LEX":
                        return Lex;
                    case "LINC":
                        return Linc;
                    case "LUX":
                        return Lux;
                    case "MANO":
                        return Mano;
                    case "MCT":
                        return Mct;
                    case "MERI":
                        return Meri;
                    case "MOG":
                        return Mog;
                    case "MONA":
                        return Mona;
                    case "NNC":
                        return Nnc;
                    case "ORE":
                        return Ore;
                    case "PAC":
                        return Pac;
                    case "$PAC":
                        return Pac;
                    case "PGN":
                        return Pgn;
                    case "QBIC":
                        return Qbic;
                    case "QUANS":
                        return Quans;
                    case "QUAR":
                        return Quar;
                    case "RAP":
                        return Rap;
                    case "REDN":
                        return Redn;
                    case "RESQ":
                        return Resq;
                    case "RESS":
                        return Ress;
                    case "RVN":
                        return Rvn;
                    case "SCRIV":
                        return Scriv;
                    case "SECI":
                        return Seci;
                    case "SERA":
                        return Sera;
                    case "SMN":
                        return Smn;
                    case "SNIDE":
                        return Snide;
                    case "SOV":
                        return Sov;
                    case "SPK":
                        return Spk;
                    case "SPLB":
                        return Splb;
                    case "STN":
                        return Stn;
                    case "STONE":
                        return Stn;
                    case "TIMEC":
                        return Timec;
                    case "UFO":
                        return Ufo;
                    case "VLM":
                        return Vlm;
                    case "VTL":
                        return Vtl;
                    case "XCG":
                        return Xcg;
                    case "XDNA":
                        return Xdna;
                    case "XGCS":
                        return Xgcs;
                    case "XMN":
                        return Xmn;
                    case "XSH":
                        return Xshb;
                    case "XSHB":
                        return Xshb;
                    case "XSHX16S":
                        return Xshx16s;
                    case "XSHX17":
                        return Xshx17;
                    case "XVG":
                        return Xvg;
                    case "XZC":
                        return Xzc;
                    case "ZAAP":
                        return Zaap;
                    case "ZOO":
                        return Zoo;
                    default: return null;
                }
            }
        }

    }
}
