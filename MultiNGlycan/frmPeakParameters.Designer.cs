namespace COL.MultiNGlycan
{
    partial class frmPeakParameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackPeptideMinRatio = new System.Windows.Forms.TrackBar();
            this.trackPeakBackgroundRatio = new System.Windows.Forms.TrackBar();
            this.trackSN = new System.Windows.Forms.TrackBar();
            this.trackMaxCharge = new System.Windows.Forms.TrackBar();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.txtPeakPeakBackgroundRatioRatio = new System.Windows.Forms.TextBox();
            this.txtPeptideMinRatio = new System.Windows.Forms.TextBox();
            this.txtMaxCharge = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkPeptideMinAbso = new System.Windows.Forms.CheckBox();
            this.txtPeptideMinAbso = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackPeptideMinRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPeakBackgroundRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxCharge)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Single to noise ratio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Peak background ratio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Peptide min background ratio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Max charge";
            // 
            // trackPeptideMinRatio
            // 
            this.trackPeptideMinRatio.Location = new System.Drawing.Point(12, 185);
            this.trackPeptideMinRatio.Maximum = 1000;
            this.trackPeptideMinRatio.Name = "trackPeptideMinRatio";
            this.trackPeptideMinRatio.Size = new System.Drawing.Size(271, 45);
            this.trackPeptideMinRatio.TabIndex = 4;
            this.trackPeptideMinRatio.TickFrequency = 10;
            this.trackPeptideMinRatio.Value = 200;
            this.trackPeptideMinRatio.Scroll += new System.EventHandler(this.trackPeptideMinRatio_Scroll);
            // 
            // trackPeakBackgroundRatio
            // 
            this.trackPeakBackgroundRatio.Location = new System.Drawing.Point(12, 108);
            this.trackPeakBackgroundRatio.Maximum = 1000;
            this.trackPeakBackgroundRatio.Minimum = 1;
            this.trackPeakBackgroundRatio.Name = "trackPeakBackgroundRatio";
            this.trackPeakBackgroundRatio.Size = new System.Drawing.Size(271, 45);
            this.trackPeakBackgroundRatio.TabIndex = 5;
            this.trackPeakBackgroundRatio.TickFrequency = 10;
            this.trackPeakBackgroundRatio.Value = 100;
            this.trackPeakBackgroundRatio.Scroll += new System.EventHandler(this.trackPeakMinRatio_Scroll);
            // 
            // trackSN
            // 
            this.trackSN.Location = new System.Drawing.Point(12, 48);
            this.trackSN.Maximum = 100;
            this.trackSN.Name = "trackSN";
            this.trackSN.Size = new System.Drawing.Size(271, 45);
            this.trackSN.TabIndex = 6;
            this.trackSN.Value = 2;
            this.trackSN.Scroll += new System.EventHandler(this.trackSN_Scroll);
            // 
            // trackMaxCharge
            // 
            this.trackMaxCharge.Location = new System.Drawing.Point(12, 307);
            this.trackMaxCharge.Minimum = 1;
            this.trackMaxCharge.Name = "trackMaxCharge";
            this.trackMaxCharge.Size = new System.Drawing.Size(271, 45);
            this.trackMaxCharge.TabIndex = 7;
            this.trackMaxCharge.Value = 5;
            this.trackMaxCharge.Scroll += new System.EventHandler(this.trackMaxCharge_Scroll);
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(289, 48);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(57, 20);
            this.txtSN.TabIndex = 8;
            // 
            // txtPeakPeakBackgroundRatioRatio
            // 
            this.txtPeakPeakBackgroundRatioRatio.Location = new System.Drawing.Point(289, 108);
            this.txtPeakPeakBackgroundRatioRatio.Name = "txtPeakPeakBackgroundRatioRatio";
            this.txtPeakPeakBackgroundRatioRatio.Size = new System.Drawing.Size(57, 20);
            this.txtPeakPeakBackgroundRatioRatio.TabIndex = 9;
            // 
            // txtPeptideMinRatio
            // 
            this.txtPeptideMinRatio.Location = new System.Drawing.Point(289, 185);
            this.txtPeptideMinRatio.Name = "txtPeptideMinRatio";
            this.txtPeptideMinRatio.Size = new System.Drawing.Size(57, 20);
            this.txtPeptideMinRatio.TabIndex = 10;
            // 
            // txtMaxCharge
            // 
            this.txtMaxCharge.Location = new System.Drawing.Point(289, 307);
            this.txtMaxCharge.Name = "txtMaxCharge";
            this.txtMaxCharge.Size = new System.Drawing.Size(57, 20);
            this.txtMaxCharge.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "More Peak(slow)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(200, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Less Peak(Fast)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "(this value * background = threshold)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(291, 333);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkPeptideMinAbso
            // 
            this.chkPeptideMinAbso.AutoSize = true;
            this.chkPeptideMinAbso.Location = new System.Drawing.Point(18, 236);
            this.chkPeptideMinAbso.Name = "chkPeptideMinAbso";
            this.chkPeptideMinAbso.Size = new System.Drawing.Size(167, 17);
            this.chkPeptideMinAbso.TabIndex = 16;
            this.chkPeptideMinAbso.Text = "Use absolute peptide intensity";
            this.chkPeptideMinAbso.UseVisualStyleBackColor = true;
            this.chkPeptideMinAbso.CheckedChanged += new System.EventHandler(this.chkPeptideMinAbso_CheckedChanged);
            // 
            // txtPeptideMinAbso
            // 
            this.txtPeptideMinAbso.Enabled = false;
            this.txtPeptideMinAbso.Location = new System.Drawing.Point(246, 233);
            this.txtPeptideMinAbso.Name = "txtPeptideMinAbso";
            this.txtPeptideMinAbso.Size = new System.Drawing.Size(100, 20);
            this.txtPeptideMinAbso.TabIndex = 18;
            // 
            // frmPeakParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 360);
            this.Controls.Add(this.txtPeptideMinAbso);
            this.Controls.Add(this.chkPeptideMinAbso);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMaxCharge);
            this.Controls.Add(this.txtPeptideMinRatio);
            this.Controls.Add(this.txtPeakPeakBackgroundRatioRatio);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.trackMaxCharge);
            this.Controls.Add(this.trackSN);
            this.Controls.Add(this.trackPeakBackgroundRatio);
            this.Controls.Add(this.trackPeptideMinRatio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmPeakParameters";
            this.Text = "Peak Parameters";
            ((System.ComponentModel.ISupportInitialize)(this.trackPeptideMinRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPeakBackgroundRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxCharge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackPeptideMinRatio;
        private System.Windows.Forms.TrackBar trackPeakBackgroundRatio;
        private System.Windows.Forms.TrackBar trackSN;
        private System.Windows.Forms.TrackBar trackMaxCharge;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.TextBox txtPeakPeakBackgroundRatioRatio;
        private System.Windows.Forms.TextBox txtPeptideMinRatio;
        private System.Windows.Forms.TextBox txtMaxCharge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkPeptideMinAbso;
        private System.Windows.Forms.TextBox txtPeptideMinAbso;
    }
}