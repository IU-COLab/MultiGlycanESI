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
            //int MaxCPU = Environment.ProcessorCount;
            //for (int i = 1; i <= MaxCPU; i++)
            //{
            //    cboCPU.Items.Add(i); 
            //}
            //cboCPU.SelectedIndex = (int)Math.Floor(cboCPU.Items.Count / 2.0f)-1;
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
            //FileInfo rawInfo = new FileInfo(txtRawFile.Text);
            //DriveInfo dInfo = new DriveInfo(Directory.GetDirectoryRoot(txtRawFile.Text));
            //if (dInfo.TotalFreeSpace <= rawInfo.Length * (int)cboCPU.SelectedItem)
            //{
            //    MessageBox.Show("Not enough free space to duplicate Raw file.");
            //    return;
            //}


            saveFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            DateTime time = DateTime.Now;             // Use current time
            string TimeFormat = "yyMMdd HHmm";            // Use this format

            saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(txtRawFile.Text) + "-" + time.ToString(TimeFormat) + ".csv";
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

                   // MultiNGlycanESIMultiThreads MultiESIs = new MultiNGlycanESIMultiThreads(glycanlist, txtRawFile.Text, Convert.ToInt32(cboCPU.SelectedItem), _peakParameter, _transformParameters);

                    MultiNGlycanESI ESI = new MultiNGlycanESI(txtRawFile.Text, Convert.ToInt32(txtStartScan.Text), Convert.ToInt32(txtEndScan.Text), glycanlist, Convert.ToDouble(txtPPM.Text), Convert.ToDouble(txtGlycanPPM.Text), Convert.ToDouble(txtMergeWindow.Text), chkPermethylated.Checked, chkReducedReducingEnd.Checked);
                    ESI.IncludeNonClusterGlycan = chkSingleCluster.Checked;
                    ESI.MergeDifferentChargeIntoOne = chkMergeDffCharge.Checked;
                    ESI.PeakProcessorParameters = _peakParameter;
                    ESI.TransformParameters = _transformParameters;
                    ESI.ExportFilePath = saveFileDialog1.FileName;
                    frmProcessing frmProcess = new frmProcessing(ESI, Convert.ToInt32(txtOutputScanFilter.Text));
                    frmProcess.ShowDialog();
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

        private void btnView_Click(object sender, EventArgs e)
        {
            frmView frView = new frmView();
            frView.ShowDialog();
        }




 
    }
}
