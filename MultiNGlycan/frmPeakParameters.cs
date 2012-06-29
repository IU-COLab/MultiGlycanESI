﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace COL.MultiNGlycan
{
    public partial class frmPeakParameters : Form
    {
        GlypID.Peaks.clsPeakProcessorParameters _peakParameter;
        GlypID.HornTransform.clsHornTransformParameters _transformParameters;
        public frmPeakParameters()
        {
            InitializeComponent();
            txtSN.Text = trackSN.Value.ToString();
            txtPeakPeakBackgroundRatioRatio.Text = (trackPeakBackgroundRatio.Value / 100.0).ToString();
            txtPeptideMinRatio.Text = (trackPeptideMinRatio.Value / 100.0).ToString();
            txtMaxCharge.Text = (10 - trackMaxCharge.Value).ToString();
            _peakParameter = new GlypID.Peaks.clsPeakProcessorParameters();
            _transformParameters = new GlypID.HornTransform.clsHornTransformParameters();
            _peakParameter.PeakBackgroundRatio = Convert.ToDouble(txtPeakPeakBackgroundRatioRatio.Text);
            _peakParameter.SignalToNoiseThreshold = Convert.ToDouble(txtSN.Text);
            _transformParameters.PeptideMinBackgroundRatio = Convert.ToDouble(txtPeptideMinRatio.Text);
            _transformParameters.MaxCharge = Convert.ToInt16(txtMaxCharge.Text);
            _transformParameters.UseAbsolutePeptideIntensity = false;
        }
        public GlypID.Peaks.clsPeakProcessorParameters PeakProcessorParameters
        {
            get { return _peakParameter; }
        }
        public GlypID.HornTransform.clsHornTransformParameters TransformParameters
        {
            get { return _transformParameters; }
        }
        private void trackSN_Scroll(object sender, EventArgs e)
        {
            txtSN.Text = trackSN.Value.ToString();
        }

        private void trackPeakMinRatio_Scroll(object sender, EventArgs e)
        {
            txtPeakPeakBackgroundRatioRatio.Text = (trackPeakBackgroundRatio.Value/100.0).ToString();
        }

        private void trackPeptideMinRatio_Scroll(object sender, EventArgs e)
        {
            txtPeptideMinRatio.Text = (trackPeptideMinRatio.Value / 100.0).ToString();
        }

        private void trackMaxCharge_Scroll(object sender, EventArgs e)
        {
            txtMaxCharge.Text = (11 - trackMaxCharge.Value).ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _transformParameters.UseAbsolutePeptideIntensity = chkPeptideMinAbso.Checked;
            _transformParameters.AbsolutePeptideIntensity = 0.0;
            if (_transformParameters.UseAbsolutePeptideIntensity)
            {
                _transformParameters.AbsolutePeptideIntensity = Convert.ToDouble(txtPeptideMinAbso.Text);
            }
            this.Close();
        }

        private void chkPeptideMinAbso_CheckedChanged(object sender, EventArgs e)
        {
            txtPeptideMinAbso.Enabled = chkPeptideMinAbso.Checked;
            trackPeptideMinRatio.Enabled = !chkPeptideMinAbso.Checked;
            txtPeptideMinRatio.Enabled = !chkPeptideMinAbso.Checked;


        }
    }
}
