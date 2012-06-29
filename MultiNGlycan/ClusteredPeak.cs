﻿using System;
using System.Collections.Generic;
using System.Text;
using COL.MassLib;
using COL.GlycoLib;
namespace COL.MultiNGlycan
{
    class ClusteredPeak
    {
        private List<MSPeak> _MSPeak;
        private double _StatrTime;
        private double _EndTime;
        private int _StartScan;
        private int _EndScan;
        private double _charge;
        private GlycanCompound _glycanComposition;
        private double _Intensity;
        public ClusteredPeak(int argScanNum)
        {
            _StartScan = argScanNum;
            _Intensity = 0.0;
            _EndScan = argScanNum;
            _MSPeak = new List<MSPeak>();
        }
        public double Intensity
        {
            get
            {
                if (_Intensity == 0)
                {                   
                    foreach (MSPeak P in _MSPeak)
                    {
                        _Intensity = _Intensity + P.MonoIntensity;
                    }
                }
                return _Intensity;
            }
            set
            {
                _Intensity = value;
            }
        }
        public double StartTime
        {
            get { return _StatrTime; }
            set { _StatrTime = value; }
        }
        public double mz
        {
            get { return _MSPeak[0].MZ; }
        }

        public double EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        public double Charge
        {
            get {return _charge;}
            set {_charge = value;}
        }
        public List<MSPeak> Peaks
        {
            get { return _MSPeak; }
            set { _MSPeak = value; }
        }
        public double ClusterMono
        {
            get { return _MSPeak[0].MonoMass; }
        }
        public int StartScan
        {
            get { return _StartScan; }
        }
        public int EndScan
        {
            get { return _EndScan; }
            set { _EndScan = value; }
        }

        public GlycanCompound GlycanCompostion
        {
            get { return _glycanComposition; }
            set { _glycanComposition = value; }
        }
    }
}