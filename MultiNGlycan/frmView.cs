using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
namespace COL.MultiGlycan
{
    public partial class frmView : Form
    {
        List<Color> LstColor ;
        public frmView()
        {
            InitializeComponent();
            LstColor = new List<Color>();
            zgcGlycan.IsEnableHZoom = false;
            zgcGlycan.IsEnableVZoom = false;
            zgcGlycan.IsEnableWheelZoom = false;
            
            foreach (KnownColor knowColor in Enum.GetValues(typeof(KnownColor)))
            {
                LstColor.Add( Color.FromKnownColor(knowColor));
            }            
        }
        Dictionary<string, Dictionary<float,List<string>>> dictValue;
        
        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dictValue = new Dictionary<string, Dictionary<float, List<string>>>();
                cboGlycan.Items.Clear();
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                int CompositionIdx = 5;
                string[] tmp = null;
                tmp = sr.ReadLine().Split(',');
                if (tmp[5] == "Adduct")
                {
                    CompositionIdx = 6;
                }
                ArrayList alstGlycans = new ArrayList();
                do
                {
                    tmp = sr.ReadLine().Split(',');
                    int Charge = Convert.ToInt32(Math.Round(Convert.ToSingle(tmp[CompositionIdx]) / Convert.ToSingle(tmp[3]), 0)); 
                    string Key = tmp[4]+ "-" + Charge.ToString(); // hex-hexnac-dehax-sia
                    if (!dictValue.ContainsKey(Key))
                    {
                        dictValue.Add(Key, new Dictionary<float, List<string>>());
                        alstGlycans.Add(Key);
                    }
                    float mz = Convert.ToSingle( tmp[3].ToString());
                    mz = Convert.ToSingle( mz.ToString("F1"));
                    if (!dictValue[Key].ContainsKey(mz))
                    {
                        dictValue[Key].Add(mz, new List<string>());
                    }

                    dictValue[Key][mz].Add( tmp[0] + "-" + tmp[2]); // scan time - abuntance
                } while (!sr.EndOfStream);
                sr.Close();
                alstGlycans.Sort();
                cboGlycan.Items.AddRange(alstGlycans.ToArray());
                btnSaveAll.Enabled = true;
            }
        }

        private void cboGlycan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGlycan.SelectedItem == null)
            {
                return;
            }
            string key = cboGlycan.SelectedItem.ToString();
            Dictionary<float, List<string>> keyValue = dictValue[key];
            
            string[] tmpLst = null;
            #region zedgraph
            GraphPane GP = zgcGlycan.GraphPane;
            GP.Title.Text = "Glycan: " + key;
            GP.XAxis.Title.Text = "Scan time (min)";
            GP.YAxis.Title.Text = "Abundance";
            GP.CurveList.Clear();
            double MaxIntensity = 0.0;
            Dictionary<float, PointPairList> _ZedgraphRaw = new Dictionary<float, PointPairList>();
            COL.ElutionViewer.MSPointSet3D Eluction3DRaw = new ElutionViewer.MSPointSet3D();
            foreach (float mz in keyValue.Keys)
            {
                PointPairList RawPPL = new PointPairList();
                foreach (string tmp in keyValue[mz])
                {
                    tmpLst = tmp.Split('-');
                    float time = Convert.ToSingle(tmpLst[0]);
                    float intensity = Convert.ToSingle(tmpLst[1]);

                    RawPPL.Add(new PointPair(time,intensity));
                    if (MaxIntensity <= Convert.ToDouble(tmpLst[1]))
                    {
                        MaxIntensity = Convert.ToDouble(tmpLst[1]);
                    }
                    Eluction3DRaw.Add(time, mz, intensity);
                }
                _ZedgraphRaw.Add(mz, RawPPL);
            }
            int ColorIdx = 0;

            //Add LineItem
            foreach (float mz in _ZedgraphRaw.Keys)
            {

                LineItem RawLine = GP.AddCurve(mz.ToString(), _ZedgraphRaw[mz], LstColor[ColorIdx]);
                ColorIdx = ColorIdx+10%174;
                RawLine.Symbol.Size = 2.0f;
                RawLine.Line.IsVisible = true;
                //List<float> LstIntensity = new List<float>();
                //if (chkSmooth.Checked)
                //{
                //    MaxIntensity = 0.0;
                //    SmoothPPL = new PointPairList();
                //    List<COL.MassLib.MSPeak> tmpPeak = new List<COL.MassLib.MSPeak>();
                //    foreach (string tmp in keyValue)
                //    {
                //        tmpLst = tmp.Split('-');
                //        tmpPeak.Add(new COL.MassLib.MSPeak(Convert.ToSingle(tmpLst[0]),
                //                                                Convert.ToSingle(tmpLst[1])));
                //    }
                //    List<COL.MassLib.MSPeak> Smoothed = COL.MassLib.Smooth.SavitzkyGolay.Smooth(tmpPeak, COL.MassLib.Smooth.SavitzkyGolay.FILTER_WIDTH.FILTER_WIDTH_11);

                //    for (int i = 0; i < Smoothed.Count; i++)
                //    {
                //        SmoothPPL.Add(new PointPair(Convert.ToDouble(RawPPL[i].X),
                //                            Convert.ToDouble(Smoothed[i].MonoIntensity)));
                //        if (MaxIntensity <= Convert.ToDouble(Smoothed[i].MonoIntensity))
                //        {
                //            MaxIntensity = Convert.ToDouble(Smoothed[i].MonoIntensity);
                //        }
                //        LstIntensity.Add(Smoothed[i].MonoIntensity);
                //    }


                //}
                //    LineItem SmoothLine = GP.AddCurve("Smooth", SmoothPPL, Color.Red, SymbolType.XCross);
                //    SmoothLine.Symbol.Size = 5.0f;
                //    SmoothLine.Line.Width = 2.0f;
                //}
                //LstIntensity.Sort();
                //LstIntensity.Reverse();
            }
            //double Cutoff = Math.Sqrt(MaxIntensity);
            //PointPairList CutoffPPL = new PointPairList();
            //CutoffPPL.Add(SmoothPPL[0].X, Cutoff);
            //CutoffPPL.Add(SmoothPPL[SmoothPPL.Count-1].X, Cutoff);
            //LineItem Background = GP.AddCurve("Cufoff", CutoffPPL, Color.Black);
            //Background.Line.Width = 3.0f;
            zgcGlycan.AxisChange();
            zgcGlycan.Refresh();
            #endregion zedgraph


            #region Eluction3D
            eluctionViewer1.SetData( COL.ElutionViewer.EViewerHandler.Create3DHandler(Eluction3DRaw));
            
            #endregion Eluction3D
        }
        private void rdoFullLC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFullLC.Checked)
            {
                PlotFullLC();
            }
            else
            {
                cboGlycan_SelectedIndexChanged(sender, e);
            }
        }
        private void PlotFullLC()
        {
            //ZedGraph.GraphPane zgp = zgcGlycan.GraphPane;
            //zgp.CurveList.Clear();
            //foreach (string key in dictValue.Keys)
            //{
            //    if (dictValue[key].Count == 1)
            //    {
            //        continue;
            //    }
            //    PointPairList ppl = new PointPairList();
            //    string[] tmpLst = null;
            //    double mz = Convert.ToDouble(dictValue[key][0].Split('-')[2]);
            //    foreach (string tmp in  dictValue[key])
            //    {
            //        tmpLst = tmp.Split('-');
            //        ppl.Add(new PointPair(Convert.ToDouble(tmpLst[0]),
            //                                                mz));
            //    }
            //    TextObj txtLabel = new TextObj(key,ppl[ppl.Count-1].X,ppl[ppl.Count-1].Y);
            //    txtLabel.FontSpec.Border.IsVisible = false;
            //    txtLabel.FontSpec.Fill.IsVisible = false;
            //    txtLabel.Location.AlignH = AlignH.Left;
            //    txtLabel.Location.AlignV = AlignV.Center;
            //    txtLabel.FontSpec.Size = 5.0f;
            //    zgp.GraphObjList.Add(txtLabel);
            //    zgp.AddCurve(key, ppl, Color.Blue,SymbolType.None);
            //    ((ZedGraph.LineItem)zgp.CurveList[zgp.CurveList.Count - 1]).Line.Width = 3.0f;
            //}
            //zgp.Legend.IsVisible = false;
            //zgp.AxisChange();

        }
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
        
                //string strPath = Path.GetDirectoryName(openFileDialog1.FileName) + "\\Glycan_EluteProfile(" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + ")";
                //if (!Directory.Exists(strPath))
                //{
                //    Directory.CreateDirectory(strPath);
                //}
                //for (int i = 0; i < cboGlycan.Items.Count; i++)
                //{
                //    string key = cboGlycan.Items[i].ToString();
                //    List<string> keyValue = dictValue[key];
                //    PointPairList ppl = new PointPairList();
                //    string[] tmpLst = null;
                //    foreach (string tmp in keyValue)
                //    {
                //        tmpLst = tmp.Split('-');
                //        ppl.Add(new PointPair(Convert.ToDouble(tmpLst[0]),
                //                                                Convert.ToDouble(tmpLst[1])));
                //    }
                //    List<COL.MassLib.MSPeak> tmpPeak = new List<COL.MassLib.MSPeak>();
                //    foreach (string tmp in keyValue)
                //    {
                //        tmpLst = tmp.Split('-');
                //        tmpPeak.Add(new COL.MassLib.MSPeak(Convert.ToSingle(tmpLst[0]),
                //                                                Convert.ToSingle(tmpLst[1])));
                //    }
                //    List<COL.MassLib.MSPeak> Smoothed = COL.MassLib.Smooth.SavitzkyGolay.Smooth(tmpPeak, COL.MassLib.Smooth.SavitzkyGolay.FILTER_WIDTH.FILTER_WIDTH_11);
                //    SmoothPPL = new PointPairList();
                //    for (int j = 0; j < Smoothed.Count; j++)
                //    {
                //        SmoothPPL.Add(new PointPair(Convert.ToDouble(ppl[j].X),
                //                            Convert.ToDouble(Smoothed[j].MonoIntensity)));
                //    }
                //    GraphPane GP = zgcGlycan.GraphPane;
                //    GP.Title.Text = "Glycan: " + key;
                //    GP.XAxis.Title.Text = "Time";
                //    GP.YAxis.Title.Text = "Abundance";
                //    GP.CurveList.Clear();
                //    GP.AddCurve(key, ppl, Color.Blue, SymbolType.Circle);
                //    LineItem SmoothLine = GP.AddCurve("Smooth", SmoothPPL, Color.Red, SymbolType.XCross);
                //    SmoothLine.Symbol.Size = 5.0f;
                //    SmoothLine.Line.Width = 2.0f;
                //    GP.AxisChange();
                //    GP.GetImage().Save(strPath + "\\" + key + ".png", System.Drawing.Imaging.ImageFormat.Png);
                //}
                ////Export Merge Result
                //openFileDialog1.Title = "Load Merge Result";
                //if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                //    sr.ReadLine(); // title
                //    string PreviousKey ="";
                //    string Key ;
                //    List<PointPairList> lstPPL = new List<PointPairList>();
                //    do
                //    {
                //        string[] tmpAry = sr.ReadLine().Split(',');
                //        Key = Convert.ToInt32(tmpAry[6].Trim()).ToString("00") + "-" + Convert.ToInt32(tmpAry[7]).ToString("00") + "-" + Convert.ToInt32(tmpAry[8]).ToString("00") + "-" + Convert.ToInt32(tmpAry[9]).ToString("00"); // hex-hexnac-dehax-sia
                //        if (Key == "06-06-00-00")
                //        {
                //            Console.Write("aaa");

                //        }
                //        double startTime = Convert.ToDouble(tmpAry[0]);
                //        double endTime = Convert.ToDouble(tmpAry[1]);
                //        if (PreviousKey == "")
                //        {
                //            PreviousKey = Key;
                //        }
                //        if(Key!= PreviousKey) //Export
                //        {
                //            GraphPane GP = zgcGlycan.GraphPane;
                //            GP.Title.Text = "Glycan: " + PreviousKey;
                //            GP.XAxis.Title.Text = "Time";
                //            GP.YAxis.Title.Text = "Abundance";
                //            GP.CurveList.Clear();
                //            for (int i = 0; i < lstPPL.Count; i++)
                //            {
                //                PointPairList ppl = lstPPL[i];
                //                GP.AddCurve("Cluster-" + i.ToString() + "(" + ppl[0].X.ToString("00.00") + "-" + ppl[ppl.Count - 1].X.ToString("00.00") + ")", ppl, Color.Blue, SymbolType.Circle);
                //            }
                //            GP.AxisChange();
                //            GP.GetImage().Save(strPath + "\\Merge_" + PreviousKey + ".png", System.Drawing.Imaging.ImageFormat.Png);
                //            lstPPL.Clear();
                //            PreviousKey = Key;
                //        }
                //        PointPairList tmpppl = new PointPairList();
                //        for(int i = 1;i<=5;i++)
                //        {
                //            string KeyPlusCharge = Key+"-"+i.ToString();
                //            if(dictValue.ContainsKey(KeyPlusCharge))
                //            {
                //                List<string> keyValue = dictValue[KeyPlusCharge];
                //                string[] tmpLst;
                //                foreach (string tmp in keyValue)
                //                {
                //                    tmpLst = tmp.Split('-');
                //                    double time =Convert.ToDouble(tmpLst[0]);
                //                    if (startTime <= time && time <= endTime)
                //                    {
                //                        tmpppl.Add(new PointPair(time,
                //                                                                Convert.ToDouble(tmpLst[1])));
                //                    }
                //                }
                //            }
                //        }
                //        tmpppl.Sort();
                //        lstPPL.Add(tmpppl);
                //    }while (!sr.EndOfStream);
                //    sr.Close();

                //    GraphPane lstGP = zgcGlycan.GraphPane;
                //    lstGP.Title.Text = "Glycan: " + Key;
                //    lstGP.XAxis.Title.Text = "Time";
                //    lstGP.YAxis.Title.Text = "Abundance";
                //    lstGP.CurveList.Clear();
                //    for (int i = 0; i < lstPPL.Count; i++)
                //    {
                //        PointPairList ppl = lstPPL[i];
                //        lstGP.AddCurve("Cluster-" + i.ToString() + "(" + ppl[0].X.ToString("00.00") + "-" + ppl[ppl.Count - 1].X.ToString("00.00") + ")", ppl, Color.Blue, SymbolType.Circle);
                //    }
                //    lstGP.AxisChange();
                //    lstGP.GetImage().Save(strPath + "\\Merge_" + Key + ".png", System.Drawing.Imaging.ImageFormat.Png);
                //    lstPPL.Clear();
                //}
                //MessageBox.Show("Done");
            }

        private void btnSaveWholeProfile_Click(object sender, EventArgs e)
        {
            //if (dictValue.Keys.Count == 0)
            //{
            //    return;
            //}
            //string strPath = Path.GetDirectoryName(openFileDialog1.FileName) + "\\Glycan_EluteProfile(" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + ")";
            //if (!Directory.Exists(strPath))
            //{
            //    Directory.CreateDirectory(strPath);
            //}
            //ZedGraph.GraphPane zgp = new GraphPane(new RectangleF(0, 0, 5000,5000), Path.GetFileNameWithoutExtension(openFileDialog1.FileName), "Time(min)", "m/z");
            //zgp.CurveList.Clear();
            //foreach (string key in dictValue.Keys)
            //{
            //    if (dictValue[key].Count == 1)
            //    {
            //        continue;
            //    }
            //    PointPairList ppl = new PointPairList();
            //    string[] tmpLst = null;
            //    double mz = Convert.ToDouble(dictValue[key][0].Split('-')[2]);
            //    foreach (string tmp in dictValue[key])
            //    {
            //        tmpLst = tmp.Split('-');
            //        ppl.Add(new PointPair(Convert.ToDouble(tmpLst[0]),
            //                                                mz));
            //    }
            //    TextObj txtLabel = new TextObj(key, ppl[ppl.Count - 1].X, ppl[ppl.Count - 1].Y);
            //    txtLabel.FontSpec.Border.IsVisible = false;
            //    txtLabel.FontSpec.Fill.IsVisible = false;
            //    txtLabel.Location.AlignH = AlignH.Left;
            //    txtLabel.Location.AlignV = AlignV.Center;
            //    txtLabel.FontSpec.Size = 5.0f;
            //    zgp.GraphObjList.Add(txtLabel);
            //    zgp.AddCurve(key, ppl, Color.Blue, SymbolType.None);
            //    ((ZedGraph.LineItem)zgp.CurveList[zgp.CurveList.Count - 1]).Line.Width = 3.0f;
            //}
            //zgp.Legend.IsVisible = false;
            //zgp.AxisChange();

            //zgp.GetImage().Save(strPath + "\\Eluction_Profile.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void chkSmooth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSmooth.Checked)
            {
                eluctionViewer1.Smooth();
            }

        }
        public void ReSizeZedGraphForm3DPlot(object sender, EventArgs e)
        {

            zgcGlycan.MasterPane.PaneList[0].XAxis.Scale.Max = ((COL.ElutionViewer.RegionSize)e).RightBound;
            zgcGlycan.MasterPane.PaneList[0].XAxis.Scale.Min = ((COL.ElutionViewer.RegionSize)e).LeftBound;

            zgcGlycan.AxisChange();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            int ColorIdx = 0;
            zgcGlycan.MasterPane[0].CurveList.Clear();
            List<string> Glycans = new List<string>();
            Glycans.Add("0-4-0-0");
            Glycans.Add("2-3-0-0");
            Glycans.Add("2-4-0-0");
            Glycans.Add("2-5-0-0");
            Glycans.Add("2-6-0-0");
            Glycans.Add("2-7-0-0");
            Glycans.Add("2-8-0-0");
            Glycans.Add("2-9-0-0");
            Glycans.Add("2-10-0-0");
            GraphPane GP = zgcGlycan.GraphPane;
            GP.Title.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName).Split('-')[0];
            GP.XAxis.Title.Text = "Scan time (min)";
            GP.YAxis.Title.Text = "Abundance";
            foreach (string g in Glycans)
            {
                Dictionary<float, float> _GLycanIntensity = new Dictionary<float, float>();
                for (int i = 1; i <= 5; i++)
                {
                    string key = g + "-" + i.ToString();
                    if (!dictValue.ContainsKey(key))
                    {
                        continue;
                    }
                    Dictionary<float, List<string>> keyValue = dictValue[key];

                    string[] tmpLst = null;                      
                    foreach (float mz in keyValue.Keys)
                    {
  
                        foreach (string tmp in keyValue[mz])
                        {
                            tmpLst = tmp.Split('-');
                            float time = Convert.ToSingle(Convert.ToSingle(tmpLst[0]).ToString("0.00"));
                            float intensity = Convert.ToSingle(tmpLst[1]);
                            if (_GLycanIntensity.ContainsKey(time))
                            {
                                _GLycanIntensity[time] = _GLycanIntensity[time] + intensity;
                            }
                            else
                            {
                                _GLycanIntensity.Add(time, intensity);
                            }
                        }                        
                    }
                }

                List<KeyValuePair<float, float>> myList = new List<KeyValuePair<float, float>>();
                foreach (float key in _GLycanIntensity.Keys)
                {
                    myList.Add(new KeyValuePair<float, float>(key, _GLycanIntensity[key]));
                }
                myList.Sort(delegate(KeyValuePair<float, float> firstPair, KeyValuePair<float, float> nextPair)
                {
                    return firstPair.Key.CompareTo(nextPair.Key);
                });

                //Add LineItem
                PointPairList RawPPL = new PointPairList();
                foreach (KeyValuePair<float, float> KV in myList)
                {
                    RawPPL.Add(KV.Key,KV.Value);
                }
                LineItem RawLine = GP.AddCurve(g, RawPPL, LstColor[ColorIdx]);
                ColorIdx = ColorIdx + 11 % 174;
                RawLine.Symbol.Size = 3.0f;
                RawLine.Line.IsVisible = true;
            }
            zgcGlycan.MasterPane[0].YAxis.Scale.Max = 400000000;
            zgcGlycan.AxisChange();

            zgcGlycan.Refresh();
        }        
    }
}
