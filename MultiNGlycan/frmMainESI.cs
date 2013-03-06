using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace COL.MultiGlycan
{
    public partial class frmMainESI : Form
    {
        GlypID.Peaks.clsPeakProcessorParameters _peakParameter;
        GlypID.HornTransform.clsHornTransformParameters _transformParameters;
        frmPeakParameters frmPeakpara;
        bool DoLog = false;
        private int _endScan = 0;

        public frmMainESI()
        {
            InitializeComponent();
            //int MaxCPU = Environment.ProcessorCount;
            //for (int i = 1; i <= MaxCPU; i++)
            //{
            //    cboCPU.Items.Add(i); 
            //}
            //cboCPU.SelectedIndex = (int)Math.Floor(cboCPU.Items.Count / 2.0f)-1;   
        }


        private void btnBrowseRaw_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "RAW Files (*.raw; *.mzXML)|*.raw;*.mzxml";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRawFile.Text = openFileDialog1.FileName;

                if (Path.GetExtension(openFileDialog1.FileName) == ".raw")
                {
                    COL.MassLib.XRawReader raw = new COL.MassLib.XRawReader(txtRawFile.Text);
                    _endScan = raw.NumberOfScans;
                }
                else
                {
                    COL.MassLib.mzXMLReader raw = new COL.MassLib.mzXMLReader(txtRawFile.Text);
                    _endScan = raw.NumberOfScans;
                }
                txtEndScan.Text = _endScan.ToString();
            }
        }

        private void rdoDefaultList_CheckedChanged(object sender, EventArgs e)
        {
            rdoUserList.Checked = !rdoDefaultList.Checked;
            txtGlycanList.Enabled = !rdoDefaultList.Checked;
            btnBrowseGlycan.Enabled = !rdoDefaultList.Checked;            
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            DoLog = chkLog.Checked;
            saveFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            DateTime time = DateTime.Now;             // Use current time
            string TimeFormat = "yyMMdd HHmm";            // Use this format

            if (DoLog)
            {
                Logger.WriteLog(System.Environment.NewLine + System.Environment.NewLine + "-----------------------------------------------------------" );
                Logger.WriteLog("Start Process");
            }

            saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(txtRawFile.Text) + "-" + time.ToString(TimeFormat) + ".csv";
            if (txtRawFile.Text == "" || (rdoUserList.Checked && txtGlycanList.Text == "") || txtMaxLCTime.Text =="")
            {
                MessageBox.Show("Please check input values.");
                if (DoLog)
                {
                   Logger.WriteLog("End Process- because input value not complete");
                }
                return ;
            }

                 _peakParameter = frmPeakpara.PeakProcessorParameters;
                _transformParameters = frmPeakpara.TransformParameters;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    string glycanlist = System.Windows.Forms.Application.StartupPath + "\\Default_Combination.csv";
                    if (!rdoDefaultList.Checked)
                    {
                        glycanlist = txtGlycanList.Text;
                    }

                    if (DoLog)
                    {

                       Logger.WriteLog("Start initial program");

                    }
                   // MultiNGlycanESIMultiThreads MultiESIs = new MultiNGlycanESIMultiThreads(glycanlist, txtRawFile.Text, Convert.ToInt32(cboCPU.SelectedItem), _peakParameter, _transformParameters);

                    MultiGlycanESI ESI = new MultiGlycanESI(txtRawFile.Text, Convert.ToInt32(txtStartScan.Text), Convert.ToInt32(txtEndScan.Text), glycanlist, Convert.ToDouble(txtPPM.Text), Convert.ToDouble(txtGlycanPPM.Text), Convert.ToDouble(txtMaxLCTime.Text), chkPermethylated.Checked, chkReducedReducingEnd.Checked,DoLog);
                    ESI.MergeDifferentChargeIntoOne = chkMergeDffCharge.Checked;
                    ESI.PeakProcessorParameters = _peakParameter;
                    ESI.TransformParameters = _transformParameters;
                    ESI.ExportFilePath = saveFileDialog1.FileName;
                    if (chkLCMax.Checked)
                    {
                        ESI.MaxLCMin = Convert.ToSingle(txtMaxLCTime.Text);
                    }
                    else
                    {
                        ESI.MaxLCMin = 9999;
                    }
                    if (chkLCMin.Checked)
                    {
                        ESI.MinLCMin = Convert.ToSingle(txtMinLCTime.Text);
                    }
                    else
                    {
                        ESI.MinLCMin = 0;
                    }
                    if (chkAbundance.Checked)
                    {
                        ESI.MinAbundance = Convert.ToDouble(txtAbundanceMin.Text);
                    }
                    else
                    {
                        ESI.MinAbundance = 0;
                    }
                    List<float> AdductMasses = new List<float>();
                    if (chkAdductK.Checked)
                    {
                        AdductMasses.Add(MassLib.Atoms.Potassium);
                    }
                    if (chkAdductNH4.Checked)
                    {
                        AdductMasses.Add(MassLib.Atoms.NitrogenMass + 4 * MassLib.Atoms.HydrogenMass);
                    }
                    if (chkAdductNa.Checked)
                    {
                        AdductMasses.Add(MassLib.Atoms.SodiumMass);
                    }
                    if (chkAdductProton.Checked)
                    {
                        AdductMasses.Add(MassLib.Atoms.ProtonMass);
                    }
                    float outMass = 0.0f;
                    if (chkAdductUser.Checked && float.TryParse(txtAdductMass.Text,out outMass))
                    {
                        AdductMasses.Add(outMass);
                    }
                    if (chkScanCount.Checked)
                    {
                        ESI.MinScanCount = Convert.ToInt32(txtScanCount.Text);
                    }
                    ESI.AdductMass = AdductMasses;

                    if (DoLog)
                    {
                       Logger.WriteLog("Initial program complete");
                    }

                    frmProcessing frmProcess = new frmProcessing(ESI, DoLog);
                    frmProcess.ShowDialog();

                    if (DoLog)
                    {
                       Logger.WriteLog("Finish process");
                    }
                }            
        }

        private void btnBrowseGlycan_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "CSV Files (.csv)|*.csv";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {                
                txtGlycanList.Text = openFileDialog1.FileName;
            }
        }

        private void rdoAllRaw_CheckedChanged(object sender, EventArgs e)
        {
            rdoScanNum.Checked = !rdoAllRaw.Checked;
            txtStartScan.Enabled = !rdoAllRaw.Checked;         
            txtEndScan.Enabled = !rdoAllRaw.Checked;
            txtStartScan.Text = "1";
            txtEndScan.Text = _endScan.ToString();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmPeakpara = new frmPeakParameters();
            frmPeakpara.ShowDialog();
            btnMerge.Enabled = true;
        }


        private void btnMergeTest_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"D:\Dropbox\for_Yunli_Hu\b1_19_1_07142012-121002 1349_FullList.csv");
            string tmp = sr.ReadLine();
            List<ClusteredPeak> clu = new List<ClusteredPeak>();
            do
            {
                tmp = sr.ReadLine();
                string[] tmpArray = tmp.Split(',');
                ClusteredPeak tnpCluPeak = new ClusteredPeak(Convert.ToInt32(tmpArray[1]));
                tnpCluPeak.StartTime = Convert.ToDouble(tmpArray[0]);
                tnpCluPeak.EndTime = Convert.ToDouble(tmpArray[0]);
                
                tnpCluPeak.EndScan = Convert.ToInt32(tmpArray[1]);
                tnpCluPeak.Intensity = Convert.ToSingle(tmpArray[2]);
                tnpCluPeak.GlycanComposition = new COL.GlycoLib.GlycanCompound(
                                                                                Convert.ToInt32(tmpArray[8]),
                                                                                Convert.ToInt32(tmpArray[9]),
                                                                                Convert.ToInt32(tmpArray[10]),
                                                                                Convert.ToInt32(tmpArray[11]));
                tnpCluPeak.Charge = Convert.ToInt32(Math.Ceiling(  Convert.ToSingle(tmpArray[12]) / Convert.ToSingle(tmpArray[3])));

                clu.Add(tnpCluPeak);
            } while (!sr.EndOfStream);
            sr.Close();
            //MultiNGlycanESI.MergeCluster(clu, 8.0);
        }

        private void chkAdductUser_CheckedChanged(object sender, EventArgs e)
        {
            txtAdductMass.Enabled = chkAdductUser.Checked;
        }

        private void eluctionProfileViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmView frView = new frmView();
            frView.ShowDialog();
        }

        private void chkLCMin_CheckedChanged(object sender, EventArgs e)
        {
            txtMinLCTime.Enabled = chkLCMin.Checked;
        }

        private void chkLCMax_CheckedChanged(object sender, EventArgs e)
        {
            txtMaxLCTime.Enabled = chkLCMax.Checked;
        }

        private void chkAbundance_CheckedChanged(object sender, EventArgs e)
        {
            txtAbundanceMin.Enabled = chkAbundance.Checked;
        }

        private void chkScanCount_CheckedChanged(object sender, EventArgs e)
        {
            txtScanCount.Enabled = chkScanCount.Checked;
        }

        private void massCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCalculator frmCalc = new frmCalculator();
            frmCalc.Show();
        }


 
    }
}
