namespace COL.MultiNGlycan
{
    partial class frmProcessing
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
            this.bgWorker_Process = new System.ComponentModel.BackgroundWorker();
            this.lblCurrentScan = new System.Windows.Forms.Label();
            this.lblScan = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumberOfCluster = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNumberOfMerge = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bgWorker_Process
            // 
            this.bgWorker_Process.WorkerReportsProgress = true;
            this.bgWorker_Process.WorkerSupportsCancellation = true;
            this.bgWorker_Process.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_Process_DoWork);
            this.bgWorker_Process.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_Process_RunWorkerCompleted);
            this.bgWorker_Process.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_Process_ProgressChanged);
            // 
            // lblCurrentScan
            // 
            this.lblCurrentScan.AutoSize = true;
            this.lblCurrentScan.Location = new System.Drawing.Point(141, 9);
            this.lblCurrentScan.Name = "lblCurrentScan";
            this.lblCurrentScan.Size = new System.Drawing.Size(30, 13);
            this.lblCurrentScan.TabIndex = 0;
            this.lblCurrentScan.Text = "0 / 0";
            // 
            // lblScan
            // 
            this.lblScan.AutoSize = true;
            this.lblScan.Location = new System.Drawing.Point(0, 9);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(135, 13);
            this.lblScan.TabIndex = 1;
            this.lblScan.Text = "Current Scan / Total Scan:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(317, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Location = new System.Drawing.Point(321, 36);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(24, 13);
            this.lblPercentage.TabIndex = 3;
            this.lblPercentage.Text = "0 %";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Found Clusters:";
            // 
            // lblNumberOfCluster
            // 
            this.lblNumberOfCluster.AutoSize = true;
            this.lblNumberOfCluster.Location = new System.Drawing.Point(76, 67);
            this.lblNumberOfCluster.Name = "lblNumberOfCluster";
            this.lblNumberOfCluster.Size = new System.Drawing.Size(13, 13);
            this.lblNumberOfCluster.TabIndex = 5;
            this.lblNumberOfCluster.Text = "0";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(226, 67);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(95, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status: Processing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Merged Cluster:";
            // 
            // lblNumberOfMerge
            // 
            this.lblNumberOfMerge.AutoSize = true;
            this.lblNumberOfMerge.Location = new System.Drawing.Point(193, 67);
            this.lblNumberOfMerge.Name = "lblNumberOfMerge";
            this.lblNumberOfMerge.Size = new System.Drawing.Size(13, 13);
            this.lblNumberOfMerge.TabIndex = 8;
            this.lblNumberOfMerge.Text = "0";
            // 
            // frmProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 89);
            this.Controls.Add(this.lblNumberOfMerge);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblNumberOfCluster);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblScan);
            this.Controls.Add(this.lblCurrentScan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcessing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Processing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProcessing_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker_Process;
        private System.Windows.Forms.Label lblCurrentScan;
        private System.Windows.Forms.Label lblScan;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNumberOfCluster;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNumberOfMerge;
    }
}