using System;
using System.Collections.Generic;
using System.Text;
using COL.MassLib;
using COL.GlycoLib;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace COL.MultiGlycan
{
    [Serializable]
    public class ClusteredPeak : ICloneable
    {
        private List<MSPeak> _MSPeak;
        private double _StatrTime;
        private double _EndTime;
        private int _StartScan;
        private int _EndScan;
        private double _charge;
        private GlycanCompound _glycanComposition;
        private float  _Intensity;
        private float _MergedIntensity;
        public ClusteredPeak(int argScanNum)
        {
            _StartScan = argScanNum;
            _Intensity = 0.0f;
            _EndScan = argScanNum;
            _MSPeak = new List<MSPeak>();
        }

        public float Intensity
        {
            get
            {
                if (_Intensity == 0)
                {                   
                    foreach (MSPeak P in _MSPeak)
                    {
                        _Intensity = _Intensity + P.MonoIntensity;
                    }
                    _MergedIntensity = _Intensity;
                }
                return _Intensity;
            }
            set
            {
                _Intensity = value;
            }
        }
        public float MergedIntensity
        {
            get { return _MergedIntensity; }
            set { _MergedIntensity = value; }
        }
        public double StartTime
        {
            get { return _StatrTime; }
            set { _StatrTime = value; }
        }
        public double mz
        {
            get { return _MSPeak[0].MonoisotopicMZ; }
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
        public string GlycanKey
        {
            get
            {
                return _glycanComposition.NoOfHexNAc.ToString() + "-" +
                              _glycanComposition.NoOfHex.ToString() + "-" +
                              _glycanComposition.NoOfDeHex.ToString() + "-" +
                              _glycanComposition.NoOfSia.ToString();
            }
        }
        public GlycanCompound GlycanComposition
        {
            get { return _glycanComposition; }
            set { _glycanComposition = value; }
        }
        public object Clone() // ICloneable implementation
        {
            ClusteredPeak ClusPeaksClone = new ClusteredPeak(this.StartScan);
            ClusPeaksClone.Charge = this.Charge;
            ClusPeaksClone.EndScan = this.EndScan;
            ClusPeaksClone.EndTime = this.EndTime;
            ClusPeaksClone.GlycanComposition = this.GlycanComposition;
            ClusPeaksClone.Intensity = this.Intensity;
            ClusPeaksClone.MergedIntensity = this.MergedIntensity;
            ClusPeaksClone.Peaks = this.Peaks;
            ClusPeaksClone.StartTime = this.StartTime;
            return ClusPeaksClone;
        }
        public override bool Equals(object obj)
        {
            //obj is null 
            if (obj == null)
            {
                return false;
            }

            //check if we have a customer 
            if (obj is ClusteredPeak)
            {
                ClusteredPeak ClsPaek = obj as ClusteredPeak;

                if( ClsPaek._charge == this._charge &&
                     ClsPaek._EndScan == this._EndScan &&
                    ClsPaek._EndTime == this._EndTime &&
                    ClsPaek._glycanComposition == this._glycanComposition &&
                    ClsPaek._Intensity == this._Intensity &&
                    ClsPaek._MergedIntensity == this._MergedIntensity &&
                    //ClsPaek._MSPeak == this._MSPeak &&
                    ClsPaek._StartScan == this._StartScan &&
                    ClsPaek._StatrTime == this._StatrTime )
                {
                    return true;
                }

                
            }
            return false; 
        }
    }
}
