namespace COL.MultiNGlycan
{
    partial class frmMainESI
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
            this.btnBrowseRaw = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtRawFile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGlycanList = new System.Windows.Forms.TextBox();
            this.rdoDefaultList = new System.Windows.Forms.RadioButton();
            this.rdoUserList = new System.Windows.Forms.RadioButton();
            this.btnBrowseGlycan = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPPM = new System.Windows.Forms.TextBox();
            this.chkPermethylated = new System.Windows.Forms.CheckBox();
            this.chkReducedReducingEnd = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGlycanPPM = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartScan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndScan = new System.Windows.Forms.TextBox();
            this.rdoAllRaw = new System.Windows.Forms.RadioButton();
            this.rdoScanNum = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSingleCluster = new System.Windows.Forms.CheckBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseRaw
            // 
            this.btnBrowseRaw.Location = new System.Drawing.Point(447, 12);
            this.btnBrowseRaw.Name = "btnBrowseRaw";
            this.btnBrowseRaw.Size = new System.Drawing.Size(25, 23);
            this.btnBrowseRaw.TabIndex = 1;
            this.btnBrowseRaw.Text = "...";
            this.btnBrowseRaw.UseVisualStyleBackColor = true;
            this.btnBrowseRaw.Click += new System.EventHandler(this.btnBrowseRaw_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(8, 17);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(51, 13);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "Raw File:";
            // 
            // txtRawFile
            // 
            this.txtRawFile.Location = new System.Drawing.Point(65, 14);
            this.txtRawFile.Name = "txtRawFile";
            this.txtRawFile.Size = new System.Drawing.Size(376, 20);
            this.txtRawFile.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGlycanList);
            this.groupBox1.Controls.Add(this.rdoDefaultList);
            this.groupBox1.Controls.Add(this.rdoUserList);
            this.groupBox1.Controls.Add(this.btnBrowseGlycan);
            this.groupBox1.Location = new System.Drawing.Point(6, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 44);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Glycan List";
            // 
            // txtGlycanList
            // 
            this.txtGlycanList.Enabled = false;
            this.txtGlycanList.Location = new System.Drawing.Point(196, 16);
            this.txtGlycanList.Name = "txtGlycanList";
            this.txtGlycanList.Size = new System.Drawing.Size(246, 20);
            this.txtGlycanList.TabIndex = 8;
            // 
            // rdoDefaultList
            // 
            this.rdoDefaultList.AutoSize = true;
            this.rdoDefaultList.Checked = true;
            this.rdoDefaultList.Location = new System.Drawing.Point(12, 19);
            this.rdoDefaultList.Name = "rdoDefaultList";
            this.rdoDefaultList.Size = new System.Drawing.Size(78, 17);
            this.rdoDefaultList.TabIndex = 6;
            this.rdoDefaultList.TabStop = true;
            this.rdoDefaultList.Text = "Default List";
            this.rdoDefaultList.UseVisualStyleBackColor = true;
            this.rdoDefaultList.CheckedChanged += new System.EventHandler(this.rdoDefaultList_CheckedChanged);
            // 
            // rdoUserList
            // 
            this.rdoUserList.AutoSize = true;
            this.rdoUserList.Location = new System.Drawing.Point(100, 19);
            this.rdoUserList.Name = "rdoUserList";
            this.rdoUserList.Size = new System.Drawing.Size(99, 17);
            this.rdoUserList.TabIndex = 7;
            this.rdoUserList.Text = "Glycan List File:";
            this.rdoUserList.UseVisualStyleBackColor = true;
            // 
            // btnBrowseGlycan
            // 
            this.btnBrowseGlycan.Enabled = false;
            this.btnBrowseGlycan.Location = new System.Drawing.Point(448, 14);
            this.btnBrowseGlycan.Name = "btnBrowseGlycan";
            this.btnBrowseGlycan.Size = new System.Drawing.Size(25, 22);
            this.btnBrowseGlycan.TabIndex = 9;
            this.btnBrowseGlycan.Text = "...";
            this.btnBrowseGlycan.UseVisualStyleBackColor = true;
            this.btnBrowseGlycan.Click += new System.EventHandler(this.btnBrowseGlycan_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Enabled = false;
            this.btnMerge.Location = new System.Drawing.Point(411, 164);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 14;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mass tol(PPM):";
            // 
            // txtPPM
            // 
            this.txtPPM.Location = new System.Drawing.Point(101, 138);
            this.txtPPM.Name = "txtPPM";
            this.txtPPM.Size = new System.Drawing.Size(35, 20);
            this.txtPPM.TabIndex = 10;
            this.txtPPM.Text = "5";
            // 
            // chkPermethylated
            // 
            this.chkPermethylated.AutoSize = true;
            this.chkPermethylated.Checked = true;
            this.chkPermethylated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPermethylated.Location = new System.Drawing.Point(142, 164);
            this.chkPermethylated.Name = "chkPermethylated";
            this.chkPermethylated.Size = new System.Drawing.Size(93, 17);
            this.chkPermethylated.TabIndex = 13;
            this.chkPermethylated.Text = "Permethylated";
            this.chkPermethylated.UseVisualStyleBackColor = true;
            // 
            // chkReducedReducingEnd
            // 
            this.chkReducedReducingEnd.AutoSize = true;
            this.chkReducedReducingEnd.Checked = true;
            this.chkReducedReducingEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReducedReducingEnd.Location = new System.Drawing.Point(142, 141);
            this.chkReducedReducingEnd.Name = "chkReducedReducingEnd";
            this.chkReducedReducingEnd.Size = new System.Drawing.Size(141, 17);
            this.chkReducedReducingEnd.TabIndex = 12;
            this.chkReducedReducingEnd.Text = "Reduced Reducing End";
            this.chkReducedReducingEnd.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Glycan tol(PPM):";
            // 
            // txtGlycanPPM
            // 
            this.txtGlycanPPM.Location = new System.Drawing.Point(101, 165);
            this.txtGlycanPPM.Name = "txtGlycanPPM";
            this.txtGlycanPPM.Size = new System.Drawing.Size(35, 20);
            this.txtGlycanPPM.TabIndex = 11;
            this.txtGlycanPPM.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Start Scan:";
            // 
            // txtStartScan
            // 
            this.txtStartScan.Location = new System.Drawing.Point(255, 44);
            this.txtStartScan.Name = "txtStartScan";
            this.txtStartScan.Size = new System.Drawing.Size(53, 20);
            this.txtStartScan.TabIndex = 4;
            this.txtStartScan.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(314, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "End Scan:";
            // 
            // txtEndScan
            // 
            this.txtEndScan.Location = new System.Drawing.Point(377, 45);
            this.txtEndScan.Name = "txtEndScan";
            this.txtEndScan.Size = new System.Drawing.Size(53, 20);
            this.txtEndScan.TabIndex = 5;
            this.txtEndScan.Text = "99999";
            // 
            // rdoAllRaw
            // 
            this.rdoAllRaw.AutoSize = true;
            this.rdoAllRaw.Checked = true;
            this.rdoAllRaw.Location = new System.Drawing.Point(11, 45);
            this.rdoAllRaw.Name = "rdoAllRaw";
            this.rdoAllRaw.Size = new System.Drawing.Size(83, 17);
            this.rdoAllRaw.TabIndex = 2;
            this.rdoAllRaw.TabStop = true;
            this.rdoAllRaw.Text = "All MS Scan";
            this.rdoAllRaw.UseVisualStyleBackColor = true;
            this.rdoAllRaw.CheckedChanged += new System.EventHandler(this.rdoAllRaw_CheckedChanged);
            // 
            // rdoScanNum
            // 
            this.rdoScanNum.AutoSize = true;
            this.rdoScanNum.Location = new System.Drawing.Point(102, 45);
            this.rdoScanNum.Name = "rdoScanNum";
            this.rdoScanNum.Size = new System.Drawing.Size(85, 17);
            this.rdoScanNum.TabIndex = 3;
            this.rdoScanNum.Text = "Scan Range";
            this.rdoScanNum.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRawFile);
            this.groupBox2.Controls.Add(this.txtEndScan);
            this.groupBox2.Controls.Add(this.rdoScanNum);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnBrowseRaw);
            this.groupBox2.Controls.Add(this.txtStartScan);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.rdoAllRaw);
            this.groupBox2.Controls.Add(this.lblFile);
            this.groupBox2.Location = new System.Drawing.Point(6, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 71);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Raw File";
            // 
            // chkSingleCluster
            // 
            this.chkSingleCluster.AutoSize = true;
            this.chkSingleCluster.Checked = true;
            this.chkSingleCluster.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingleCluster.Location = new System.Drawing.Point(289, 141);
            this.chkSingleCluster.Name = "chkSingleCluster";
            this.chkSingleCluster.Size = new System.Drawing.Size(191, 17);
            this.chkSingleCluster.TabIndex = 18;
            this.chkSingleCluster.Text = "Include non-modified glycans(NH4)";
            this.chkSingleCluster.UseVisualStyleBackColor = true;
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(330, 163);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 19;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // frmMainESI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 194);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.chkSingleCluster);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtGlycanPPM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkReducedReducingEnd);
            this.Controls.Add(this.chkPermethylated);
            this.Controls.Add(this.txtPPM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMainESI";
            this.Text = "MultiNglycan-ESI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseRaw;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtRawFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoDefaultList;
        private System.Windows.Forms.RadioButton rdoUserList;
        private System.Windows.Forms.Button btnBrowseGlycan;
        private System.Windows.Forms.TextBox txtGlycanList;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPPM;
        private System.Windows.Forms.CheckBox chkPermethylated;
        private System.Windows.Forms.CheckBox chkReducedReducingEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGlycanPPM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartScan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEndScan;
        private System.Windows.Forms.RadioButton rdoAllRaw;
        private System.Windows.Forms.RadioButton rdoScanNum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkSingleCluster;
        private System.Windows.Forms.Button btnSetting;
    }
}