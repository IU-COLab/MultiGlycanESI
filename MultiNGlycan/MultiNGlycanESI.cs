using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using COL.GlycoLib;
using COL.MassLib;
using System.Threading;
namespace COL.MultiNGlycan
{

    //Mass Spectrometry Adduct Calculator http://fiehnlab.ucdavis.edu/staff/kind/Metabolomics/MS-Adduct-Calculator/
    public class MultiNGlycanESI
    {
        private ManualResetEvent _doneEvent;
        private string _rawFile;
        private string _glycanFile;
        private List<ClusteredPeak> _cluPeaks;
        private List<ClusteredPeak> _mergeClusterResultPeaks;
        private double _massPPM;
        private double _glycanPPM;
        private double _MergeDurationMax = 5.0;
        private List<GlycanCompound> _GlycanList;
        private bool _isPermethylated;
        private bool _isReducedReducingEnd;
        private int _StartScan;
        private int _EndScan;
        private bool _IncludeNonClusterGlycan;
        private GlypID.Peaks.clsPeakProcessorParameters _peakParameter;
        private GlypID.HornTransform.clsHornTransformParameters _transformParameters;
        private bool _MergeDifferentCharge = true;
        private int _MaxCharge = 5;
        private bool _FindClusterUseList = true; 
        private string _ExportFilePath;
        private float _adductMass =0.0f;
        IRawFileReader rawReader;
        List<int> MSScanList;
        public MultiNGlycanESI(string argRawFile,int argStartScan,int argEndScan, string argGlycanList, double argMassPPM ,double argGlycanMass,double argMergeDurationMax, bool argPermenthylated, bool argReducedReducingEnd)
        {
            _rawFile = argRawFile;
            _cluPeaks = new List<ClusteredPeak>();
            _massPPM = argMassPPM;
            _glycanFile = argGlycanList;
            _isPermethylated = argPermenthylated;
            _isReducedReducingEnd = argReducedReducingEnd;
            _glycanPPM = argGlycanMass;
            _StartScan = argStartScan;
            _EndScan = argEndScan;
            _MergeDurationMax = argMergeDurationMax;
            _IncludeNonClusterGlycan = true;
            //Read Glycan list           
            ReadGlycanList();
            if (Path.GetExtension(argRawFile) == ".raw")
            {
                rawReader = new XRawReader(_rawFile);
            }
            else
            {
                rawReader = new mzXMLReader(_rawFile);
            }
        }
        //public MultiNGlycanESI(IRawFileReader argRaw, List<int> argMSScanList, List<GlycoLib.GlycanCompound> argGlycanCompound, double argMassPPM, double argGlycanMass, double argMergeDurationMin, bool argPermenthylated, bool argReducedReducingEnd, ManualResetEvent doneEvent)
        //{
        //    rawReader = argRaw;
        //    _GlycanList = argGlycanCompound;
        //    MSScanList = argMSScanList;
        //    _cluPeaks = new List<ClusteredPeak>();
        //    _massPPM = argMassPPM;
        //    _isPermethylated = argPermenthylated;
        //    _isReducedReducingEnd = argReducedReducingEnd;
        //    _glycanPPM = argGlycanMass;
        //    _MergeDurationMin = argMergeDurationMin;
        //    _IncludeNonClusterGlycan = true;
        //    _doneEvent = doneEvent;
        //}
        public List<ClusteredPeak> ClustedPeak
        {
            get { return _cluPeaks; }
        }
        public List<ClusteredPeak> MergedPeak
        {
            get { return _mergeClusterResultPeaks; }
        }
        public string ExportFilePath
        {
            set { _ExportFilePath = value; }
        }
        public int StartScan
        {
            get { return _StartScan; }
        }
        public int EndScan
        {
            get { return _EndScan; }
        }
        public int MaxGlycanCharge
        {
            set { _MaxCharge = value; }
            get { return _MaxCharge; }
        }
        public bool MergeDifferentChargeIntoOne
        {
            set { _MergeDifferentCharge = value; }
            get { return _MergeDifferentCharge; }
        }
        public bool IncludeNonClusterGlycan
        {
            get { return _IncludeNonClusterGlycan; }
            set { _IncludeNonClusterGlycan = value; }
        }
        public GlypID.Peaks.clsPeakProcessorParameters PeakProcessorParameters
        {
            get { return _peakParameter; }
            set { _peakParameter = value; }
        }
        public GlypID.HornTransform.clsHornTransformParameters TransformParameters
        {
            get { return _transformParameters; }
            set { _transformParameters = value; }
        }
        public float AdductMass
        {
            get { return _adductMass; }
            set { _adductMass = value; }
        }
        public void ProcessSingleScan(int argScanNo)
        {
             
            rawReader.SetPeakProcessorParameter( _peakParameter);
            rawReader.SetTransformParameter(_transformParameters);

            if (rawReader.GetMsLevel(argScanNo) == 1)
            {
                MSScan GMSScan = rawReader.ReadScan(argScanNo);
                List<MSPeak> deIsotopedPeaks = GMSScan.MSPeaks;
                List<float> mzList = new List<float>();
                foreach (MSPeak Peak in GMSScan.MSPeaks)
                {
                    mzList.Add(Peak.MonoisotopicMZ);
                }
                mzList.Sort();
     
                
                List<ClusteredPeak> Cluster;
                if (_FindClusterUseList)
                {
                    Cluster = FindClusterWGlycanList(deIsotopedPeaks, argScanNo, GMSScan.Time);
                    _cluPeaks.AddRange(Cluster);
                }
                else
                {
                    Cluster = FindClusterWOGlycanList(deIsotopedPeaks, argScanNo, GMSScan.Time);
                    List<MSPeak> UsedPeakList = new List<MSPeak>();

                    //ConvertGlycanListMz into MSPoint
                    List<MSPoint> MSPs = new List<MSPoint>();
                    foreach (GlycanCompound comp in _GlycanList)
                    {
                        MSPs.Add(new MSPoint(Convert.ToSingle(comp.MonoMass), 0.0f));
                    }
                    //Find Composition for each Cluster
                    foreach (ClusteredPeak cls in Cluster)
                    {
                        int Idx = MassLib.MassUtility.GetClosestMassIdx(MSPs, Convert.ToSingle(cls.ClusterMono));
                        if (GetMassPPM(_GlycanList[Idx].MonoMass, cls.ClusterMono) < _glycanPPM)
                        {
                            cls.GlycanCompostion = _GlycanList[Idx];
                        }
                        UsedPeakList.AddRange(cls.Peaks);
                        _cluPeaks.Add(cls);
                    }
                    //Find Composition for single peak
                    if (_IncludeNonClusterGlycan)
                    {
                        foreach (MSPeak peak in deIsotopedPeaks)
                        {
                            if (!UsedPeakList.Contains(peak))
                            {
                                int Idx = MassLib.MassUtility.GetClosestMassIdx(MSPs, peak.MonoMass);
                                if (GetMassPPM(_GlycanList[Idx].MonoMass, peak.MonoMass) < _glycanPPM)
                                {
                                    ClusteredPeak cls = new ClusteredPeak(argScanNo);
                                    cls.StartTime = GMSScan.Time;
                                    cls.EndTime = GMSScan.Time;
                                    cls.Charge = peak.ChargeState;
                                    cls.Peaks.Add(peak);
                                    cls.GlycanCompostion = _GlycanList[Idx];
                                    _cluPeaks.Add(cls);
                                    UsedPeakList.Add(peak);
                                }
                            }
                        }
                    }
                }
                GMSScan = null;
            }
            
        }        
        public void Process(Object threadContext)
        {   
            rawReader.SetPeakProcessorParameter(_peakParameter);
            rawReader.SetTransformParameter(_transformParameters);
            if (MSScanList == null)
            {
                MSScanList = new List<int>();
                for (int i = _StartScan; i <= _EndScan; i++)
                {
                    if (rawReader.GetMsLevel(i) == 1)
                    {
                        MSScanList.Add(i);
                    }
                }
            }
            foreach(int ScanNo in MSScanList) // (int i = _StartScan; i <= _EndScan; i++)
            {
                ProcessSingleScan(ScanNo);
            }
            _doneEvent.Set();
        }
        //public int aaFindClosedPeakIdx(double argMz)
        //{
        //    int min = 0;
        //    int max = _GlycanList.Count-1;
        //    int mid = -1;

