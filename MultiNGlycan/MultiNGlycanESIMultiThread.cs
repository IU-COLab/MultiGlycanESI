using System;
using System.Collections.Generic;
using System.Text;
using COL.MassLib;
using COL.GlycoLib;
namespace COL.MultiNGlycan
{
    public class MultiNGlycanESIMultiThread
    {
        List<ClusteredPeak> ClsPeaks;
        public  MultiNGlycanESIMultiThread(List<GlycoLib.GlycanCompound> argGlycanCompound, MassLib.MSScan argScan, int argMaxCharge, float argGlycanPPM, float argMassPPM)
        {            
             ClsPeaks = new List<ClusteredPeak>();
            List<MSPeak> SortedPeaks = argScan.MSPeaks;
            SortedPeaks.Sort(delegate(MSPeak P1, MSPeak P2) { return Comparer<double>.Default.Compare(P1.MonoisotopicMZ, P2.MonoisotopicMZ); });
            List<float> PeakMZ = new List<float>();
            foreach (MSPeak p in SortedPeaks)
            {
                PeakMZ.Add(p.MonoMass);
            }
            foreach (GlycanCompound comp in argGlycanCompound)
            {
                float[] GlycanMZ = new float[argMaxCharge + 1]; // GlycanMZ[1] = charge 1; GlycanMZ[2] = charge 2
                for (int i = 1; i <= argMaxCharge; i++)
                {
                    GlycanMZ[i] = (float)(comp.MonoMass + MassLib.Atoms.ProtonMass * i) / (float)i;
                }
                for (int i = 1; i <= argMaxCharge; i++)
                {
                    int ClosedPeak = FindClosedPeakIdx(PeakMZ, GlycanMZ[i]);
                    int ChargeState = SortedPeaks[ClosedPeak].ChargeState;
                    if (ChargeState != i || ChargeState == 0 ||
                        MassLib.MassUtility.GetMassPPM(SortedPeaks[ClosedPeak].MonoisotopicMZ, GlycanMZ[i]) > argGlycanPPM)
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
                        PeakIdx[0] = i;
                        for (int j = 1; j < PeakIdx.Length; j++)
                        {
                            PeakIdx[j] = -1;
                        }
                        int MatchCount = 1;
                        for (int j = 1; j < PeakIdx.Length; j++)
                        {
                            int ClosedPeak2 = FindClosedPeakIdx(PeakMZ, Step[j]);
                            if (MassLib.MassUtility.GetMassPPM(PeakMZ[ClosedPeak2], Step[j]) < argMassPPM)
                            {
                                PeakIdx[j] = ClosedPeak2;
                                MatchCount++;
                            }
                        }
                        if (MatchCount >= SortedPeaks[i].ChargeState || (MatchCount == 1))
                        {
                            ClusteredPeak Cls = new ClusteredPeak(argScan.ScanNo);
                            for (int j = 0; j < PeakIdx.Length; j++)
                            {
                                if (PeakIdx[j] != -1)
                                {
                                    Cls.Peaks.Add(SortedPeaks[PeakIdx[j]]);
                                }
                            }
                            Cls.StartTime = argScan.Time;
                            Cls.Charge = SortedPeaks[i].ChargeState;
                            Cls.GlycanCompostion = comp;
                            ClsPeaks.Add(Cls);
                        }
                    }
                }
            }            
        }
        public List<ClusteredPeak> ClusteredPeaks
        {
            get { return ClsPeaks; }
        }
        private  int FindClosedPeakIdx(List<float> argMZList, float argTargetMz)
        {
            float Distance = 10000.0f;
            int smallidx = 0;
            for (int i = 0; i < argMZList.Count; i++)
            {
                if (Math.Abs(argMZList[i] - argTargetMz) < Distance)
                {
                    Distance = Math.Abs(argMZList[i] - argTargetMz);
                    smallidx = i;
                }
            }
            return smallidx;
        }
    }
}
