namespace COL.MultiGlycan
{
    partial class frmView
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSaveWholeProfile = new System.Windows.Forms.Button();
            this.rdoSingle = new System.Windows.Forms.RadioButton();
            this.rdoFullLC = new System.Windows.Forms.RadioButton();
            this.chkSmooth = new System.Windows.Forms.CheckBox();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.cboGlycan = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.eluctionViewer1 = new COL.ElutionViewer.EluctionViewer();
            this.zgcGlycan = new ZedGraph.ZedGraphControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(3, 4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(101, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load Full Result";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnClear);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveWholeProfile);
            this.splitContainer1.Panel1.Controls.Add(this.rdoSingle);
            this.splitContainer1.Panel1.Controls.Add(this.rdoFullLC);
            this.splitContainer1.Panel1.Controls.Add(this.chkSmooth);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveAll);
            this.splitContainer1.Panel1.Controls.Add(this.cboGlycan);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoad);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1063, 773);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnSaveWholeProfile
            // 
            this.btnSaveWholeProfile.Location = new System.Drawing.Point(961, 3);
            this.btnSaveWholeProfile.Name = "btnSaveWholeProfile";
            this.btnSaveWholeProfile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveWholeProfile.TabIndex = 3;
            this.btnSaveWholeProfile.Text = "Save Whole";
            this.btnSaveWholeProfile.UseVisualStyleBackColor = true;
            this.btnSaveWholeProfile.Visible = false;
            this.btnSaveWholeProfile.Click += new System.EventHandler(this.btnSaveWholeProfile_Click);
            // 
            // rdoSingle
            // 
            this.rdoSingle.AutoSize = true;
            this.rdoSingle.Checked = true;
            this.rdoSingle.Location = new System.Drawing.Point(366, 6);
            this.rdoSingle.Name = "rdoSingle";
            this.rdoSingle.Size = new System.Drawing.Size(90, 17);
            this.rdoSingle.TabIndex = 1;
            this.rdoSingle.TabStop = true;
            this.rdoSingle.Text = "Single Glycan";
            this.rdoSingle.UseVisualStyleBackColor = true;
            // 
            // rdoFullLC
            // 
            this.rdoFullLC.AutoSize = true;
            this.rdoFullLC.Location = new System.Drawing.Point(457, 6);
            this.rdoFullLC.Name = "rdoFullLC";
            this.rdoFullLC.Size = new System.Drawing.Size(116, 17);
            this.rdoFullLC.TabIndex = 2;
            this.rdoFullLC.Text = "Entire LC expriment";
            this.rdoFullLC.UseVisualStyleBackColor = true;
            this.rdoFullLC.CheckedChanged += new System.EventHandler(this.rdoFullLC_CheckedChanged);
            // 
            // chkSmooth
            // 
            this.chkSmooth.AutoSize = true;
            this.chkSmooth.Location = new System.Drawing.Point(812, 8);
            this.chkSmooth.Name = "chkSmooth";
            this.chkSmooth.Size = new System.Drawing.Size(62, 17);
            this.chkSmooth.TabIndex = 2;
            this.chkSmooth.Text = "Smooth";
            this.chkSmooth.UseVisualStyleBackColor = true;
            this.chkSmooth.Visible = false;
            this.chkSmooth.CheckedChanged += new System.EventHandler(this.chkSmooth_CheckedChanged);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Enabled = false;
            this.btnSaveAll.Location = new System.Drawing.Point(880, 4);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAll.TabIndex = 2;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Visible = false;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // cboGlycan
            // 
            this.cboGlycan.FormattingEnabled = true;
            this.cboGlycan.Location = new System.Drawing.Point(186, 4);
            this.cboGlycan.Name = "cboGlycan";
            this.cboGlycan.Size = new System.Drawing.Size(174, 21);
            this.cboGlycan.TabIndex = 1;
            this.cboGlycan.SelectedIndexChanged += new System.EventHandler(this.cboGlycan_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.eluctionViewer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.zgcGlycan);
            this.splitContainer2.Size = new System.Drawing.Size(1063, 739);
            this.splitContainer2.SplitterDistance = 455;
            this.splitContainer2.TabIndex = 1;
            // 
            // eluctionViewer1
            // 
            this.eluctionViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eluctionViewer1.Location = new System.Drawing.Point(0, 0);
            this.eluctionViewer1.Name = "eluctionViewer1";
            this.eluctionViewer1.Size = new System.Drawing.Size(1063, 455);
            this.eluctionViewer1.TabIndex = 0;
            this.eluctionViewer1.StatusUpdated += new System.EventHandler(this.ReSizeZedGraphForm3DPlot);
            // 
            // zgcGlycan
            // 
            this.zgcGlycan.BackColor = System.Drawing.SystemColors.Control;
            this.zgcGlycan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgcGlycan.Location = new System.Drawing.Point(0, 0);
            this.zgcGlycan.Name = "zgcGlycan";
            this.zgcGlycan.ScrollGrace = 0D;
            this.zgcGlycan.ScrollMaxX = 0D;
            this.zgcGlycan.ScrollMaxY = 0D;
            this.zgcGlycan.ScrollMaxY2 = 0D;
            this.zgcGlycan.ScrollMinX = 0D;
            this.zgcGlycan.ScrollMinY = 0D;
            this.zgcGlycan.ScrollMinY2 = 0D;
            this.zgcGlycan.Size = new System.Drawing.Size(1063, 280);
            this.zgcGlycan.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(714, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 773);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmView";
            this.Text = "View";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cboGlycan;
        private ZedGraph.ZedGraphControl zgcGlycan;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.CheckBox chkSmooth;
        private System.Windows.Forms.RadioButton rdoSingle;
        private System.Windows.Forms.RadioButton rdoFullLC;
        private System.Windows.Forms.Button btnSaveWholeProfile;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ElutionViewer.EluctionViewer eluctionViewer1;
        private System.Windows.Forms.Button btnClear;
    }
}