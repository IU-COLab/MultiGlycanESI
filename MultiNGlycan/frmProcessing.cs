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
    public partial class frmProcessing : Form
    {
        private MultiGlycanESI _MultiNGlycan;
        List<int> LstScanNumber;
        private int CurrentScan=0;
        DateTime Start;
  
        bool DoLog = false;
   

        public frmProcessing(MultiGlycanESI argMultiNGlycan, bool argLog )
        {
            InitializeComponent();
            DoLog = argLog;

            _MultiNGlycan = argMultiNGlycan;
            _MultiNGlycan.MaxGlycanCharge = _MultiNGlycan.TransformParameters.MaxCharge;
            int StartScan = argMultiNGlycan.StartScan;
            int EndScan = argMultiNGlycan.EndScan;
            LstScanNumber = new List<int>();
            for (int i = StartScan; i <= EndScan; i++)
            {
                LstScanNumber.Add(i);
            }
            Start = DateTime.Now;
            if (DoLog)
            {
                Logger.WriteLog("Start process each scan");
            }
            bgWorker_Process.RunWorkerAsync();
        }
        //public frmProcessing(MultiNGlycanESIMultiThreads argMultiNGlycan, int argExportScanFilter)
        //{
        //    InitializeComponent();
        //    //_MultiNGlycan = argMultiNGlycan;
        //    //int StartScan = argMultiNGlycan.StartScan;
        //    //int EndScan = argMultiNGlycan.EndScan;
        //    //LstScanNumber = new List<int>();
        //    //for (int i = StartScan; i <= EndScan; i++)
        //    //{
        //    //    LstScanNumber.Add(i);
        //    //}
        //    //Start = DateTime.Now;
        //    //_GlycanScanFilter = argExportScanFilter;
        //    //bgWorker_Process.RunWorkerAsync();
        //}
        private void bgWorker_Process_DoWork(object sender, DoWorkEventArgs e)
        {            
            for(int i=0;i<LstScanNumber.Count;i++)
            {
                  _MultiNGlycan.ProcessSingleScan(LstScanNumber[i]);
                CurrentScan = i;
                bgWorker_Process.ReportProgress(Convert.ToInt32((i / (float)LstScanNumber.Count)*100));
                if (DoLog)
                {
                    Logger.WriteLog("Finish scan:" + LstScanNumber[i].ToString());
                }
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
            try
            {
                if (DoLog)
                {
                    Logger.WriteLog("Start merge peaks");
                }
                lblStatus.Text = "Mergeing Peaks";
                _MultiNGlycan.MergeCluster();
                if (DoLog)
                {
                    Logger.WriteLog("End merge peaks");
                }
                if (_MultiNGlycan.GlycanLCorderExist)
                {
                    _MultiNGlycan.ApplyLCordrer();
                }
                if (DoLog)
                {
                    Logger.WriteLog("Start export");
                }
                lblStatus.Text = "Exporting";
                _MultiNGlycan.Export();
                if (DoLog)
                {
                    Logger.WriteLog("End export");
                }
                TimeSpan TDiff = DateTime.Now.Subtract(Start);
                lblStatus.Text = "Finish in " + TDiff.TotalMinutes.ToString("0.00") + " mins";
                lblNumberOfMerge.Text = _MultiNGlycan.MergedPeak.Count.ToString();
                progressBar1.Value = 100;
                lblPercentage.Text = "100%";
                FlashWindow.Flash(this);
                this.Text = "Done";
                if (DoLog)
                {
                    Logger.WriteLog("End process each scan");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void frmProcessing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bgWorker_Process.IsBusy)
            {
                if (MessageBox.Show("Still processing, do you want to quit?", "Exit process?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bgWorker_Process.CancelAsync();
                    if (DoLog)
                    {
                        Logger.WriteLog("User terminate process");
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }    

    }
}