        //    if (_GlycanList[max].MonoMass < argMz)
        //    {
        //        return max;
        //    }
        //    if (_GlycanList[min].MonoMass > argMz)
        //    {
        //        return min;
        //    }

        //    do
        //    {
        //        mid = min + (max - min) / 2;
        //        if (argMz > _GlycanList[mid].MonoMass)
        //        {
        //            min = mid + 1;
        //        }
        //        else
        //        {
        //            max = mid - 1;
        //        }
        //    } while (min < max);

        //    int StartIdx = mid - 2;
        //    if (StartIdx < 0)
        //    {
        //        StartIdx = 0;
        //    }
        //    int EndIdx = mid + 2;
        //    if (EndIdx > _GlycanList.Count-1)
        //    {
        //        EndIdx = _GlycanList.Count - 1;
        //    }
        //    int CandIdx = 0;
        //    double different = 1000.0;
        //    for (int i = StartIdx; i <= EndIdx; i++)
        //    {
        //        if (Math.Abs(argMz - _GlycanList[i].MonoMass) < different)
        //        {
        //            different = Math.Abs(argMz - _GlycanList[i].MonoMass);
        //            CandIdx = i;
        //        }
        //    }
        //    return CandIdx;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="argContainCompositionOnly">Skip cluster with no composition assigned</param>
        public void Export(bool argContainCompositionOnly, int argGlycanScanFilter)
        {
            //Merged Cluster
            StreamWriter sw = new StreamWriter(_ExportFilePath);
            sw.WriteLine("Start Time,End Time,Start Scan Num,End Scan Num,Charge,Abuntance,HexNac,Hex,deHex,Sia,Composition mono");
            foreach (ClusteredPeak cls in _mergeClusterResultPeaks)
            {
                if (argContainCompositionOnly && cls.GlycanCompostion == null)
                {
                    continue;
                }
                if (cls.EndScan - cls.StartScan < argGlycanScanFilter)
                {
                    continue;
                }
                string export = cls.StartTime + ",";

                if (cls.EndTime == 0)
                {
                    export = export + cls.StartTime + ",";
                }
                else
                {
                    export = export + cls.EndTime + ",";
                }
                export = export + cls.StartScan + ","
                                + cls.EndScan +","
                               + cls.Charge +","
                                 + cls.MergedIntensity.ToString() + ",";  
                                       
                         
                if (cls.GlycanCompostion != null)
                {
                    string Composition = cls.GlycanCompostion.NoOfHexNAc + "," + cls.GlycanCompostion.NoOfHex + "," + cls.GlycanCompostion.NoOfDeHex + "," + cls.GlycanCompostion.NoOfSia;
                    export = export + Composition + "," + cls.GlycanCompostion.MonoMass ;
                }
                else
                {
                    export = export + ",-,-";
                }

                sw.WriteLine(export);
            }
            sw.Flush();
            sw.Close();

            //Full Cluster
            string FullFilename = _ExportFilePath.Replace(Path.GetFileNameWithoutExtension(_ExportFilePath), Path.GetFileNameWithoutExtension(_ExportFilePath) + "_FullList");
            sw = new StreamWriter(FullFilename);
            sw.WriteLine("Time,Scan Num,Abuntance,1st,2nd,3rd,4th,5th,HexNac,Hex,deHex,Sia,Composition mono");
            foreach (ClusteredPeak cls in _cluPeaks)
            {
                if (argContainCompositionOnly && cls.GlycanCompostion == null)
                {
                    continue;
                }
                string export = cls.StartTime + ","
                                + cls.StartScan + ",";

                export = export + cls.Intensity.ToString() + ",";
                for (int i = 0; i < cls.Peaks.Count; i++)
                {
                    export = export + cls.Peaks[i].MonoisotopicMZ + ",";
                }
                for (int i = 0; i < 5 - cls.Peaks.Count-1; i++)
                {
                    export = export + ",";
                }

                if (cls.GlycanCompostion != null)
                {
                    string Composition = cls.GlycanCompostion.NoOfHexNAc + " ," + cls.GlycanCompostion.NoOfHex + "," + cls.GlycanCompostion.NoOfDeHex + "," + cls.GlycanCompostion.NoOfSia;
                    export = export + "," + Composition + "," + cls.GlycanCompostion.MonoMass;
                }
                else
                {
                    export = export + ",-,-";
                }

                sw.WriteLine(export);
            }
            sw.Flush();
            sw.Close();
        }
  
