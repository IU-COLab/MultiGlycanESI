using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using COL.GlycoLib;
using COL.MassLib;
using System.Threading;
namespace COL.MultiGlycan
{

    //Mass Spectrometry Adduct Calculator http://fiehnlab.ucdavis.edu/staff/kind/Metabolomics/MS-Adduct-Calculator/
    public class MultiGlycanESI
    {
        private ManualResetEvent _doneEvent;
        private string _rawFile;
        private string _glycanFile;
        private List<ClusteredPeak> _cluPeaks;
        private List<ClusteredPeak> _mergeClusterResultPeaks;
        private double _massPPM;
        private double _glycanPPM;
        private List<GlycanCompound> _GlycanList;
        private bool _isPermethylated;
        private bool _isReducedReducingEnd;
        private int _StartScan;
        private int _EndScan;
        private GlypID.Peaks.clsPeakProcessorParameters _peakParameter;
        private GlypID.HornTransform.clsHornTransformParameters _transformParameters;
        private bool _MergeDifferentCharge = true;
        private int _MaxCharge = 5;
        private bool _FindClusterUseList = true;
        private string _ExportFilePath;
        private List<float> _adductMass;
        private float _minLCMin = 0.05f;
        private float _maxLCMin = 8.0f;
        private double _minAbundance = 10 ^ 6;
        private int _minScanCount = 10;
        IRawFileReader rawReader;
        List<int> MSScanList;
        List<CandidatePeak> _lstCandidatePeak; //Store candidate glycan m/z
        List<float> _candidateMzList;
        Dictionary<float, List<CandidatePeak>> _dicCandidatePeak;
        List<float> GlycanMassList;
        bool DoLog = false;
        public MultiGlycanESI(string argRawFile, int argStartScan, int argEndScan, string argGlycanList, double argMassPPM, double argGlycanMass, double argMergeDurationMax, bool argPermenthylated, bool argReducedReducingEnd, bool argLog)
        {
            DoLog = argLog;
            _rawFile = argRawFile;
            _cluPeaks = new List<ClusteredPeak>();
            _massPPM = argMassPPM;
            _glycanFile = argGlycanList;
            _isPermethylated = argPermenthylated;
            _isReducedReducingEnd = argReducedReducingEnd;
            _glycanPPM = argGlycanMass;
            _StartScan = argStartScan;
            _EndScan = argEndScan;
            _adductMass = new List<float>();

            //Read Glycan list           
            if (DoLog)
            {
                Logger.WriteLog("Start Reading glycan list");
            }
            ReadGlycanList();
            if (DoLog)
            {
                Logger.WriteLog("Finish Reading glycan list");
            }
            if (Path.GetExtension(argRawFile) == ".raw")
            {
                rawReader = new XRawReader(_rawFile);
            }
            else
            {
                rawReader = new mzXMLReader(_rawFile);
            }
        }
        public int MinScanCount
        {
            set { _minScanCount = value; }
        }
        public float MaxLCMin
        {
            set { _maxLCMin = value; }
        }
        public float MinLCMin
        {
            set { _minLCMin = value; }
        }
        public double MinAbundance
        {
            set { _minAbundance = value; }
        }
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
        public List<float> AdductMass
        {
            get { return _adductMass; }
            set { _adductMass = value; }
        }
        public void ProcessSingleScan(int argScanNo)
        {
            rawReader.SetPeakProcessorParameter(_peakParameter);
            rawReader.SetTransformParameter(_transformParameters);
            if (DoLog)
            {
                Logger.WriteLog("Start process scan:" + argScanNo.ToString());
            }
            if (rawReader.GetMsLevel(argScanNo) == 1)
            {
                if (DoLog)
                {
                    Logger.WriteLog("\tStart read raw file: "+argScanNo.ToString());
                }
                MSScan GMSScan = rawReader.ReadScan(argScanNo);
                if (DoLog)
                {
                    Logger.WriteLog("\tEnd read raw file: " + argScanNo.ToString());
                }
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
                    if (_candidateMzList==null || _lstCandidatePeak == null)
                    {
                        if (DoLog)
                        {
                            Logger.WriteLog("Start generate candidate peak");
                        }
                        GenerateCandidatePeakList();
                        if (DoLog)
                        {
                            Logger.WriteLog("End generate candidate peak");
                        }
                    }
                    if (DoLog)
                    {
                        Logger.WriteLog("\tStart find cluster use default list:" + argScanNo.ToString());
                    }                   
                    Cluster = FindClusterWGlycanList(deIsotopedPeaks, argScanNo, GMSScan.Time);
                    _cluPeaks.AddRange(Cluster);
                    if (DoLog)
                    {
                        Logger.WriteLog("\tEnd find cluster use default list:" + argScanNo.ToString());
                    }
                }
                else
                {
                    if (DoLog)
                    {
                        Logger.WriteLog("\tStart find cluster without list:" + argScanNo.ToString());
                    }
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
                            cls.GlycanComposition = _GlycanList[Idx];
                        }
                        UsedPeakList.AddRange(cls.Peaks);
                        _cluPeaks.Add(cls);
                    }
                    //Find Composition for single peak
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
                                cls.GlycanComposition = _GlycanList[Idx];
                                _cluPeaks.Add(cls);
                                UsedPeakList.Add(peak);
                            }
                        }
                    }
                    if (DoLog)
                    {
                        Logger.WriteLog("\tEnd find cluster without list:" + argScanNo.ToString());
                    }
                }// Don't use glycan list;
                if (DoLog)
                {
                    Logger.WriteLog("\tEnd find cluster:" + argScanNo.ToString());
                }
                GMSScan = null;
            } //MS scan only

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
            foreach (int ScanNo in MSScanList)
            {
                ProcessSingleScan(ScanNo);
            }
            _doneEvent.Set();
        }       

        public void Export()
        {
            //Merged Cluster
            StreamWriter sw = new StreamWriter(_ExportFilePath);
            sw.WriteLine("Start Time,End Time,Start Scan Num,End Scan Num,Charge,Abuntance,HexNac-Hex-deHex-Sia,Composition mono");
            foreach (ClusteredPeak cls in _mergeClusterResultPeaks)
            {
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
                                + cls.EndScan + ","
                               + cls.Charge + ","
                                 + cls.MergedIntensity.ToString() + ",";

                if (cls.GlycanComposition != null)
                {
                    string Composition = cls.GlycanComposition.NoOfHexNAc + "-" + cls.GlycanComposition.NoOfHex + "-" + cls.GlycanComposition.NoOfDeHex + "-" + cls.GlycanComposition.NoOfSia;
                    export = export + Composition + "," + cls.GlycanComposition.MonoMass;
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
            sw.WriteLine("Time,Scan Num,Abuntance,m/z,HexNac-Hex-deHex-Sia,Composition mono");
            foreach (ClusteredPeak cls in _cluPeaks)
            {
                string export = cls.StartTime + ","
                                + cls.StartScan + ",";

                export = export + cls.Intensity.ToString() + "," + cls.Peaks[0].MonoisotopicMZ;
   

                if (cls.GlycanComposition != null)
                {
                    string Composition = cls.GlycanComposition.NoOfHexNAc + "-" + cls.GlycanComposition.NoOfHex + "-" + cls.GlycanComposition.NoOfDeHex + "-" + cls.GlycanComposition.NoOfSia;
                    export = export + "," + Composition + "," + cls.GlycanComposition.MonoMass;
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
        private void GenerateCandidatePeakList()
        {
            _lstCandidatePeak = new List<CandidatePeak>();
            _candidateMzList = new List<float>();
            _dicCandidatePeak = new Dictionary<float, List<CandidatePeak>>();
            foreach (GlycanCompound comp in _GlycanList)
            {
                for (int i = 1; i <= _MaxCharge; i++) //Charge
                {
                    foreach (float adductMass in _adductMass)
                    {
                        for (int j = 0; j <= i; j++) //Adduct Number
                        {   
                            CandidatePeak tmpCandidate = new CandidatePeak(comp, i, adductMass,j);
                            //_lstCandidatePeak.Add(tmpCandidate);
                            if (!_candidateMzList.Contains(tmpCandidate.TotalMZ))
                            {
                                _candidateMzList.Add(tmpCandidate.TotalMZ);
                            }
                            
                            if(!_dicCandidatePeak.ContainsKey(tmpCandidate.TotalMZ))
                            {
                                _dicCandidatePeak.Add(tmpCandidate.TotalMZ, new List<CandidatePeak>());
                            }
                            bool FoundSameGlycanKey = false;
                            foreach ( CandidatePeak CP in _dicCandidatePeak[tmpCandidate.TotalMZ])
                            {
                                if(CP.GlycanKey == tmpCandidate.GlycanKey)
                                {
                                    FoundSameGlycanKey = true;
                                    break;
                                }                                
                            }
                            if (!FoundSameGlycanKey)
                            {
                                ((List<CandidatePeak>)(_dicCandidatePeak[tmpCandidate.TotalMZ])).Add(tmpCandidate);
                            }
                        }
                    }                    
                }
            }            
            //_lstCandidatePeak.Sort();            
            _candidateMzList.Sort();
        }
        private List<ClusteredPeak> FindClusterWGlycanList(List<MSPeak> argPeaks, int argScanNum, double argTime)
        {
            List<ClusteredPeak> ClsPeaks = new List<ClusteredPeak>(); //Store all cluster in this scan
            List<MSPeak> SortedPeaks = argPeaks;
            SortedPeaks.Sort(delegate(MSPeak P1, MSPeak P2) { return Comparer<double>.Default.Compare(P1.MonoisotopicMZ, P2.MonoisotopicMZ); });

            foreach (MSPeak p in SortedPeaks)
            {
                //PeakMZ.Add(p.MonoisotopicMZ);
                int ClosedPeakIdx = MassLib.MassUtility.GetClosestMassIdx(_candidateMzList,p.MonoisotopicMZ);
                List<CandidatePeak> ClosedPeaks = _dicCandidatePeak[_candidateMzList[ClosedPeakIdx]];
                foreach (CandidatePeak ClosedPeak in ClosedPeaks)
                {
                    if (p.ChargeState == ClosedPeak.Charge &&
                        Math.Abs(GetMassPPM(ClosedPeak.TotalMZ, p.MonoisotopicMZ)) <= _massPPM)
                    {
                        ClusteredPeak tmpPeak = new ClusteredPeak(argScanNum);
                        tmpPeak.EndScan = argScanNum;
                        tmpPeak.StartTime = argTime;
                        tmpPeak.EndTime = argTime;
                        tmpPeak.Charge = ClosedPeak.Charge;
                        tmpPeak.GlycanComposition = ClosedPeak.GlycanComposition;
                        tmpPeak.Intensity = p.MonoIntensity;
                        tmpPeak.Peaks.Add(p);
                        ClsPeaks.Add(tmpPeak);
                    }
                }
            }
           

            //foreach (GlycanCompound comp in _GlycanList)
            //{
            //    float[] GlycanMZ = new float[_MaxCharge + 1]; // GlycanMZ[1] = charge 1; GlycanMZ[2] = charge 2
            //    for (int i = 1; i <= _MaxCharge; i++)
            //    {
            //        GlycanMZ[i] = (float)(comp.MonoMass + MassLib.Atoms.ProtonMass * i) / (float)i;
            //    }
            //    for (int i = 1; i <= _MaxCharge; i++)
            //    {
            //        int ClosedPeak = MassLib.MassUtility.GetClosestMassIdx(PeakMZ, GlycanMZ[i]);
            //        int ChargeState = Convert.ToInt32(SortedPeaks[ClosedPeak].ChargeState);
            //        if (ChargeState == 0 || ChargeState != i ||
            //            (MassLib.MassUtility.GetClosestMassIdx(PeakMZ, GlycanMZ[i]) == 0 && PeakMZ[0] - GlycanMZ[i] > 10.0f) ||
            //            (MassLib.MassUtility.GetClosestMassIdx(PeakMZ, GlycanMZ[i]) == PeakMZ.Count - 1 && GlycanMZ[i] - PeakMZ[PeakMZ.Count - 1] > 10.0f))
            //        {
            //            continue;
            //        }
            //        else
            //        {
            //            //GetMassPPM(SortedPeaks[ClosedPeak].MonoisotopicMZ,GlycanMZ[i])> _glycanPPM
            //            /// Cluster of glycan
            //            /// Z = 1 [M+H]     [M+NH4]
            //            /// Z = 2 [M+2H]   [M+NH4+H]	    [M+2NH4]
            //            /// Z = 3 [M+3H]	[M+NH4+2H]	[M+2NH4+H] 	[M+3NH4]
            //            /// Z = 4 [M+4H]	[M+NH4+3H]	[M+2NH4+2H]	[M+3NH4+H]	[M+4NH4]
            //            if (_adductMass.Count == 0)
            //            {
            //                _adductMass.Add(0.0f);
            //            }
            //            foreach (float adductMass in _adductMass)
            //            {
            //                float[] Step = new float[ChargeState + 1];
            //                //Step[0] = GlycanMZ[i];
            //                for (int j = 0; j <= ChargeState; j++)
            //                {
            //                    Step[j] = (GlycanMZ[1] + adductMass * j) / ChargeState;
            //                }
            //                int[] PeakIdx = new int[Step.Length];
            //                for (int j = 0; j < PeakIdx.Length; j++)
            //                {
            //                    PeakIdx[j] = -1;
            //                }
            //                for (int j = 0; j < PeakIdx.Length; j++)
            //                {
            //                    int ClosedPeak2 = Convert.ToInt32(MassLib.MassUtility.GetClosestMassIdx(PeakMZ, Step[j]));
            //                    if (GetMassPPM(PeakMZ[ClosedPeak2], Step[j]) < _massPPM)
            //                    {
            //                        PeakIdx[j] = ClosedPeak2;
            //                    }
            //                }
            //                ClusteredPeak Cls = new ClusteredPeak(argScanNum);
            //                for (int j = 0; j < PeakIdx.Length; j++)
            //                {
            //                    if (PeakIdx[j] != -1)
            //                    {
            //                        Cls.Peaks.Add(SortedPeaks[PeakIdx[j]]);
            //                    }
            //                }
            //                if (Cls.Peaks.Count > 0)
            //                {
            //                    Cls.StartTime = argTime;
            //                    Cls.EndTime = argTime;
            //                    Cls.Charge = i;
            //                    Cls.GlycanCompostion = comp;
            //                    Cls.AdductMass = adductMass;
            //                    if (!ClsPeaks.Contains(Cls))
            //                    {
            //                        ClsPeaks.Add(Cls);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            return ClsPeaks;
        }
        private List<ClusteredPeak> FindClusterWOGlycanList(List<MSPeak> argPeaks, int argScanNum, double argTime)
        {
            List<ClusteredPeak> ClsPeaks = new List<ClusteredPeak>();
            List<MSPeak> SortedPeaks = argPeaks;
            SortedPeaks.Sort(delegate(MSPeak P1, MSPeak P2) { return Comparer<double>.Default.Compare(P1.MonoisotopicMZ, P2.MonoisotopicMZ); });

            if (_adductMass.Count == 0)
            {
                _adductMass.Add(0.0f);
            }
            for (int i = 0; i < SortedPeaks.Count; i++)
            {
                /// Cluster of glycan
                /// Z = 1 [M+H]     [M+NH4]
                /// Z = 2 [M+2H]   [M+NH4+H]	    [M+2NH4]
                /// Z = 3 [M+3H]	[M+NH4+2H]	[M+2NH4+H] 	[M+3NH4]
                /// Z = 4 [M+4H]	[M+NH4+3H]	[M+2NH4+2H]	[M+3NH4+H]	[M+4NH4]
                //Create cluster interval
                foreach (float adductMass in _adductMass)
                {
                    double[] Step = new double[Convert.ToInt32(SortedPeaks[i].ChargeState) + 1];
                    //double NH3 = MassLib.Atoms.NitrogenMass + 3 * MassLib.Atoms.HydrogenMass;
                    Step[0] = SortedPeaks[i].MonoisotopicMZ;
                    for (int j = 1; j <= SortedPeaks[i].ChargeState; j++)
                    {
                        Step[j] = Step[j - 1] + (adductMass) / SortedPeaks[i].ChargeState;
                    }
                    int[] PeakIdx = new int[Step.Length];
                    PeakIdx[0] = i;
                    for (int j = 1; j < PeakIdx.Length; j++)
                    {
                        PeakIdx[j] = -1;
                    }
                    int CurrentMatchIdx = 1;
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
                                CurrentMatchIdx = k + 1;
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
                    if (!ClsPeaks.Contains(Cls))
                    {
                        ClsPeaks.Add(Cls);
                    }
                }
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
                string key = "";
                if (_MergeDifferentCharge)
                {
                    key = _cluPeaks[i].GlycanKey;
                }
                else
                {
                    key = _cluPeaks[i].GlycanKey + "-" +
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
                if (CLSPeaks[CLSPeaks.Count - 1].StartTime - CLSPeaks[0].StartTime <= _maxLCMin)
                {
                    mergedPeak = (ClusteredPeak)CLSPeaks[0].Clone();
                    mergedPeak.EndTime = CLSPeaks[CLSPeaks.Count - 1].StartTime;
                    mergedPeak.EndScan = CLSPeaks[CLSPeaks.Count - 1].StartScan;
                    for (int j = 0; j < CLSPeaks.Count; j++)
                    {
                        mergedPeak.MergedIntensity = mergedPeak.MergedIntensity + CLSPeaks[j].Intensity;                        
                    }

                    double timeinterval = mergedPeak.EndTime - mergedPeak.StartTime;
                    if (mergedPeak.MergedIntensity > _minAbundance &&
                        timeinterval > _minLCMin &&
                         timeinterval < _maxLCMin &&
                          CLSPeaks.Count >_minScanCount)
                    {
                        _mergeClusterResultPeaks.Add(mergedPeak);
                    }
                }
                else
                {
                    int ScanCount = 0;
                    for (int i = 0; i < CLSPeaks.Count; i++)
                    {
                        if (mergedPeak == null)
                        {
                            mergedPeak = (ClusteredPeak)CLSPeaks[i].Clone();
                            mergedPeak.MergedIntensity = CLSPeaks[i].Intensity;
                            ScanCount = 1;
                            continue;
                        }
                        if (CLSPeaks[i].StartTime - mergedPeak.EndTime < 1.0)
                        {
                            mergedPeak.EndTime = CLSPeaks[i].StartTime;
                            mergedPeak.EndScan = CLSPeaks[i].StartScan;
                            mergedPeak.MergedIntensity = mergedPeak.MergedIntensity + CLSPeaks[i].Intensity;
                            ScanCount++;
                        }
                        else //New Cluster
                        {
                            double timeinterval = mergedPeak.EndTime - mergedPeak.StartTime;
                            if (mergedPeak.MergedIntensity > _minAbundance &&
                                timeinterval > _minLCMin &&
                                timeinterval < _maxLCMin &&
                                ScanCount > _minScanCount
                                )
                            {
                                _mergeClusterResultPeaks.Add(mergedPeak);
                            }
                            mergedPeak = (ClusteredPeak)CLSPeaks[i].Clone();
                            mergedPeak.MergedIntensity = CLSPeaks[i].Intensity;
                            ScanCount = 1;
                        }
                    }
                    if (_mergeClusterResultPeaks.Count > 1 && _mergeClusterResultPeaks[_mergeClusterResultPeaks.Count - 1] != mergedPeak) //Add last Cluster into result
                    {
                        double timeinterval = mergedPeak.EndTime - mergedPeak.StartTime;
                        if (mergedPeak.MergedIntensity > _minAbundance &&
                            timeinterval > _minLCMin &&
                             timeinterval < _maxLCMin &&
                             ScanCount > _minScanCount)
                        {
                            _mergeClusterResultPeaks.Add(mergedPeak);
                        }
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
