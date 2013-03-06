namespace COL.MultiGlycan
{
    partial class frmCalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalculator));
            this.label1 = new System.Windows.Forms.Label();
            this.txtHexNAc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.txtdeHex = new System.Windows.Forms.TextBox();
            this.txtSia = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAdduct = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblDescribtion = new System.Windows.Forms.Label();
            this.chkReducedReducingEnd = new System.Windows.Forms.CheckBox();
            this.chkPermethylated = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HexNAc";
            // 
            // txtHexNAc
            // 
            this.txtHexNAc.Location = new System.Drawing.Point(53, 17);
            this.txtHexNAc.Name = "txtHexNAc";
            this.txtHexNAc.Size = new System.Drawing.Size(100, 20);
            this.txtHexNAc.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hex";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "deHex";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sia";
            // 
            // txtHex
            // 
            this.txtHex.Location = new System.Drawing.Point(53, 42);
            this.txtHex.Name = "txtHex";
            this.txtHex.Size = new System.Drawing.Size(100, 20);
            this.txtHex.TabIndex = 1;
            // 
            // txtdeHex
            // 
            this.txtdeHex.Location = new System.Drawing.Point(53, 67);
            this.txtdeHex.Name = "txtdeHex";
            this.txtdeHex.Size = new System.Drawing.Size(100, 20);
            this.txtdeHex.TabIndex = 2;
            // 
            // txtSia
            // 
            this.txtSia.Location = new System.Drawing.Point(53, 92);
            this.txtSia.Name = "txtSia";
            this.txtSia.Size = new System.Drawing.Size(100, 20);
            this.txtSia.TabIndex = 3;
            // 
            // txtResult
            // 
            this.txtResult.Enabled = false;
            this.txtResult.Location = new System.Drawing.Point(172, 92);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(440, 73);
            this.txtResult.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Adduct";
            // 
            // txtAdduct
            // 
            this.txtAdduct.Location = new System.Drawing.Point(53, 116);
            this.txtAdduct.Name = "txtAdduct";
            this.txtAdduct.Size = new System.Drawing.Size(100, 20);
            this.txtAdduct.TabIndex = 4;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(172, 171);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 11;
            this.btnCalc.Text = "Calculate";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // lblDescribtion
            // 
            this.lblDescribtion.AutoSize = true;
            this.lblDescribtion.Location = new System.Drawing.Point(169, 10);
            this.lblDescribtion.Name = "lblDescribtion";
            this.lblDescribtion.Size = new System.Drawing.Size(451, 78);
            this.lblDescribtion.TabIndex = 12;
            this.lblDescribtion.Text = resources.GetString("lblDescribtion.Text");
            // 
            // chkReducedReducingEnd
            // 
            this.chkReducedReducingEnd.AutoSize = true;
            this.chkReducedReducingEnd.Checked = true;
            this.chkReducedReducingEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReducedReducingEnd.Location = new System.Drawing.Point(15, 146);
            this.chkReducedReducingEnd.Name = "chkReducedReducingEnd";
            this.chkReducedReducingEnd.Size = new System.Drawing.Size(141, 17);
            this.chkReducedReducingEnd.TabIndex = 14;
            this.chkReducedReducingEnd.Text = "Reduced Reducing End";
            this.chkReducedReducingEnd.UseVisualStyleBackColor = true;
            // 
            // chkPermethylated
            // 
            this.chkPermethylated.AutoSize = true;
            this.chkPermethylated.Checked = true;
            this.chkPermethylated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPermethylated.Location = new System.Drawing.Point(15, 171);
            this.chkPermethylated.Name = "chkPermethylated";
            this.chkPermethylated.Size = new System.Drawing.Size(93, 17);
            this.chkPermethylated.TabIndex = 15;
            this.chkPermethylated.Text = "Permethylated";
            // 
            // frmCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 200);
            this.Controls.Add(this.chkReducedReducingEnd);
            this.Controls.Add(this.chkPermethylated);
            this.Controls.Add(this.lblDescribtion);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.txtAdduct);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtSia);
            this.Controls.Add(this.txtdeHex);
            this.Controls.Add(this.txtHex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHexNAc);
            this.Controls.Add(this.label1);
            this.Name = "frmCalculator";
            this.Text = "Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHexNAc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHex;
        private System.Windows.Forms.TextBox txtdeHex;
        private System.Windows.Forms.TextBox txtSia;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAdduct;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label lblDescribtion;
        private System.Windows.Forms.CheckBox chkReducedReducingEnd;
        private System.Windows.Forms.CheckBox chkPermethylated;
    }
}