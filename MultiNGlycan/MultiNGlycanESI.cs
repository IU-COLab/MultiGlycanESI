using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using COL.GlycoLib;
using COL.MassLib;
namespace COL.MultiNGlycan
{
    class MultiNGlycanESI
    {
        private string _rawFile;
        private string _glycanFile;
        private List<ClusteredPeak> _cluPeaks;
        private List<ClusteredPeak> _mergePeaks;
        private double Proton =1.0073;
        private double Hydrogen=1.00783;
        private double Nitrogen=14.003072;
        private double _massPPM;
        private double _glycanPPM;
        private double _MergeDurationMin;
        private List<GlycanCompound> _GlycanList;
        private bool _isPermethylated;
        private bool _isReducedReducingEnd;
        private int _StartScan;
        private int _EndScan;
        private bool _IncludeNonClusterGlycan;
        private GlypID.Peaks.clsPeakProcessorParameters _peakParameter;
        private GlypID.HornTransform.clsHornTransformParameters _transformParameters;
        public MultiNGlycanESI(string argRawFile,int argStartScan,int argEndScan, string argGlycanList, double argMassPPM ,double argGlycanMass,double argMergeDurationMin, bool argPermenthylated, bool argReducedReducingEnd)
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
            _MergeDurationMin = argMergeDurationMin;
            _IncludeNonClusterGlycan = true;
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
        public void Process()
        {

           
            //Read Glycan list
            ReadGlycanList();
            XRawReader rawReader = new XRawReader(_rawFile);
            rawReader.PeakProcessorParameter = _peakParameter;
            rawReader.TransformParameter = _transformParameters;

            if (_EndScan == 99999)
            {
                _EndScan = rawReader.NumberOfScans;
            }            
            for (int i = _StartScan; i <= _EndScan; i++)
            {
                if (rawReader.GetMsLevel(i) == 1)
                {
                    MSScan GMSScan = rawReader.ReadScan(i);
                    List<MSPeak> deIsotopedPeaks = GMSScan.MSPeaks;
                    //int closedIdx =0;
                    //float distance = 9999f;
                    //for (int j = 0; j < deIsotopedPeaks.Count; j++)
                    //{
                    //    if (Math.Abs(deIsotopedPeaks[j].mz - findmz) < distance)
                    //    {
                    //        closedIdx = j;
                    //        distance = (float)Math.Abs(deIsotopedPeaks[j].mz - findmz);
                    //    }
                    //}
                    //double closemz = deIsotopedPeaks[closedIdx].mz;
                    List<ClusteredPeak> Cluster = FindCluster(deIsotopedPeaks, i, GMSScan.Time);
                    List<MSPeak> UsedPeakList = new List<MSPeak>();
                    //Find Composition for each Cluster
                    foreach (ClusteredPeak cls in Cluster)
                    {
                        int Idx = FindClosedPeakIdx(cls.ClusterMono);
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
                                int Idx = FindClosedPeakIdx(peak.MonoMass);
                                if (GetMassPPM(_GlycanList[Idx].MonoMass, peak.MonoMass) < _glycanPPM)
                                {
                                    ClusteredPeak cls = new ClusteredPeak(i);
                                    cls.StartTime = GMSScan.Time;
                                    cls.Charge = peak.ChargeState;
                                    cls.Peaks.Add(peak);
                                    cls.GlycanCompostion = _GlycanList[Idx];
                                    _cluPeaks.Add(cls);
                                    UsedPeakList.Add(peak);
                                }
                            }
                        }
                    }

                    
                    


                    GMSScan = null;
                }
            }

    
            //Merge Cluster
            _mergePeaks = MergeCluster(_MergeDurationMin);
        }
        public int FindClosedPeakIdx(double argMz)
        {
            int min = 0;
            int max = _GlycanList.Count-1;
            int mid = -1;

            if (_GlycanList[max].MonoMass < argMz)
            {
                return max;
            }
            if (_GlycanList[min].MonoMass > argMz)
            {
                return min;
            }

            do
            {
                mid = min + (max - min) / 2;
                if (argMz > _GlycanList[mid].MonoMass)
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid - 1;
                }
            } while (min < max);

            int StartIdx = mid - 2;
            if (StartIdx < 0)
            {
                StartIdx = 0;
            }
            int EndIdx = mid + 2;
            if (EndIdx > _GlycanList.Count-1)
            {
                EndIdx = _GlycanList.Count - 1;
            }
            int CandIdx = 0;
            double different = 1000.0;
            for (int i = StartIdx; i <= EndIdx; i++)
            {
                if (Math.Abs(argMz - _GlycanList[i].MonoMass) < different)
                {
                    different = Math.Abs(argMz - _GlycanList[i].MonoMass);
                    CandIdx = i;
                }
            }
            return CandIdx;
        }

        public void Export(string argFilename, bool argContainCompsitionOnly)
        {
            //Merged Cluster
            StreamWriter sw = new StreamWriter(argFilename);
            sw.WriteLine("Start Time,End Time,Start Scan Num,End Scan Num,Charge,M/Z,Abuntance,MonoMass,1st,2nd,3rd,4th,5th,HexNac,Hex,deHex,Sia,Composition mono,Difference PPM");
            foreach (ClusteredPeak cls in _mergePeaks)
            {
                if (argContainCompsitionOnly && cls.GlycanCompostion == null)
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
                               + cls.Charge + ","
                               + cls.Peaks[0].MZ + ",";

                export = export + cls.Intensity.ToString() + ","
                                + cls.ClusterMono.ToString() + ",";
                for (int i = 0; i < cls.Peaks.Count; i++)
                {
                    export = export + cls.Peaks[i].MZ + ",";
                }
                for (int i = 0; i < 5 - cls.Peaks.Count-1; i++)
                {
                    export = export + ",";
                }
                            
                if (cls.GlycanCompostion != null)
                {
                    string Composition = cls.GlycanCompostion.NoOfHexNAc + " ," + cls.GlycanCompostion.NoOfHex + "," + cls.GlycanCompostion.NoOfDeHex + "," + cls.GlycanCompostion.NoOfSia;
                    export = export + "," + Composition + "," + cls.GlycanCompostion.MonoMass + "," + GetMassPPM(cls.GlycanCompostion.MonoMass,cls.ClusterMono).ToString("00.0000");
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
            string FullFilename = argFilename.Replace(Path.GetFileNameWithoutExtension(argFilename), Path.GetFileNameWithoutExtension(argFilename) + "_FullList");
            sw = new StreamWriter(FullFilename);
            sw.WriteLine("Time,Scan Num,Charge,M/Z,Abuntance,MonoMass,1st,2nd,3rd,4th,5th,HexNac,Hex,deHex,Sia,Composition mono,Difference PPM");
            foreach (ClusteredPeak cls in _cluPeaks)
            {
                if (argContainCompsitionOnly && cls.GlycanCompostion == null)
                {
                    continue;
                }
                string export = cls.StartTime + ","
                                + cls.StartScan + ","
                               + cls.Charge + ","
                               + cls.Peaks[0].MZ + ",";

                export = export + cls.Intensity.ToString() + ","
                                + cls.ClusterMono.ToString() + ",";
                for (int i = 0; i < cls.Peaks.Count; i++)
                {
                    export = export + cls.Peaks[i].MZ + ",";
                }
                for (int i = 0; i < 5 - cls.Peaks.Count-1; i++)
                {
                    export = export + ",";
                }

                if (cls.GlycanCompostion != null)
                {
                    string Composition = cls.GlycanCompostion.NoOfHexNAc + " ," + cls.GlycanCompostion.NoOfHex + "," + cls.GlycanCompostion.NoOfDeHex + "," + cls.GlycanCompostion.NoOfSia;
                    export = export + "," + Composition + "," + cls.GlycanCompostion.MonoMass + "," + GetMassPPM(cls.GlycanCompostion.MonoMass, cls.ClusterMono).ToString("00.0000");
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
        private List<ClusteredPeak> FindCluster(List<MSPeak> argPeaks, int argScanNum, double argTime)
        {
            List<ClusteredPeak> ClsPeaks = new List<ClusteredPeak>();
            List<MSPeak> SortedPeaks = argPeaks;
            SortedPeaks.Sort(delegate(MSPeak P1, MSPeak P2) { return Comparer<double>.Default.Compare(P1.MZ, P2.MZ); });
          
            for (int i = 0; i < SortedPeaks.Count; i++)
            {
                          
                /// Cluster of glycan
                /// Z = 1 [M+H]     [M+NH4]
                /// Z = 2 [M+2H]    [M+H+NH4]	[M+2NH4]
                /// Z = 3 [M+3H]	[M+2H+NH4]	[M+H+2NH4]	[M+3NH4]
                /// Z = 4 [M+4H]	[M+NH4+3H]	[M+2NH4+2H]	[M+3NH4+H]	[M+4NH4]
                //Create cluster interval
                double[] Step = new double[Convert.ToInt32(SortedPeaks[i].ChargeState)+1];
                double NH3 = Nitrogen + 3 * Hydrogen;
                Step[0] = SortedPeaks[i].MZ;
                for (int j = 1; j <= SortedPeaks[i].ChargeState; j++)
                {  
                    Step[j] = Step[j - 1] + (NH3) / SortedPeaks[i].ChargeState;     
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
                        if (GetMassPPM(Step[k], SortedPeaks[j].MZ) < _massPPM)
                        {
                            PeakIdx[k] = j;
                            CurrentMatchIdx = k+1;
                            MatchCount++;
                            break;
                        }
                    }
                     
                }
                //Cluster status check
                if (MatchCount >= SortedPeaks[i].ChargeState|| (MatchCount ==1))
                {
                    ClusteredPeak Cls = new ClusteredPeak(argScanNum);
                    for (int j = 0; j < PeakIdx.Length; j++)
                    {
                        if (PeakIdx[j] != -1)
                        {
                            Cls.Peaks.Add(SortedPeaks[PeakIdx[j]]);
                        }
                    }
                    Cls.StartTime = argTime;
                    Cls.Charge = SortedPeaks[i].ChargeState;
                    ClsPeaks.Add(Cls);
                }
            }
            return ClsPeaks;
        }
        public List<ClusteredPeak> MergeCluster(double argDurationMin)
        {
            List<ClusteredPeak> MergedCluster = new List<ClusteredPeak>();
            List<int> MergedIdx = new List<int>();
            for (int i = 0; i < _cluPeaks.Count; i++)
            {
                if (!MergedIdx.Contains(i))
                {
                    for (int j = i + 1; j < _cluPeaks.Count; j++)
                    {
                        if ( _cluPeaks[i].GlycanCompostion!=null
                             &&
                            ( _cluPeaks[i].GlycanCompostion == _cluPeaks[j].GlycanCompostion)
                             &&
                             (_cluPeaks[i].Charge == _cluPeaks[j].Charge)
                             &&
                            ((_cluPeaks[j].StartTime - _cluPeaks[i].StartTime) < argDurationMin))
                        {
                            _cluPeaks[i].EndTime = _cluPeaks[j].StartTime;
                            _cluPeaks[i].EndScan = _cluPeaks[j].StartScan;
                            _cluPeaks[i].Intensity = _cluPeaks[i].Intensity + _cluPeaks[j].Intensity;
                            MergedIdx.Add(j);
                        }
                    }
                    MergedCluster.Add(_cluPeaks[i]);
                    MergedIdx.Add(i);
                }
            }
            return MergedCluster;
        }
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
                sr.Close();
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
