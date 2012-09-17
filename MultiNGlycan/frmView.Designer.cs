namespace COL.MultiNGlycan
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
            this.components = new System.ComponentModel.Container();
            this.btnLoad = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cboGlycan = new System.Windows.Forms.ComboBox();
            this.zgcGlycan = new ZedGraph.ZedGraphControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(3, 4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
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
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveAll);
            this.splitContainer1.Panel1.Controls.Add(this.cboGlycan);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoad);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.zgcGlycan);
            this.splitContainer1.Size = new System.Drawing.Size(1063, 647);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.TabIndex = 1;
            // 
            // cboGlycan
            // 
            this.cboGlycan.FormattingEnabled = true;
            this.cboGlycan.Location = new System.Drawing.Point(84, 6);
            this.cboGlycan.Name = "cboGlycan";
            this.cboGlycan.Size = new System.Drawing.Size(174, 21);
            this.cboGlycan.TabIndex = 1;
            this.cboGlycan.SelectedIndexChanged += new System.EventHandler(this.cboGlycan_SelectedIndexChanged);
            // 
            // zgcGlycan
            // 
            this.zgcGlycan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgcGlycan.Location = new System.Drawing.Point(0, 0);
            this.zgcGlycan.Name = "zgcGlycan";
            this.zgcGlycan.ScrollGrace = 0;
            this.zgcGlycan.ScrollMaxX = 0;
            this.zgcGlycan.ScrollMaxY = 0;
            this.zgcGlycan.ScrollMaxY2 = 0;
            this.zgcGlycan.ScrollMinX = 0;
            this.zgcGlycan.ScrollMinY = 0;
            this.zgcGlycan.ScrollMinY2 = 0;
            this.zgcGlycan.Size = new System.Drawing.Size(1063, 613);
            this.zgcGlycan.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Enabled = false;
            this.btnSaveAll.Location = new System.Drawing.Point(976, 4);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAll.TabIndex = 2;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // frmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 647);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmView";
            this.Text = "View";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cboGlycan;
        private ZedGraph.ZedGraphControl zgcGlycan;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSaveAll;
    }
}