using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace COL.MultiNGlycan
{
    public partial class frmMainESI : Form
    {
        GlypID.Peaks.clsPeakProcessorParameters _peakParameter;
        GlypID.HornTransform.clsHornTransformParameters _transformParameters;
        frmPeakParameters frmPeakpara;
        private int _endScan = 0;
        public frmMainESI()
        {
            InitializeComponent();
            
        }


        private void btnBrowseRaw_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "RAW Files (*.raw)|*.raw";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRawFile.Text = openFileDialog1.FileName;
                COL.MassLib.XRawReader raw = new COL.MassLib.XRawReader(txtRawFile.Text);
                _endScan = raw.NumberOfScans;
                txtEndScan.Text = _endScan.ToString();
                raw.CloseRaw();
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
            saveFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            DateTime time = DateTime.Now;             // Use current time
	        string format = "yyMMdd hhmm";            // Use this format
	       
            saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(txtRawFile.Text) + "-" +time.ToString(format)+".csv";
            if (txtRawFile.Text == "" || (rdoUserList.Checked && txtGlycanList.Text == ""))
            {
                MessageBox.Show("Input raw and list or selest file.");
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

                    MultiNGlycanESI ESI = new MultiNGlycanESI(txtRawFile.Text, Convert.ToInt32(txtStartScan.Text), Convert.ToInt32(txtEndScan.Text), glycanlist, Convert.ToDouble(txtPPM.Text), Convert.ToDouble(txtGlycanPPM.Text), 5.0, chkPermethylated.Checked, chkReducedReducingEnd.Checked);
                    ESI.IncludeNonClusterGlycan = chkSingleCluster.Checked;
                    ESI.MergeDifferentChargeIntoOne = chkMergeDffCharge.Checked;
                    ESI.PeakProcessorParameters = _peakParameter;
                    ESI.TransformParameters = _transformParameters;
                    ESI.Process();
                    ESI.Export(saveFileDialog1.FileName, true);
                    MessageBox.Show("Done");

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

 
    }
}
