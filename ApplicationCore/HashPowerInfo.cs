using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoMining.ApplicationCore
{
    public class HashPowerInfo
    {

        public HashPowerInfo()
        {

        }

        public HashPowerInfo(double power, string unit, double watt)
        {
            _power = power;
            _unit = unit;
            _watt = watt;
        }

        private double _power;
        private string _unit;
        private double _watt;

        public double Power
        {
            get { return _power; }
            set { _power = value; }
        }


        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }


        public double Watt
        {
            get { return _watt; }
            set { _watt = value; }
        }

    }
}
