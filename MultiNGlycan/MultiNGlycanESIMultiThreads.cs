using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.IO;
using COL.MassLib;
using COL.GlycoLib;

namespace COL.MultiNGlycan
{
    public class MultiNGlycanESIMultiThreads
    {
        private static Semaphore _pool;
        GlypID.Peaks.clsPeakProcessorParameters _peakParameter;
        GlypID.HornTransform.clsHornTransformParameters _transformParameter;
        private int _noOfThreads = 1;
        List<ClusteredPeak> ClsPeaks;
        List<List<int>> _splitDataset;
        List<GlycoLib.GlycanCompound> _ListGlycompound;
        Stack<string> rawFiles;
        string glycanFile;
       //ManualResetEvent[] doneEvents;
        MultiNGlycanESI[] _multiESIThreads;
        float _GlycanPPM = 10.0f;
        float _MassPPM = 5.0f;
        int _MaxCharge = 5;
        double _MergeDurationMin= 5;
        bool _Permenthylated =true;
        bool _ReducedReducingEnd = true;
        int StartScan = 1;
        int EndScan = 99999;
        public int NoOfThreads
        {
            set { _noOfThreads = value; }
        }
        public int MaxCharge
        {
            set { _MaxCharge = value; }
        }
        public float GlycanPPM
        {
            set { _GlycanPPM = value; }
        }
        public float MassPPM
        {
            set { _MassPPM = value; }
        }
        public double MergeDurationMin
        {
            set { _MergeDurationMin = value; }
        }
        public bool Permenthylated
        {
            set { _Permenthylated = value; }
        }
        public bool ReducedReducingEnd
        {
            set { _ReducedReducingEnd = value; }
        }
        public  MultiNGlycanESIMultiThreads(string argGlycanCompound, 
                                                                          string argRawFile, 
                                                                          int argStartScan,
                                                                          int argEndScan,
                                                                          int argNoThreads,
                                                                          float argMassPPM ,
                                                                          float argGlycanMass,
                                                                          float argMergeDurationMin, 
                                                                          bool argPermenthylated, 
                                                                          bool argReducedReducingEnd,
                                                                          GlypID.Peaks.clsPeakProcessorParameters argPeakProcessorPara,
                                                                          GlypID.HornTransform.clsHornTransformParameters argTransformPara)

        {
            glycanFile = argGlycanCompound;
            rawFiles = new Stack<string>();
            rawFiles.Push(argRawFile);
            StartScan = argStartScan;
            EndScan = argEndScan;
            _noOfThreads = argNoThreads;
            _MassPPM = argMassPPM;
            _GlycanPPM = argGlycanMass;
            _MergeDurationMin = argMergeDurationMin;
            _Permenthylated = argPermenthylated;
            _ReducedReducingEnd = argReducedReducingEnd; 
            _peakParameter = argPeakProcessorPara;
            _transformParameter = argTransformPara;

            _pool = new Semaphore(0, _noOfThreads);
            //doneEvents = new ManualResetEvent[_noOfThreads];

           
            _multiESIThreads = new MultiNGlycanESI[_noOfThreads];
            //Copy Dataset
            for (int i = 1; i < _noOfThreads; i++)  // only need n-1 copies
            {
                string newFilename = Path.GetDirectoryName(argRawFile) + "\\" + Path.GetFileNameWithoutExtension(argRawFile) + "-" + i.ToString() + Path.GetExtension(argRawFile);
                File.Copy(argRawFile, newFilename);
                rawFiles.Push(newFilename);
            }
            //Split Dataset            
            SplitDataset();
        }
        public void ProcessWithMultiThreads()
        {
            XRawReader rawReader;
            for (int i = 0; i < _noOfThreads; i++)
            {
                rawReader = new XRawReader(rawFiles.Pop());
               /* Thread p = new Thread(new MultiNGlycanESI(rawReader, _splitDataset[i], _ListGlycompound, _MassPPM, _GlycanPPM, _MergeDurationMin, _Permenthylated, _ReducedReducingEnd,));
                
               /* _multiESIProcessor.PeakProcessorParameters = _peakParameter;
                _multiESIProcessor.TransformParameters = _transformParameter;
                _multiESIThreads[i] = _multiESIProcessor;
                ThreadPool.QueueUserWorkItem(_multiESIProcessor.Process);*/
            }

            
        }
        private void SplitDataset()
        {
            System.Diagnostics.PerformanceCounter Proc = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
            int freeMemory = Convert.ToInt32(Proc.NextValue() * 0.7f);
            int MaxScanInOneSet = Convert.ToInt32(((freeMemory / _noOfThreads) / 45.0f) * 100.0f);
            if (MaxScanInOneSet >= 3000)
            {
                MaxScanInOneSet = 3000;
            }
            XRawReader rawReader = new XRawReader(rawFiles.Peek());
            List<int> MSScanNo = new List<int>();
            _splitDataset = new List<List<int>>();
            for (int i = StartScan; i <= EndScan; i++)
            {
                if (rawReader.GetMsLevel(i) == 1)
                {
                    MSScanNo.Add(i);
                }
            }
            int NoOfSet = Convert.ToInt32( Math.Ceiling(MSScanNo.Count / (double)MaxScanInOneSet));

            for (int i = 1; i <= NoOfSet; i++)
            {
                _splitDataset.Add(new List<int>());
            }            
            int DataSetIdx = 0;
            for (int i = 0; i < MSScanNo.Count; i++)
            {
                _splitDataset[DataSetIdx].Add(MSScanNo[i]);
                DataSetIdx++;
                if (DataSetIdx == NoOfSet)
                {
                    DataSetIdx = 0;
                }
            }
            rawReader.Close();
        }
        private void ReadGlycanList(string argGlycanFile)
        {
            _ListGlycompound = new List<GlycanCompound>();
            StreamReader sr;
            int LineNumber = 0;
            sr = new StreamReader(argGlycanFile);

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
                    _ListGlycompound.Add(new GlycanCompound(Convert.ToInt32(spilttmp[(int)compindex["hexnac"]]),
                                             Convert.ToInt32(spilttmp[(int)compindex["hex"]]),
                                             Convert.ToInt32(spilttmp[(int)compindex["dehex"]]),
                                             Convert.ToInt32(spilttmp[(int)compindex["sia"]]),
                                             true,
                                             false,
                                             true,
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

            if (_ListGlycompound.Count == 0)
            {
                throw new Exception("Glycan list file reading error. Please check input file.");
            }
            _ListGlycompound.Sort();
        }
    }
}