        private List<ClusteredPeak> FindClusterWGlycanList(List<MSPeak> argPeaks, int argScanNum, double argTime)
        {
            List<ClusteredPeak> ClsPeaks = new List<ClusteredPeak>();
            List<MSPeak> SortedPeaks = argPeaks;
            SortedPeaks.Sort(delegate(MSPeak P1, MSPeak P2) { return Comparer<double>.Default.Compare(P1.MonoisotopicMZ, P2.MonoisotopicMZ); });
            List<float> PeakMZ = new List<float>();
            foreach (MSPeak p in SortedPeaks)
            {
                PeakMZ.Add(p.MonoisotopicMZ);
            }
            foreach (GlycanCompound comp in _GlycanList)
            {
                float[] GlycanMZ = new float[_MaxCharge+1]; // GlycanMZ[1] = charge 1; GlycanMZ[2] = charge 2
                for (int i = 1; i <= _MaxCharge; i++)
                {
                    GlycanMZ[i] = (float)(comp.MonoMass + MassLib.Atoms.ProtonMass * i) / (float)i;
                }
                for (int i = 1; i <= _MaxCharge; i++)
                {
                    int ClosedPeak = MassLib.MassUtility.GetClosestMassIdx(PeakMZ, GlycanMZ[i]);
                    int ChargeState = Convert.ToInt32(SortedPeaks[ClosedPeak].ChargeState);
                    if (ChargeState==0 ||  ChargeState!=i||
                        GetMassPPM(SortedPeaks[ClosedPeak].MonoisotopicMZ,GlycanMZ[i])> _glycanPPM )
                    {
                        continue;
                    }
                    else
                    {
                        float[] Step = new float[ChargeState + 1];
                        float NH3 = MassLib.Atoms.NitrogenMass + 3 * MassLib.Atoms.HydrogenMass;
                        Step[0] = SortedPeaks[ClosedPeak].MonoisotopicMZ;
                        for (int j = 1; j <= ChargeState; j++)
                        {
                            Step[j] = Step[j - 1] + (NH3) / ChargeState;
                        }
                        int[] PeakIdx = new int[Step.Length];
                        PeakIdx[0] = ClosedPeak;
                        for (int j = 1; j < PeakIdx.Length; j++)
                        {
                            PeakIdx[j] = -1;
                        }
                        int MatchCount = 1;
                        for (int j = 1; j < PeakIdx.Length; j++)
                        {
                            int ClosedPeak2 = Convert.ToInt32(MassLib.MassUtility.GetClosestMassIdx(PeakMZ, Step[j]));
                            if (GetMassPPM(PeakMZ[ClosedPeak2], Step[j]) < _massPPM)
                            {
                                PeakIdx[j] = ClosedPeak2;
                                MatchCount++;
                            }
                        }

                        ClusteredPeak Cls = new ClusteredPeak(argScanNum);
                        for (int j = 0; j < PeakIdx.Length; j++)
                        {
                            if (PeakIdx[j] != -1)
                            {
                                Cls.Peaks.Add(SortedPeaks[PeakIdx[j]]);
                            }
                        }
                        Cls.StartTime = argTime;
                        Cls.EndTime = argTime;
                        Cls.Charge = i;
                        Cls.GlycanCompostion = comp;
                        ClsPeaks.Add(Cls);
                        
                    }
                }
            }


             return ClsPeaks;
        }
        private List<ClusteredPeak> FindClusterWOGlycanList(List<MSPeak> argPeaks, int argScanNum, double argTime)
        {
            List<ClusteredPeak> ClsPeaks = new List<ClusteredPeak>();
            List<MSPeak> SortedPeaks = argPeaks;
            SortedPeaks.Sort(delegate(MSPeak P1, MSPeak P2) { return Comparer<double>.Default.Compare(P1.MonoisotopicMZ, P2.MonoisotopicMZ); });
          
            for (int i = 0; i < SortedPeaks.Count; i++)
            {                          
                /// Cluster of glycan
                /// Z = 1 [M+H]     [M+NH4]
                /// Z = 2 [M+2H]   [M+NH4+H]	    [M+2NH4]
                /// Z = 3 [M+3H]	[M+NH4+2H]	[M+2NH4+H] 	[M+3NH4]
                /// Z = 4 [M+4H]	[M+NH4+3H]	[M+2NH4+2H]	[M+3NH4+H]	[M+4NH4]
                //Create cluster interval
                double[] Step = new double[Convert.ToInt32(SortedPeaks[i].ChargeState)+1];
                //double NH3 = MassLib.Atoms.NitrogenMass + 3 * MassLib.Atoms.HydrogenMass;
                Step[0] = SortedPeaks[i].MonoisotopicMZ;
                for (int j = 1; j <= SortedPeaks[i].ChargeState; j++)
                {  
                    Step[j] = Step[j - 1] + (_adductMass) / SortedPeaks[i].ChargeState;     
                }
                int[] PeakIdx = new int[Step.Length];
                PeakIdx[0] = i;
                for (int j = 1; j < PeakIdx.Length; j++)
                {
                    PeakIdx[j] = -1;
                }
                int CurrentMatchIdx = 1;
                int MatchCount = 1;
                for (int j = i + 1; j < SortedPeaks.Count; j++)
                {
                    if (SortedPeaks[i].ChargeState != SortedPeaks[j].ChargeState)
                    {
                        continue;
                    }
                    for (int k = CurrentMatchIdx; k < Step.Length; k++)
                    {
                        if (GetMassPPM(Step[k], SortedPeaks[j].MonoisotopicMZ) < _massPPM)
                        {
                            PeakIdx[k] = j;
                            CurrentMatchIdx = k+1;
                            MatchCount++;
                            break;
                        }
                    }
                     
                }
                //Cluster status check
 
                ClusteredPeak Cls = new ClusteredPeak(argScanNum);
                for (int j = 0; j < PeakIdx.Length; j++)
                {
                    if (PeakIdx[j] != -1)
                    {
                        Cls.Peaks.Add(SortedPeaks[PeakIdx[j]]);
                    }
                }
                Cls.StartTime = argTime;
                Cls.EndTime = argTime;
                Cls.Charge = SortedPeaks[i].ChargeState;
                ClsPeaks.Add(Cls);
                
            }
            return ClsPeaks;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="argDurationMin"></param>
        /// <returns></returns>
        public void MergeCluster()
        {
            //List<ClusteredPeak> MergedClusterForAllKeys = new List<ClusteredPeak>();
            _mergeClusterResultPeaks = new List<ClusteredPeak>();
            Dictionary<string, List<ClusteredPeak>> dictAllPeak = new Dictionary<string, List<ClusteredPeak>>();
            for (int i = 0; i < _cluPeaks.Count; i++)
            {
                string key ="";
                if (_MergeDifferentCharge)
                {
                    key = _cluPeaks[i].GlycanCompostion.NoOfHexNAc.ToString() + "-" +
                                       _cluPeaks[i].GlycanCompostion.NoOfHex.ToString() + "-" +
                                       _cluPeaks[i].GlycanCompostion.NoOfDeHex.ToString() + "-" +
                                       _cluPeaks[i].GlycanCompostion.NoOfSia.ToString() ;
                }
                else
                {
                    key = _cluPeaks[i].GlycanCompostion.NoOfHexNAc.ToString() + "-" +
                                     _cluPeaks[i].GlycanCompostion.NoOfHex.ToString() + "-" +
                                     _cluPeaks[i].GlycanCompostion.NoOfDeHex.ToString() + "-" +
                                     _cluPeaks[i].GlycanCompostion.NoOfSia.ToString() + "-" +
                                     _cluPeaks[i].Charge.ToString();
                }
                if (!dictAllPeak.ContainsKey(key))
                {
                    dictAllPeak.Add(key, new List<ClusteredPeak>());
                }
                dictAllPeak[key].Add(_cluPeaks[i]);

            }

            foreach (string KEY in dictAllPeak.Keys)
            {
                List<ClusteredPeak> CLSPeaks = dictAllPeak[KEY];
                ClusteredPeak mergedPeak = null;

                //All peaks within duration
                if (CLSPeaks[CLSPeaks.Count - 1].StartTime - CLSPeaks[0].StartTime <= _MergeDurationMax)
                {
                    mergedPeak = (ClusteredPeak)CLSPeaks[0].Clone();
                    mergedPeak.EndTime = CLSPeaks[CLSPeaks.Count - 1].StartTime;
                    mergedPeak.EndScan = CLSPeaks[CLSPeaks.Count - 1].StartScan;
                    for (int j = 0; j < CLSPeaks.Count; j++)
                    {
                        mergedPeak.MergedIntensity = mergedPeak.MergedIntensity + CLSPeaks[j].Intensity;
                    }
                    _mergeClusterResultPeaks.Add(mergedPeak);
                }
                else
                {
                    for (int i = 0; i < CLSPeaks.Count; i++)
                    {
                        if (mergedPeak == null)
                        {
                            mergedPeak = (ClusteredPeak)CLSPeaks[i].Clone();
                            mergedPeak.MergedIntensity = CLSPeaks[i].Intensity;
                            continue;
                        }
                        if (CLSPeaks[i].StartTime - mergedPeak.EndTime < 1.0)
                        {
                            mergedPeak.EndTime = CLSPeaks[i].StartTime;
                            mergedPeak.EndScan = CLSPeaks[i].StartScan;
                            mergedPeak.MergedIntensity = mergedPeak.MergedIntensity + CLSPeaks[i].Intensity;
                        }
                        else //New Cluster
                        {
                            _mergeClusterResultPeaks.Add(mergedPeak);
                            mergedPeak = (ClusteredPeak)CLSPeaks[i].Clone();
                            mergedPeak.MergedIntensity = CLSPeaks[i].Intensity;
                        }
                    }
                    if (_mergeClusterResultPeaks.Count > 1 && _mergeClusterResultPeaks[_mergeClusterResultPeaks.Count - 1] != mergedPeak) //Add last Cluster into result
                    {
                        _mergeClusterResultPeaks.Add(mergedPeak);
                    }
                }
            }
            //_mergeClusterResultPeaks = MergedCluster;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="argDurationMin"></param>
        ///// <returns></returns>
        //public static List<ClusteredPeak> MergeCluster(List<ClusteredPeak> argCLU, double argDurationMin)
        //{
        //    List<ClusteredPeak> MergedCluster = new List<ClusteredPeak>();
        //    List<ClusteredPeak> _cluPeaks = argCLU;
        //    Dictionary<string, List<ClusteredPeak>> dictAllPeak = new Dictionary<string, List<ClusteredPeak>>();
        //    Dictionary<string, double> dictPeakIntensityMax = new Dictionary<string, double>();
        //    for (int i = 0; i < _cluPeaks.Count; i++)
        //    {
        //        string key = _cluPeaks[i].GlycanCompostion.NoOfHexNAc.ToString() +"-"+
        //                            _cluPeaks[i].GlycanCompostion.NoOfHex.ToString() + "-" +
        //                            _cluPeaks[i].GlycanCompostion.NoOfDeHex.ToString() + "-" +
        //                            _cluPeaks[i].GlycanCompostion.NoOfSia.ToString() + "-" +
        //                            _cluPeaks[i].Charge.ToString();
        //        if (!dictAllPeak.ContainsKey(key))
        //        {
        //            dictAllPeak.Add(key, new List<ClusteredPeak>());
        //            dictPeakIntensityMax.Add(key, _cluPeaks[i].Intensity);
        //        }
        //        dictAllPeak[key].Add(_cluPeaks[i]);
        //        if (_cluPeaks[i].Intensity > dictPeakIntensityMax[key])
        //        {
        //            dictPeakIntensityMax[key] = _cluPeaks[i].Intensity;
        //        }
        //    }

        //    foreach (string KEY in dictAllPeak.Keys)
        //    {
        //        List<ClusteredPeak> CLSPeaks = dictAllPeak[KEY];
        //        double threshold = Math.Sqrt(dictPeakIntensityMax[KEY]);
        //        ClusteredPeak mergedPeak =null;
        //        for(int i =0 ; i< CLSPeaks.Count;i++)
        //        {
        //            if (CLSPeaks[i].Intensity < threshold)
        //            {
        //                continue;
        //            }
        //            if (mergedPeak == null)
        //            {
        //                mergedPeak = (ClusteredPeak)CLSPeaks[i].Clone();
        //                mergedPeak.MergedIntensity = CLSPeaks[i].Intensity;
        //                continue;
        //            }
        //            if (CLSPeaks[i].StartTime - mergedPeak.EndTime < 1.0)
        //            {
        //                mergedPeak.EndTime = CLSPeaks[i].StartTime;
        //                mergedPeak.EndScan = CLSPeaks[i].StartScan;
        //                mergedPeak.MergedIntensity = mergedPeak.MergedIntensity + CLSPeaks[i].Intensity;
        //            }
        //            else
        //            {
        //                MergedCluster.Add(mergedPeak);
        //                mergedPeak = (ClusteredPeak)CLSPeaks[i].Clone();
        //                mergedPeak.MergedIntensity = CLSPeaks[i].Intensity;
        //            }
        //        }
        //        if (MergedCluster[MergedCluster.Count - 1] != mergedPeak)
        //        {
        //            MergedCluster.Add(mergedPeak);
        //        }
        //    } 
        //    return MergedCluster;
        //}
        public static double GetMassPPM(double argExactMass, double argMeasureMass)
        {
            return Math.Abs(Convert.ToDouble(((argMeasureMass - argExactMass) / argExactMass) * Math.Pow(10.0, 6.0)));
        }

        public void ReadGlycanList()
        {

            _GlycanList = new List<GlycanCompound>();
            StreamReader sr;
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //sr = new StreamReader(assembly.GetManifestResourceStream( "MutliNGlycanFitControls.Properties.Resources.combinations.txt"));
            int LineNumber = 0;
            sr = new StreamReader(_glycanFile);

            string tmp; // temp line for processing
            tmp = sr.ReadLine();
            LineNumber++;
            Hashtable compindex = new Hashtable(); //Glycan Type index.



            //Read the title
            string[] spilttmp = tmp.Trim().Split(',');
            try
            {
                for (int i = 0; i < spilttmp.Length; i++)
                {
                    if (spilttmp[i].ToLower() == "neunac" || spilttmp[i].ToLower() == "neungc" || spilttmp[i].ToLower() == "sialic")
                    {
                        compindex.Add("sia", i);
                        continue;
                    }
                    if (spilttmp[i].ToLower() != "hexnac" && spilttmp[i].ToLower() != "hex" && spilttmp[i].ToLower() != "dehex" && spilttmp[i].ToLower() != "sia")
                    {
                        throw new Exception("Glycan list file title error. (Use:HexNAc,Hex,DeHex,Sia,NeuNAc,NeuNGc)");
                    }
                    compindex.Add(spilttmp[i].ToLower(), i);
                }
            }
            catch (Exception ex)
            {
                sr.Close();
                throw ex;
            }
            int processed_count = 0;

            //Read the list    
            try
            {
                do
                {
                    tmp = sr.ReadLine();
                    LineNumber++;
                    spilttmp = tmp.Trim().Split(',');
                    _GlycanList.Add(new GlycanCompound(Convert.ToInt32(spilttmp[(int)compindex["hexnac"]]),
                                             Convert.ToInt32(spilttmp[(int)compindex["hex"]]),
                                             Convert.ToInt32(spilttmp[(int)compindex["dehex"]]),
                                             Convert.ToInt32(spilttmp[(int)compindex["sia"]]),
                                             _isPermethylated,
                                             false,
                                             _isReducedReducingEnd,
                                             false,
                                             true,
                                             true)
                                             );
                    processed_count++;
                } while (!sr.EndOfStream);
            }
            catch (Exception ex)
            {
                throw new Exception("Glycan list file reading error on Line:" + LineNumber + ". Please check input file. (" + ex.Message + ")");
            }
            finally
            {
                sr.Close();
            }

            if (_GlycanList.Count == 0)
            {
                throw new Exception("Glycan list file reading error. Please check input file.");
            }
            _GlycanList.Sort();
        }
    }
}
