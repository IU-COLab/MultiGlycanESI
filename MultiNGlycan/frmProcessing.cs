using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace COL.MultiNGlycan
{
    public partial class frmProcessing : Form
    {
        private MultiNGlycanESI _MultiNGlycan;
        List<int> LstScanNumber;
        private int CurrentScan=0;
        DateTime Start;
        float _GlycanDuration;
        public frmProcessing(MultiNGlycanESI argMultiNGlycan, float argExportDuration )
        {
            InitializeComponent();
            _MultiNGlycan = argMultiNGlycan;
            int StartScan = argMultiNGlycan.StartScan;
            int EndScan = argMultiNGlycan.EndScan;
            LstScanNumber = new List<int>();
            for (int i = StartScan; i <= EndScan; i++)
            {
                LstScanNumber.Add(i);
            }
            Start = DateTime.Now;
            _GlycanDuration = argExportDuration;
            bgWorker_Process.RunWorkerAsync();
        }

        private void bgWorker_Process_DoWork(object sender, DoWorkEventArgs e)
        {
            
            for(int i=0;i<LstScanNumber.Count;i++)
            {
                _MultiNGlycan.ProcessSingleScan(LstScanNumber[i]);
                CurrentScan = i;
                bgWorker_Process.ReportProgress(Convert.ToInt32((i / (float)LstScanNumber.Count)*100));
            }            
        }

        private void bgWorker_Process_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblCurrentScan.Text = (CurrentScan+1).ToString() + " / " + LstScanNumber.Count.ToString();
            progressBar1.Value =  e.ProgressPercentage;
            lblPercentage.Text = e.ProgressPercentage.ToString() + "%";
            lblNumberOfCluster.Text = _MultiNGlycan.ClustedPeak.Count.ToString();
            lblStatus.Text = "Processing Scan " + LstScanNumber[CurrentScan].ToString();
        }

        private void bgWorker_Process_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Text = "Mergeing Peaks";
            _MultiNGlycan.MergeCluster();
            lblStatus.Text = "Exporting";
            _MultiNGlycan.Export(true, _GlycanDuration);
            TimeSpan TDiff = DateTime.Now.Subtract(Start);
            lblStatus.Text = "Finish in " + TDiff.TotalMinutes.ToString("0.00") + " mins";
            lblNumberOfMerge.Text = _MultiNGlycan.MergedPeak.Count.ToString();
            progressBar1.Value = 100;
            lblPercentage.Text =  "100%";
            FlashWindow.Flash(this);
            this.Text = "Done";

            MessageBox.Show("Done");
        }

        private void frmProcessing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bgWorker_Process.IsBusy)
            {
                if (MessageBox.Show("Still processing, do you want to quit?", "Exit process?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bgWorker_Process.CancelAsync();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }    

    }
}
