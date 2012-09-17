using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using COL.GlycoLib;
using COL.MassLib;
namespace COL.MultiNGlycan
{
    public partial class frmMultiThreads : Form
    {
        //List<GlycanCompound> _GlycanList;
        XRawReader _Raw;
        frmPeakParameters frmPeakpara;
        //int NoOfThread = 2;

        
        public frmMultiThreads()
        {
            InitializeComponent();
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPeakpara = new frmPeakParameters();
            frmPeakpara.ShowDialog();
            //ReadGlycanList();

            


            System.Diagnostics.PerformanceCounter Proc = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
            int freeMemory = Convert.ToInt32(Proc.NextValue() * 0.7f);

            _Raw = new XRawReader(@"D:\Dropbox\for_Yunli_Hu\b1_19_1_07142012.raw");
            //MultiNGlycanESIMultiThreads main = new MultiNGlycanESIMultiThreads(_GlycanList, _Raw, NoOfThread, frmPeakpara.PeakProcessorParameters, frmPeakpara.TransformParameters);
            //main.ProcessWithMultiThreads();
        }
        
    }
}
