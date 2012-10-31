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
namespace COL.MultiNGlycan
{
    public partial class frmView : Form
    {
        public frmView()
        {
            InitializeComponent();
        }
        Dictionary<string, List<string>> dictValue;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dictValue = new Dictionary<string, List<string>>();
                cboGlycan.Items.Clear();
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                sr.ReadLine();
                string[] tmp = null;
                ArrayList alstGlycans = new ArrayList();
                do
                {
                    tmp = sr.ReadLine().Split(',');
                    int Charge = Convert.ToInt32(Math.Round(Convert.ToSingle(tmp[12]) / Convert.ToSingle(tmp[3]),0)); 
                    string Key = Convert.ToInt32(tmp[8].Trim()).ToString("00") + "-" + Convert.ToInt32(tmp[9]).ToString("00") + "-" + Convert.ToInt32(tmp[10]).ToString("00") + "-" + Convert.ToInt32(tmp[11]).ToString("00") + "-" + Charge.ToString(); // hex-hexnac-dehax-sia
                    if (!dictValue.ContainsKey(Key))
                    {
                        dictValue.Add(Key, new List<string>());
                        alstGlycans.Add(Key);
                    }
                    dictValue[Key].Add(tmp[1] + "-" + tmp[2]); // scan num - abuntance
                } while (!sr.EndOfStream);
                sr.Close();
                alstGlycans.Sort();
                cboGlycan.Items.AddRange(alstGlycans.ToArray());
                btnSaveAll.Enabled = true;
            }
        }
        PointPairList RawPPL;
        PointPairList SmoothPPL;
        private void cboGlycan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = cboGlycan.SelectedItem.ToString();
            List<string> keyValue = dictValue[key];
            RawPPL = new PointPairList();
            string[] tmpLst = null;
            GraphPane GP = zgcGlycan.GraphPane;
            GP.Title.Text = "Glycan: " + key;
            GP.XAxis.Title.Text = "Scan no.";
            GP.YAxis.Title.Text = "Abundance";
            GP.CurveList.Clear();
            double MaxIntensity = 0.0;

            foreach (string tmp in keyValue)
            {
                tmpLst = tmp.Split('-');
                RawPPL.Add(new PointPair(Convert.ToDouble(tmpLst[0]),
                                                        Convert.ToDouble(tmpLst[1])));
                if (MaxIntensity <=  Convert.ToDouble(tmpLst[1]))
                {
                    MaxIntensity = Convert.ToDouble(tmpLst[1]);
                }
            }

            LineItem RawLine = GP.AddCurve("Raw", RawPPL, Color.Blue, SymbolType.Circle);
            RawLine.Symbol.Size = 2.0f;
            RawLine.Line.IsVisible = false;
            List<float> LstIntensity = new List<float>();
            if (chkSmooth.Checked)
            {
                MaxIntensity = 0.0;
                SmoothPPL = new PointPairList();
                List<COL.MassLib.MSPeak> tmpPeak = new List<COL.MassLib.MSPeak>();
                foreach (string tmp in keyValue)
                {
                    tmpLst = tmp.Split('-');
                    tmpPeak.Add(new COL.MassLib.MSPeak(Convert.ToSingle(tmpLst[0]),
                                                            Convert.ToSingle(tmpLst[1])));
                }
                List<COL.MassLib.MSPeak> Smoothed = COL.MassLib.Smooth.SavitzkyGolay.Smooth(tmpPeak, COL.MassLib.Smooth.SavitzkyGolay.FILTER_WIDTH.FILTER_WIDTH_11);

                foreach (COL.MassLib.MSPeak pek in Smoothed)
                {
                    SmoothPPL.Add(new PointPair(Convert.ToDouble(pek.MonoisotopicMZ),
                                                            Convert.ToDouble(pek.MonoIntensity)));
                    if (MaxIntensity <= Convert.ToDouble(pek.MonoIntensity))
                    {
                        MaxIntensity = Convert.ToDouble(pek.MonoIntensity);
                    }
                    LstIntensity.Add(pek.MonoIntensity);
                }
                LineItem SmoothLine = GP.AddCurve("Smooth", SmoothPPL, Color.Red, SymbolType.XCross);
                SmoothLine.Symbol.Size = 5.0f;
                SmoothLine.Line.Width = 2.0f;
            }
            LstIntensity.Sort();
            LstIntensity.Reverse();

            //double Cutoff = Math.Sqrt(MaxIntensity);
            //PointPairList CutoffPPL = new PointPairList();
            //CutoffPPL.Add(SmoothPPL[0].X, Cutoff);
            //CutoffPPL.Add(SmoothPPL[SmoothPPL.Count-1].X, Cutoff);
            //LineItem Background = GP.AddCurve("Cufoff", CutoffPPL, Color.Black);
            //Background.Line.Width = 3.0f;
            zgcGlycan.AxisChange();
            zgcGlycan.Refresh();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            string strPath = Path.GetDirectoryName(openFileDialog1.FileName) + "\\Glycan_EluteProfile(" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + ")";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            for (int i = 0; i < cboGlycan.Items.Count; i++)
            {
                string key = cboGlycan.Items[i].ToString();
                List<string> keyValue = dictValue[key];
                PointPairList ppl = new PointPairList();
                string[] tmpLst = null;
                foreach (string tmp in keyValue)
                {
                    tmpLst = tmp.Split('-');
                    ppl.Add(new PointPair(Convert.ToDouble(tmpLst[0]),
                                                            Convert.ToDouble(tmpLst[1])));
                }

                GraphPane GP = zgcGlycan.GraphPane;
                GP.Title.Text = "Glycan: " + key;
                GP.XAxis.Title.Text = "Scan no.";
                GP.YAxis.Title.Text = "Abundance";
                GP.CurveList.Clear();
                GP.AddCurve(key, ppl, Color.Blue, SymbolType.Circle);
                GP.AxisChange();

                GP.GetImage().Save(strPath + "\\" + key + ".png", System.Drawing.Imaging.ImageFormat.Png);

            }
        }
    }
}
