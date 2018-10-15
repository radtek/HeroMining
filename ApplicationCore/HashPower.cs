using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore
{
    public static class HashPower
    {
        // format key: gpu_algorithm 
        // example: 1080Ti_lyra2z
        private static Dictionary<string, HashPowerInfo> _hardwarePower = new Dictionary<string, HashPowerInfo>();
        private static Dictionary<string, int> _hardwareOnHand = new Dictionary<string, int>();

        static HashPower()
        {
            _hardwarePower.Add("1080ti_bcd", new HashPowerInfo(28500000, "H/s", 200));
            _hardwarePower.Add("1080ti_equihash", new HashPowerInfo(635, "Sol/s", 190));
            _hardwarePower.Add("1080ti_equihash192", new HashPowerInfo(30, "Sol/s", 180));
            _hardwarePower.Add("1080ti_zhash", new HashPowerInfo(57, "Sol/s", 180));
            _hardwarePower.Add("1080ti_ethash", new HashPowerInfo(50000000, "H/s", 175));
            _hardwarePower.Add("1080ti_skein", new HashPowerInfo(863500000, "H/s", 217));
            _hardwarePower.Add("1080ti_neoscrypt", new HashPowerInfo(1400000, "H/s", 210));
            _hardwarePower.Add("1080ti_xevan", new HashPowerInfo(5000000, "H/s", 200));
            _hardwarePower.Add("1080ti_lyra2z", new HashPowerInfo(3100000, "H/s", 150));
            _hardwarePower.Add("1080ti_keccak", new HashPowerInfo(1190000000, "H/s", 217));
            _hardwarePower.Add("1080ti_tribus", new HashPowerInfo(100000000, "H/s", 190));
            _hardwarePower.Add("1080ti_skunkhash", new HashPowerInfo(47500000, "H/s", 190));
            _hardwarePower.Add("1080ti_timetravel10", new HashPowerInfo(24000000, "H/s", 202));
            _hardwarePower.Add("1080ti_phi1612", new HashPowerInfo(30000000, "H/s", 202));
            _hardwarePower.Add("1080ti_lyra2v2", new HashPowerInfo(57300000, "H/s", 217));
            _hardwarePower.Add("1080ti_c11", new HashPowerInfo(27590000, "H/s", 221));
            _hardwarePower.Add("1080ti_blake2s", new HashPowerInfo(6700000000, "H/s", 224));
            _hardwarePower.Add("1080ti_x16s", new HashPowerInfo(16000000, "H/s", 220));
            _hardwarePower.Add("1080ti_x16r", new HashPowerInfo(13500000, "H/s", 176));
            _hardwarePower.Add("1080ti_x17", new HashPowerInfo(18000000, "H/s", 210));
            _hardwarePower.Add("1080ti_phi2", new HashPowerInfo(5700000, "H/s", 174));
            _hardwarePower.Add("1080ti_hex", new HashPowerInfo(13000000, "H/s", 200));
            _hardwarePower.Add("1080ti_progpow", new HashPowerInfo(21000000, "H/s", 210));
            _hardwarePower.Add("1080ti_cnheavy", new HashPowerInfo(990, "H/s", 150));
            _hardwarePower.Add("1080ti_scrypt", new HashPowerInfo(776000, "H/s", 208));
            _hardwarePower.Add("1080ti_allium", new HashPowerInfo(5600000, "H/s", 168));
            _hardwarePower.Add("1080ti_cryptonight", new HashPowerInfo(830, "H/s", 140));
            _hardwarePower.Add("1080ti_cryptonightv7", new HashPowerInfo(830, "H/s", 140));
            _hardwarePower.Add("1080ti_hmq1725", new HashPowerInfo(6900000, "H/s", 189));
            _hardwarePower.Add("1080ti_tensority", new HashPowerInfo(1680, "H/s", 175));



            _hardwarePower.Add("1070ti_bcd", new HashPowerInfo(17000000, "H/s", 150));
            _hardwarePower.Add("1070ti_equihash", new HashPowerInfo(500, "Sol/s", 105));
            _hardwarePower.Add("1070ti_equihash192", new HashPowerInfo(21, "Sol/s", 150));
            _hardwarePower.Add("1070ti_zhash", new HashPowerInfo(32, "Sol/s", 120));
            _hardwarePower.Add("1070ti_ethash", new HashPowerInfo(30500000, "H/s", 130));
            _hardwarePower.Add("1070ti_skein", new HashPowerInfo(614500000, "H/s", 141));
            _hardwarePower.Add("1070ti_neoscrypt", new HashPowerInfo(1050000, "H/s", 140));
            _hardwarePower.Add("1070ti_xevan", new HashPowerInfo(4000000, "H/s", 155));
            _hardwarePower.Add("1070ti_lyra2z", new HashPowerInfo(1900000, "H/s", 100));
            _hardwarePower.Add("1070ti_keccak", new HashPowerInfo(830000000, "H/s", 145));
            _hardwarePower.Add("1070ti_tribus", new HashPowerInfo(59000000, "H/s", 150));
            _hardwarePower.Add("1070ti_skunkhash", new HashPowerInfo(31500000, "H/s", 120));
            _hardwarePower.Add("1070ti_timetravel10", new HashPowerInfo(19000000, "H/s", 165));
            _hardwarePower.Add("1070ti_phi1612", new HashPowerInfo(21000000, "H/s", 128));
            _hardwarePower.Add("1070ti_lyra2v2", new HashPowerInfo(43000000, "H/s", 182));
            _hardwarePower.Add("1070ti_c11", new HashPowerInfo(15000000, "H/s", 120));
            _hardwarePower.Add("1070ti_blake2s", new HashPowerInfo(4580000000, "H/s", 157));
            _hardwarePower.Add("1070ti_x16s", new HashPowerInfo(11000000, "H/s", 140));
            _hardwarePower.Add("1070ti_x16r", new HashPowerInfo(11000000, "H/s", 140));
            _hardwarePower.Add("1070ti_x17", new HashPowerInfo(11000000, "H/s", 131));
            _hardwarePower.Add("1070ti_phi2", new HashPowerInfo(4200000, "H/s", 128));
            _hardwarePower.Add("1070ti_hex", new HashPowerInfo(12000000, "H/s", 170));
            _hardwarePower.Add("1070ti_progpow", new HashPowerInfo(14300000, "H/s", 140));
            _hardwarePower.Add("1070ti_cnheavy", new HashPowerInfo(830, "H/s", 100));
            _hardwarePower.Add("1070ti_scrypt", new HashPowerInfo(645000, "H/s", 141));
            _hardwarePower.Add("1070ti_allium", new HashPowerInfo(3860000, "H/s", 111));
            _hardwarePower.Add("1070ti_cryptonight", new HashPowerInfo(630, "H/s", 140));
            _hardwarePower.Add("1070ti_cryptonightv7", new HashPowerInfo(630, "H/s", 140));
            _hardwarePower.Add("1070ti_hmq1725", new HashPowerInfo(4800000, "H/s", 123));
            _hardwarePower.Add("1070ti_tensority", new HashPowerInfo(1140, "H/s", 160));


            _hardwarePower.Add("1070_bcd", new HashPowerInfo(1400000, "H/s", 120));
            _hardwarePower.Add("1070_equihash", new HashPowerInfo(430, "Sol/s", 120));
            _hardwarePower.Add("1070_equihash192", new HashPowerInfo(20, "Sol/s", 150));
            _hardwarePower.Add("1070_zhash", new HashPowerInfo(30, "Sol/s", 150));
            _hardwarePower.Add("1070_ethash", new HashPowerInfo(30000000, "H/s", 120));
            _hardwarePower.Add("1070_skein", new HashPowerInfo(492000000, "H/s", 137));
            _hardwarePower.Add("1070_neoscrypt", new HashPowerInfo(1000000, "H/s", 140));
            _hardwarePower.Add("1070_xevan", new HashPowerInfo(2880000, "H/s", 107));
            _hardwarePower.Add("1070_lyra2z", new HashPowerInfo(1800000, "H/s", 115));
            _hardwarePower.Add("1070_keccak", new HashPowerInfo(650000000, "H/s", 130));
            _hardwarePower.Add("1070_tribus", new HashPowerInfo(44000000, "H/s", 125));
            _hardwarePower.Add("1070_skunkhash", new HashPowerInfo(26500000, "H/s", 120));
            _hardwarePower.Add("1070_timetravel10", new HashPowerInfo(15000000, "H/s", 100));
            _hardwarePower.Add("1070_phi1612", new HashPowerInfo(18000000, "H/s", 154));
            _hardwarePower.Add("1070_lyra2v2", new HashPowerInfo(34000000, "H/s", 136));
            _hardwarePower.Add("1070_c11", new HashPowerInfo(15390000, "H/s", 139));
            _hardwarePower.Add("1070_blake2s", new HashPowerInfo(3640000000, "H/s", 174));
            _hardwarePower.Add("1070_x16s", new HashPowerInfo(10000000, "H/s", 150));
            _hardwarePower.Add("1070_x16r", new HashPowerInfo(10000000, "H/s", 150));
            _hardwarePower.Add("1070_x17", new HashPowerInfo(10000000, "H/s", 133));
            _hardwarePower.Add("1070_phi2", new HashPowerInfo(3300000, "H/s", 115));
            _hardwarePower.Add("1070_hex", new HashPowerInfo(8000000, "H/s", 120));
            _hardwarePower.Add("1070_progpow", new HashPowerInfo(12200000, "H/s", 130));
            _hardwarePower.Add("1070_cnheavy", new HashPowerInfo(800, "H/s", 100));
            _hardwarePower.Add("1070_scrypt", new HashPowerInfo(584000, "H/s", 96));
            _hardwarePower.Add("1070_allium", new HashPowerInfo(3060000, "H/s", 100));
            _hardwarePower.Add("1070_cryptonight", new HashPowerInfo(630, "H/s", 100));
            _hardwarePower.Add("1070_cryptonightv7", new HashPowerInfo(630, "H/s", 100));
            _hardwarePower.Add("1070_hmq1725", new HashPowerInfo(4260000, "H/s", 118));
            _hardwarePower.Add("1070_tensority", new HashPowerInfo(880, "H/s", 150));

            _hardwarePower.Add("1060_6GB_bcd", new HashPowerInfo(1200000, "H/s", 110));
            _hardwarePower.Add("1060_6GB_equihash", new HashPowerInfo(320, "Sol/s", 110));
            _hardwarePower.Add("1060_6GB_equihash192", new HashPowerInfo(12, "Sol/s", 100));
            _hardwarePower.Add("1060_6GB_zhash", new HashPowerInfo(20, "Sol/s", 90));
            _hardwarePower.Add("1060_6GB_ethash", new HashPowerInfo(22500000, "H/s", 90));
            _hardwarePower.Add("1060_6GB_skein", new HashPowerInfo(492000000, "H/s", 102));
            _hardwarePower.Add("1060_6GB_neoscrypt", new HashPowerInfo(620000, "H/s", 90));
            _hardwarePower.Add("1060_6GB_xevan", new HashPowerInfo(2906000, "H/s", 102));
            _hardwarePower.Add("1060_6GB_lyra2z", new HashPowerInfo(1700000, "H/s", 65));
            _hardwarePower.Add("1060_6GB_keccak", new HashPowerInfo(675000000, "H/s", 96));
            _hardwarePower.Add("1060_6GB_tribus", new HashPowerInfo(44000000, "H/s", 114));
            _hardwarePower.Add("1060_6GB_skunkhash", new HashPowerInfo(28000000, "H/s", 90));
            _hardwarePower.Add("1060_6GB_timetravel10", new HashPowerInfo(15000000, "H/s", 124));
            _hardwarePower.Add("1060_6GB_phi1612", new HashPowerInfo(17900000, "H/s", 98));
            _hardwarePower.Add("1060_6GB_lyra2v2", new HashPowerInfo(34500000, "H/s", 109));
            _hardwarePower.Add("1060_6GB_c11", new HashPowerInfo(13000000, "H/s", 108));
            _hardwarePower.Add("1060_6GB_blake2s", new HashPowerInfo(2570000000, "H/s", 121));
            _hardwarePower.Add("1060_6GB_x16s", new HashPowerInfo(6500000, "H/s", 100));
            _hardwarePower.Add("1060_6GB_x16r", new HashPowerInfo(6500000, "H/s", 100));
            _hardwarePower.Add("1060_6GB_x17", new HashPowerInfo(7000000, "H/s", 103));
            _hardwarePower.Add("1060_6GB_phi2", new HashPowerInfo(2400000, "H/s", 93));
            _hardwarePower.Add("1060_6GB_hex", new HashPowerInfo(6100000, "H/s", 70));
            _hardwarePower.Add("1060_6GB_progpow", new HashPowerInfo(8500000, "H/s", 95));
            _hardwarePower.Add("1060_6GB_cnheavy", new HashPowerInfo(500, "H/s", 70));
            _hardwarePower.Add("1060_6GB_scrypt", new HashPowerInfo(515000, "H/s", 86));
            _hardwarePower.Add("1060_6GB_allium", new HashPowerInfo(2475000, "H/s", 58));
            _hardwarePower.Add("1060_6GB_cryptonight", new HashPowerInfo(430, "H/s", 70));
            _hardwarePower.Add("1060_6GB_cryptonightv7", new HashPowerInfo(430, "H/s", 70));
            _hardwarePower.Add("1060_6GB_hmq1725", new HashPowerInfo(3100000, "H/s", 99));
            _hardwarePower.Add("1060_6GB_tensority", new HashPowerInfo(630, "H/s", 140));

            _hardwarePower.Add("1060_3GB_bcd", new HashPowerInfo(11000000, "H/s", 90));
            _hardwarePower.Add("1060_3GB_equihash", new HashPowerInfo(270, "Sol/s", 90));
            _hardwarePower.Add("1060_3GB_equihash192", new HashPowerInfo(9, "Sol/s", 90));
            _hardwarePower.Add("1060_3GB_zhash", new HashPowerInfo(20, "Sol/s", 90));
            _hardwarePower.Add("1060_3GB_ethash", new HashPowerInfo(19000000, "H/s", 90));
            _hardwarePower.Add("1060_3GB_skein", new HashPowerInfo(281000000, "H/s", 99));
            _hardwarePower.Add("1060_3GB_neoscrypt", new HashPowerInfo(562000, "H/s", 98));
            _hardwarePower.Add("1060_3GB_xevan", new HashPowerInfo(1750000, "H/s", 84));
            _hardwarePower.Add("1060_3GB_lyra2z", new HashPowerInfo(800000, "H/s", 60));
            _hardwarePower.Add("1060_3GB_keccak", new HashPowerInfo(410000000, "H/s", 90));
            _hardwarePower.Add("1060_3GB_tribus", new HashPowerInfo(27000000, "H/s", 93));
            _hardwarePower.Add("1060_3GB_skunkhash", new HashPowerInfo(17270000, "H/s", 95));
            _hardwarePower.Add("1060_3GB_timetravel10", new HashPowerInfo(9800000, "H/s", 107));
            _hardwarePower.Add("1060_3GB_phi1612", new HashPowerInfo(10000000, "H/s", 87));
            _hardwarePower.Add("1060_3GB_lyra2v2", new HashPowerInfo(22400000, "H/s", 85));
            _hardwarePower.Add("1060_3GB_c11", new HashPowerInfo(9550000, "H/s", 93));
            _hardwarePower.Add("1060_3GB_blake2s", new HashPowerInfo(2250000000, "H/s", 100));
            _hardwarePower.Add("1060_3GB_x16s", new HashPowerInfo(6500000, "H/s", 100));
            _hardwarePower.Add("1060_3GB_x16r", new HashPowerInfo(6500000, "H/s", 100));
            _hardwarePower.Add("1060_3GB_x17", new HashPowerInfo(6000000, "H/s", 94));
            _hardwarePower.Add("1060_3GB_phi2", new HashPowerInfo(2000000, "H/s", 82));
            _hardwarePower.Add("1060_3GB_hex", new HashPowerInfo(5100000, "H/s", 70));
            _hardwarePower.Add("1060_3GB_progpow", new HashPowerInfo(8500000, "H/s", 95));
            _hardwarePower.Add("1060_3GB_cnheavy", new HashPowerInfo(500, "H/s", 70));
            _hardwarePower.Add("1060_3GB_scrypt", new HashPowerInfo(403000, "H/s", 86));
            _hardwarePower.Add("1060_3GB_allium", new HashPowerInfo(1868000, "H/s", 90));
            _hardwarePower.Add("1060_3GB_hmq1725", new HashPowerInfo(2700000, "H/s", 93));
            _hardwarePower.Add("1060_3GB_tensority", new HashPowerInfo(590, "H/s", 130));

            _hardwarePower.Add("1050ti_bcd", new HashPowerInfo(7000000, "H/s", 70));
            _hardwarePower.Add("1050ti_equihash", new HashPowerInfo(140, "Sol/s", 65));
            _hardwarePower.Add("1050ti_equihash192", new HashPowerInfo(7, "Sol/s", 70));
            _hardwarePower.Add("1050ti_zhash", new HashPowerInfo(12, "Sol/s", 70));
            _hardwarePower.Add("1050ti_ethash", new HashPowerInfo(13.9, "H/s", 70));
            _hardwarePower.Add("1050ti_skein", new HashPowerInfo(200000000, "H/s", 58));
            _hardwarePower.Add("1050ti_neoscrypt", new HashPowerInfo(390000, "H/s", 62));
            _hardwarePower.Add("1050ti_xevan", new HashPowerInfo(1000000, "H/s", 60));
            _hardwarePower.Add("1050ti_lyra2z", new HashPowerInfo(580000, "H/s", 40));
            _hardwarePower.Add("1050ti_keccak", new HashPowerInfo(240000000, "H/s", 70));
            _hardwarePower.Add("1050ti_tribus", new HashPowerInfo(15000000, "H/s", 52));
            _hardwarePower.Add("1050ti_skunkhash", new HashPowerInfo(9900000, "H/s", 68));
            _hardwarePower.Add("1050ti_timetravel10", new HashPowerInfo(7000000, "H/s", 60));
            _hardwarePower.Add("1050ti_phi1612", new HashPowerInfo(6000000, "H/s", 67));
            _hardwarePower.Add("1050ti_lyra2v2", new HashPowerInfo(11700000, "H/s", 55));
            _hardwarePower.Add("1050ti_c11", new HashPowerInfo(6350000, "H/s", 72));
            _hardwarePower.Add("1050ti_blake2s", new HashPowerInfo(1470000000, "H/s", 68));
            _hardwarePower.Add("1050ti_x16s", new HashPowerInfo(3610000, "H/s", 70));
            _hardwarePower.Add("1050ti_x16r", new HashPowerInfo(3610000, "H/s", 70));
            _hardwarePower.Add("1050ti_x17", new HashPowerInfo(3980000, "H/s", 63));
            _hardwarePower.Add("1050ti_phi2", new HashPowerInfo(1190000, "H/s", 50));
            _hardwarePower.Add("1050ti_hex", new HashPowerInfo(2500000, "H/s", 70));
            _hardwarePower.Add("1050ti_progpow", new HashPowerInfo(5200000, "H/s", 70));
            _hardwarePower.Add("1050ti_cnheavy", new HashPowerInfo(370, "H/s", 50));
            _hardwarePower.Add("1050ti_scrypt", new HashPowerInfo(298000, "H/s", 63));
            _hardwarePower.Add("1050ti_allium", new HashPowerInfo(1165000, "H/s", 58));
            _hardwarePower.Add("1050ti_cryptonight", new HashPowerInfo(300, "H/s", 50));
            _hardwarePower.Add("1050ti_cryptonightv7", new HashPowerInfo(300, "H/s", 50));
            _hardwarePower.Add("1050ti_hmq1725", new HashPowerInfo(1700000, "H/s", 62));
            _hardwarePower.Add("1050ti_tensority", new HashPowerInfo(375, "H/s", 100));


            _hardwarePower.Add("rx_vega_64_equihash", new HashPowerInfo(450, "Sol/s", 200));
            _hardwarePower.Add("rx_vega_64_ethash", new HashPowerInfo(40000000, "H/s", 230));
            _hardwarePower.Add("rx_vega_64_x16s", new HashPowerInfo(10000000, "H/s", 194));
            _hardwarePower.Add("rx_vega_64_x16r", new HashPowerInfo(13000000, "H/s", 225));
            _hardwarePower.Add("rx_vega_64_x17", new HashPowerInfo(12000000, "H/s", 200));
            _hardwarePower.Add("rx_vega_64_progpow", new HashPowerInfo(17000000, "H/s", 190));
            _hardwarePower.Add("rx_vega_64_cnheavy", new HashPowerInfo(1600, "H/s", 160));
            _hardwarePower.Add("rx_vega_64_neoscrypt", new HashPowerInfo(2000000, "H/s", 250));
            _hardwarePower.Add("rx_vega_64_lyra2v2", new HashPowerInfo(13000000, "H/s", 200));
            _hardwarePower.Add("rx_vega_64_timetravel10", new HashPowerInfo(16500000, "H/s", 230));
            _hardwarePower.Add("rx_vega_64_lyra2z", new HashPowerInfo(7070000, "H/s", 150));
            _hardwarePower.Add("rx_vega_64_skunkhash", new HashPowerInfo(40000000, "H/s", 230));
            _hardwarePower.Add("rx_vega_64_cryptonight", new HashPowerInfo(2050, "H/s", 200));
            _hardwarePower.Add("rx_vega_64_cryptonightv7", new HashPowerInfo(2050, "H/s", 200));






            _hardwarePower.Add("rx_vega_56_equihash", new HashPowerInfo(460, "Sol/s", 314));
            _hardwarePower.Add("rx_vega_56_ethash", new HashPowerInfo(36500000, "H/s", 210));
            _hardwarePower.Add("rx_vega_56_x16s", new HashPowerInfo(11000000, "H/s", 175));
            _hardwarePower.Add("rx_vega_56_x16r", new HashPowerInfo(11000000, "H/s", 175));
            _hardwarePower.Add("rx_vega_56_x17", new HashPowerInfo(11000000, "H/s", 180));
            _hardwarePower.Add("rx_vega_56_progpow", new HashPowerInfo(15000000, "H/s", 190));
            _hardwarePower.Add("rx_vega_56_cnheavy", new HashPowerInfo(1400, "H/s", 160));
            _hardwarePower.Add("rx_vega_56_neoscrypt", new HashPowerInfo(1600000, "H/s", 230));
            _hardwarePower.Add("rx_vega_56_lyra2v2", new HashPowerInfo(13000000, "H/s", 190));
            _hardwarePower.Add("rx_vega_56_timetravel10", new HashPowerInfo(14000000, "H/s", 210));
            _hardwarePower.Add("rx_vega_56_lyra2z", new HashPowerInfo(6000000, "H/s", 150));
            _hardwarePower.Add("rx_vega_56_skunkhash", new HashPowerInfo(36000000, "H/s", 210));
            _hardwarePower.Add("rx_vega_56_cryptonight", new HashPowerInfo(1850, "H/s", 190));
            _hardwarePower.Add("rx_vega_56_cryptonightv7", new HashPowerInfo(1850, "H/s", 190));


            _hardwarePower.Add("rx_580_equihash", new HashPowerInfo(310, "Sol/s", 230));
            _hardwarePower.Add("rx_580_ethash", new HashPowerInfo(30200000, "H/s", 135));
            _hardwarePower.Add("rx_580_neoscrypt", new HashPowerInfo(1000000, "H/s", 150));
            _hardwarePower.Add("rx_580_phi1612", new HashPowerInfo(15000000, "H/s", 150));
            _hardwarePower.Add("rx_580_blake2s", new HashPowerInfo(1500000000, "H/s", 150));
            _hardwarePower.Add("rx_580_x16s", new HashPowerInfo(5500000, "H/s", 130));
            _hardwarePower.Add("rx_580_x16r", new HashPowerInfo(7250000, "H/s", 144));
            _hardwarePower.Add("rx_580_x17", new HashPowerInfo(6500000, "H/s", 137));
            _hardwarePower.Add("rx_580_progpow", new HashPowerInfo(9600000, "H/s", 140));
            _hardwarePower.Add("rx_580_cnheavy", new HashPowerInfo(900, "H/s", 105));
            _hardwarePower.Add("rx_580_lyra2v2", new HashPowerInfo(5700000, "H/s", 120));
            _hardwarePower.Add("rx_580_timetravel10", new HashPowerInfo(8200000, "H/s", 130));
            _hardwarePower.Add("rx_580_lyra2z", new HashPowerInfo(3200000, "H/s", 120));
            _hardwarePower.Add("rx_580_skunkhash", new HashPowerInfo(18500000, "H/s", 115));
            _hardwarePower.Add("rx_580_cryptonight", new HashPowerInfo(690, "H/s", 115));
            _hardwarePower.Add("rx_580_cryptonightv7", new HashPowerInfo(690, "H/s", 115));


            _hardwarePower.Add("rx_570_equihash", new HashPowerInfo(290, "Sol/s", 120));
            _hardwarePower.Add("rx_570_ethash", new HashPowerInfo(27900000, "H/s", 120));
            _hardwarePower.Add("rx_570_neoscrypt", new HashPowerInfo(490000, "H/s", 150));
            _hardwarePower.Add("rx_570_skunkhash", new HashPowerInfo(16300000, "H/s", 110));
            _hardwarePower.Add("rx_570_phi1612", new HashPowerInfo(10000000, "H/s", 150));
            _hardwarePower.Add("rx_570_blake2s", new HashPowerInfo(1200000000, "H/s", 150));
            _hardwarePower.Add("rx_570_x16s", new HashPowerInfo(4520000, "H/s", 97));
            _hardwarePower.Add("rx_570_x16r", new HashPowerInfo(4520000, "H/s", 97));
            _hardwarePower.Add("rx_570_x17", new HashPowerInfo(4200000, "H/s", 102));
            _hardwarePower.Add("rx_570_progpow", new HashPowerInfo(8100000, "H/s", 120));
            _hardwarePower.Add("rx_570_cnheavy", new HashPowerInfo(750, "H/s", 100));
            _hardwarePower.Add("rx_570_lyra2v2", new HashPowerInfo(5500000, "H/s", 110));
            _hardwarePower.Add("rx_570_timetravel10", new HashPowerInfo(8000000, "H/s", 115));
            _hardwarePower.Add("rx_570_lyra2z", new HashPowerInfo(3000000, "H/s", 120));
            _hardwarePower.Add("rx_570_cryptonight", new HashPowerInfo(700, "H/s", 110));
            _hardwarePower.Add("rx_570_cryptonightv7", new HashPowerInfo(700, "H/s", 110));

            _hardwarePower.Add("rx_480_equihash", new HashPowerInfo(290, "Sol/s", 120));
            _hardwarePower.Add("rx_480_ethash", new HashPowerInfo(29500000, "H/s", 135));
            _hardwarePower.Add("rx_480_neoscrypt", new HashPowerInfo(650000, "H/s", 150));
            _hardwarePower.Add("rx_480_skunkhash", new HashPowerInfo(18000000, "H/s", 115));
            _hardwarePower.Add("rx_480_phi1612", new HashPowerInfo(15000000, "H/s", 150));
            _hardwarePower.Add("rx_480_blake2s", new HashPowerInfo(1400000000, "H/s", 150));
            _hardwarePower.Add("rx_480_x16s", new HashPowerInfo(7000000, "H/s", 125));
            _hardwarePower.Add("rx_480_x16r", new HashPowerInfo(7000000, "H/s", 125));
            _hardwarePower.Add("rx_480_x17", new HashPowerInfo(4200000, "H/s", 102));
            _hardwarePower.Add("rx_480_progpow", new HashPowerInfo(9600000, "H/s", 140));
            _hardwarePower.Add("rx_480_cnheavy", new HashPowerInfo(900, "H/s", 105));
            _hardwarePower.Add("rx_480_lyra2v2", new HashPowerInfo(4900000, "H/s", 130));
            _hardwarePower.Add("rx_480_timetravel10", new HashPowerInfo(11000000, "H/s", 110));
            _hardwarePower.Add("rx_480_lyra2z", new HashPowerInfo(220000, "H/s", 100));
            _hardwarePower.Add("rx_480_cryptonight", new HashPowerInfo(730, "H/s", 110));
            _hardwarePower.Add("rx_480_cryptonightv7", new HashPowerInfo(730, "H/s", 110));

            _hardwarePower.Add("rx_470_equihash", new HashPowerInfo(260, "Sol/s", 110));
            _hardwarePower.Add("rx_470_ethash", new HashPowerInfo(26000000, "H/s", 120));
            _hardwarePower.Add("rx_470_neoscrypt", new HashPowerInfo(600000, "H/s", 140));
            _hardwarePower.Add("rx_470_skunkhash", new HashPowerInfo(15000000, "H/s", 105));
            _hardwarePower.Add("rx_470_phi1612", new HashPowerInfo(10000000, "H/s", 150));
            _hardwarePower.Add("rx_470_blake2s", new HashPowerInfo(1200000000, "H/s", 150));
            _hardwarePower.Add("rx_470_x16s", new HashPowerInfo(4790000, "H/s", 87));
            _hardwarePower.Add("rx_470_x16r", new HashPowerInfo(4790000, "H/s", 87));
            _hardwarePower.Add("rx_470_x17", new HashPowerInfo(4570000, "H/s", 80));
            _hardwarePower.Add("rx_470_progpow", new HashPowerInfo(8100000, "H/s", 120));
            _hardwarePower.Add("rx_470_cnheavy", new HashPowerInfo(750, "H/s", 100));
            _hardwarePower.Add("rx_470_lyra2v2", new HashPowerInfo(4400000, "H/s", 120));
            _hardwarePower.Add("rx_470_timetravel10", new HashPowerInfo(7500000, "H/s", 120));
            _hardwarePower.Add("rx_470_lyra2z", new HashPowerInfo(70000, "H/s", 100));
            _hardwarePower.Add("rx_470_cryptonight", new HashPowerInfo(660, "H/s", 100));
            _hardwarePower.Add("rx_470_cryptonightv7", new HashPowerInfo(660, "H/s", 100));
        }

        public static HashPowerInfo GetHashPowerInfo(string gpuModel, string algorithm)
        {
            string key = gpuModel + "_" + algorithm;
            if (_hardwarePower.ContainsKey(key))
                return _hardwarePower[key];
            return null;
        }

        /// <summary>
        /// Enumerate your rig.
        /// </summary>
        /// <param name="gpuChipset">GPU chipset now support 1080ti, 1070ti, 1070, 1060_6GB, 1060_3GB</param>
        /// <param name="count">GPU count</param>
        public static void SetupHardware(string gpuChipset, int count)
        {
            if (_hardwareOnHand.ContainsKey(gpuChipset))
                _hardwareOnHand[gpuChipset] = _hardwareOnHand[gpuChipset] + count;
            else
                _hardwareOnHand.Add(gpuChipset, count);
        }


        /// <summary>
        /// Enumerate your rig.
        /// </summary>
        /// <param name="gpuChipset">GPU chipset now support 1080ti, 1070ti, 1070</param>
        /// <param name="count">GPU count</param>
        public static void SetupHardware(Rig rig)
        {
            foreach (GpuInfo gpu in rig.Chipsets)
            {
                if (_hardwareOnHand.ContainsKey(gpu.Name)) // already exist then add more
                    _hardwareOnHand[gpu.Name] = _hardwareOnHand[gpu.Name] + gpu.Count;
                else
                    _hardwareOnHand.Add(gpu.Name, gpu.Count);
            }
        }


        public static double GetHashRate(string gpuChipset, string algorithm, int numOfGPU)
        {
            string key = gpuChipset + "_" + algorithm;
            HashPowerInfo powerInfo = GetHashPowerInfo(gpuChipset, algorithm);
            if (powerInfo != null)
            {
                return powerInfo.Power * numOfGPU;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Not found hashrate of gpu {0} on algorithm {1},", gpuChipset, algorithm));
                return 0;
            }
        }

        public static double GetAlgorithmHashRate(string algorithm)
        {
            if (_hardwareOnHand.Count == 0)
                throw new Exception("Hardware is empty. Please use SetupHardware method before call GetAlgorithmHashRate method.");
            double totalHashRate = 0.0D;
            foreach (KeyValuePair<string,int> hardware in _hardwareOnHand)
            {
                HashPowerInfo powerInfo = GetHashPowerInfo(hardware.Key, algorithm);
                if (powerInfo != null)
                {
                    totalHashRate += powerInfo.Power * hardware.Value;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Not found hashrate of gpu {0} on algorithm {1},", hardware.Key, algorithm));
                }
            }
            return totalHashRate;
        }


    }
}
