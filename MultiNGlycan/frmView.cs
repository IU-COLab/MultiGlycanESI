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
                    string Key = Convert.ToInt32(tmp[8].Trim()).ToString("00") + "-" + Convert.ToInt32(tmp[9]).ToString("00") + "-" + Convert.ToInt32(tmp[10]).ToString("00") + "-" + Convert.ToInt32(tmp[11]).ToString("00"); // hex-hexnac-dehax-sia
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

        private void cboGlycan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = cboGlycan.SelectedItem.ToString();
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
            
            zgcGlycan.AxisChange();
            zgcGlycan.Refresh();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            string strPath = Path.GetDirectoryName(openFileDialog1.FileName) + "\\Glycan_EluteProfile(" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName)+")";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            for(int i =0;i<cboGlycan.Items.Count;i++)
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
