namespace COL.MultiGlycan
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartScan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndScan = new System.Windows.Forms.TextBox();
            this.rdoAllRaw = new System.Windows.Forms.RadioButton();
            this.rdoScanNum = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.chkMergeDffCharge = new System.Windows.Forms.CheckBox();
            this.cboCPU = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMergeTest = new System.Windows.Forms.Button();
            this.txtMaxLCTime = new System.Windows.Forms.TextBox();
            this.chkAdductNH4 = new System.Windows.Forms.CheckBox();
            this.chkAdductNa = new System.Windows.Forms.CheckBox();
            this.chkAdductK = new System.Windows.Forms.CheckBox();
            this.chkAdductUser = new System.Windows.Forms.CheckBox();
            this.txtAdductMass = new System.Windows.Forms.TextBox();
            this.chkAdductProton = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtScanCount = new System.Windows.Forms.TextBox();
            this.chkScanCount = new System.Windows.Forms.CheckBox();
            this.txtAbundanceMin = new System.Windows.Forms.TextBox();
            this.chkAbundance = new System.Windows.Forms.CheckBox();
            this.chkLCMax = new System.Windows.Forms.CheckBox();
            this.chkLCMin = new System.Windows.Forms.CheckBox();
            this.txtMinLCTime = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eluctionProfileViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.massCalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtGlycanPPM = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseRaw
            // 
            this.btnBrowseRaw.Location = new System.Drawing.Point(405, 12);
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
            this.txtRawFile.Size = new System.Drawing.Size(335, 20);
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
            this.groupBox1.Location = new System.Drawing.Point(6, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 44);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Glycan List";
            // 
            // txtGlycanList
            // 
            this.txtGlycanList.Enabled = false;
            this.txtGlycanList.Location = new System.Drawing.Point(196, 16);
            this.txtGlycanList.Name = "txtGlycanList";
            this.txtGlycanList.Size = new System.Drawing.Size(204, 20);
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
            this.btnBrowseGlycan.Location = new System.Drawing.Point(406, 14);
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
            this.btnMerge.Location = new System.Drawing.Point(390, 344);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(53, 23);
            this.btnMerge.TabIndex = 14;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mass tol(PPM):";
            // 
            // txtPPM
            // 
            this.txtPPM.Location = new System.Drawing.Point(95, 16);
            this.txtPPM.Name = "txtPPM";
            this.txtPPM.Size = new System.Drawing.Size(21, 20);
            this.txtPPM.TabIndex = 10;
            this.txtPPM.Text = "5";
            // 
            // chkPermethylated
            // 
            this.chkPermethylated.AutoSize = true;
            this.chkPermethylated.Checked = true;
            this.chkPermethylated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPermethylated.Location = new System.Drawing.Point(13, 44);
            this.chkPermethylated.Name = "chkPermethylated";
            this.chkPermethylated.Size = new System.Drawing.Size(93, 17);
            this.chkPermethylated.TabIndex = 13;
            this.chkPermethylated.Text = "Permethylated";
            // 
            // chkReducedReducingEnd
            // 
            this.chkReducedReducingEnd.AutoSize = true;
            this.chkReducedReducingEnd.Checked = true;
            this.chkReducedReducingEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReducedReducingEnd.Location = new System.Drawing.Point(13, 19);
            this.chkReducedReducingEnd.Name = "chkReducedReducingEnd";
            this.chkReducedReducingEnd.Size = new System.Drawing.Size(141, 17);
            this.chkReducedReducingEnd.TabIndex = 12;
            this.chkReducedReducingEnd.Text = "Reduced Reducing End";
            this.chkReducedReducingEnd.UseVisualStyleBackColor = true;
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
            this.txtStartScan.Enabled = false;
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
            this.txtEndScan.Enabled = false;
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
            this.groupBox2.Location = new System.Drawing.Point(6, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 71);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Raw File";
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(390, 319);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(54, 23);
            this.btnSetting.TabIndex = 19;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // chkMergeDffCharge
            // 
            this.chkMergeDffCharge.AutoSize = true;
            this.chkMergeDffCharge.Checked = true;
            this.chkMergeDffCharge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMergeDffCharge.Location = new System.Drawing.Point(10, 19);
            this.chkMergeDffCharge.Name = "chkMergeDffCharge";
            this.chkMergeDffCharge.Size = new System.Drawing.Size(167, 17);
            this.chkMergeDffCharge.TabIndex = 20;
            this.chkMergeDffCharge.Text = "Merge different charge glycan";
            this.chkMergeDffCharge.UseVisualStyleBackColor = true;
            // 
            // cboCPU
            // 
            this.cboCPU.FormattingEnabled = true;
            this.cboCPU.Location = new System.Drawing.Point(301, -4);
            this.cboCPU.Name = "cboCPU";
            this.cboCPU.Size = new System.Drawing.Size(30, 21);
            this.cboCPU.TabIndex = 25;
            this.cboCPU.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(211, -1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "No. of processor:";
            this.label7.Visible = false;
            // 
            // btnMergeTest
            // 
            this.btnMergeTest.Location = new System.Drawing.Point(369, -4);
            this.btnMergeTest.Name = "btnMergeTest";
            this.btnMergeTest.Size = new System.Drawing.Size(75, 23);
            this.btnMergeTest.TabIndex = 28;
            this.btnMergeTest.Text = "MergeTest";
            this.btnMergeTest.UseVisualStyleBackColor = true;
            this.btnMergeTest.Visible = false;
            this.btnMergeTest.Click += new System.EventHandler(this.btnMergeTest_Click);
            // 
            // txtMaxLCTime
            // 
            this.txtMaxLCTime.Location = new System.Drawing.Point(175, 67);
            this.txtMaxLCTime.Name = "txtMaxLCTime";
            this.txtMaxLCTime.Size = new System.Drawing.Size(38, 20);
            this.txtMaxLCTime.TabIndex = 31;
            this.txtMaxLCTime.Text = "8";
            // 
            // chkAdductNH4
            // 
            this.chkAdductNH4.AutoSize = true;
            this.chkAdductNH4.Location = new System.Drawing.Point(7, 40);
            this.chkAdductNH4.Name = "chkAdductNH4";
            this.chkAdductNH4.Size = new System.Drawing.Size(108, 17);
            this.chkAdductNH4.TabIndex = 32;
            this.chkAdductNH4.Text = "Ammonium (NH4)";
            this.chkAdductNH4.UseVisualStyleBackColor = true;
            // 
            // chkAdductNa
            // 
            this.chkAdductNa.AutoSize = true;
            this.chkAdductNa.Location = new System.Drawing.Point(7, 61);
            this.chkAdductNa.Name = "chkAdductNa";
            this.chkAdductNa.Size = new System.Drawing.Size(84, 17);
            this.chkAdductNa.TabIndex = 33;
            this.chkAdductNa.Text = "Sodium (Na)";
            this.chkAdductNa.UseVisualStyleBackColor = true;
            // 
            // chkAdductK
            // 
            this.chkAdductK.AutoSize = true;
            this.chkAdductK.Location = new System.Drawing.Point(7, 82);
            this.chkAdductK.Name = "chkAdductK";
            this.chkAdductK.Size = new System.Drawing.Size(90, 17);
            this.chkAdductK.TabIndex = 34;
            this.chkAdductK.Text = "Potassium (K)";
            this.chkAdductK.UseVisualStyleBackColor = true;
            // 
            // chkAdductUser
            // 
            this.chkAdductUser.AutoSize = true;
            this.chkAdductUser.Location = new System.Drawing.Point(7, 103);
            this.chkAdductUser.Name = "chkAdductUser";
            this.chkAdductUser.Size = new System.Drawing.Size(87, 17);
            this.chkAdductUser.TabIndex = 35;
            this.chkAdductUser.Text = "User adduct:";
            this.chkAdductUser.UseVisualStyleBackColor = true;
            this.chkAdductUser.CheckedChanged += new System.EventHandler(this.chkAdductUser_CheckedChanged);
            // 
            // txtAdductMass
            // 
            this.txtAdductMass.Enabled = false;
            this.txtAdductMass.Location = new System.Drawing.Point(92, 100);
            this.txtAdductMass.Name = "txtAdductMass";
            this.txtAdductMass.Size = new System.Drawing.Size(38, 20);
            this.txtAdductMass.TabIndex = 36;
            // 
            // chkAdductProton
            // 
            this.chkAdductProton.AutoSize = true;
            this.chkAdductProton.Checked = true;
            this.chkAdductProton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAdductProton.Enabled = false;
            this.chkAdductProton.Location = new System.Drawing.Point(7, 19);
            this.chkAdductProton.Name = "chkAdductProton";
            this.chkAdductProton.Size = new System.Drawing.Size(74, 17);
            this.chkAdductProton.TabIndex = 37;
            this.chkAdductProton.Text = "Proton (H)";
            this.chkAdductProton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.chkReducedReducingEnd);
            this.groupBox3.Controls.Add(this.chkPermethylated);
            this.groupBox3.Location = new System.Drawing.Point(5, 155);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(164, 210);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Expriment";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkAdductProton);
            this.groupBox4.Controls.Add(this.chkAdductNH4);
            this.groupBox4.Controls.Add(this.chkAdductNa);
            this.groupBox4.Controls.Add(this.chkAdductK);
            this.groupBox4.Controls.Add(this.txtAdductMass);
            this.groupBox4.Controls.Add(this.chkAdductUser);
            this.groupBox4.Location = new System.Drawing.Point(12, 67);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(142, 129);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Adduct";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtPPM);
            this.groupBox5.Location = new System.Drawing.Point(175, 319);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(124, 43);
            this.groupBox5.TabIndex = 39;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Mass accurecy";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtScanCount);
            this.groupBox6.Controls.Add(this.chkScanCount);
            this.groupBox6.Controls.Add(this.txtAbundanceMin);
            this.groupBox6.Controls.Add(this.chkAbundance);
            this.groupBox6.Controls.Add(this.chkLCMax);
            this.groupBox6.Controls.Add(this.chkLCMin);
            this.groupBox6.Controls.Add(this.txtMinLCTime);
            this.groupBox6.Controls.Add(this.chkMergeDffCharge);
            this.groupBox6.Controls.Add(this.txtMaxLCTime);
            this.groupBox6.Location = new System.Drawing.Point(175, 158);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(269, 155);
            this.groupBox6.TabIndex = 40;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Filter and output";
            // 
            // txtScanCount
            // 
            this.txtScanCount.Location = new System.Drawing.Point(175, 117);
            this.txtScanCount.Name = "txtScanCount";
            this.txtScanCount.Size = new System.Drawing.Size(38, 20);
            this.txtScanCount.TabIndex = 39;
            this.txtScanCount.Text = "10";
            // 
            // chkScanCount
            // 
            this.chkScanCount.AutoSize = true;
            this.chkScanCount.Checked = true;
            this.chkScanCount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScanCount.Location = new System.Drawing.Point(10, 119);
            this.chkScanCount.Name = "chkScanCount";
            this.chkScanCount.Size = new System.Drawing.Size(143, 17);
            this.chkScanCount.TabIndex = 38;
            this.chkScanCount.Text = "Minimum number of scan";
            this.chkScanCount.UseVisualStyleBackColor = true;
            this.chkScanCount.CheckedChanged += new System.EventHandler(this.chkScanCount_CheckedChanged);
            // 
            // txtAbundanceMin
            // 
            this.txtAbundanceMin.Location = new System.Drawing.Point(140, 92);
            this.txtAbundanceMin.Name = "txtAbundanceMin";
            this.txtAbundanceMin.Size = new System.Drawing.Size(73, 20);
            this.txtAbundanceMin.TabIndex = 37;
            this.txtAbundanceMin.Text = "1000000";
            // 
            // chkAbundance
            // 
            this.chkAbundance.AutoSize = true;
            this.chkAbundance.Checked = true;
            this.chkAbundance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAbundance.Location = new System.Drawing.Point(10, 94);
            this.chkAbundance.Name = "chkAbundance";
            this.chkAbundance.Size = new System.Drawing.Size(124, 17);
            this.chkAbundance.TabIndex = 36;
            this.chkAbundance.Text = "Minimum abundance";
            this.chkAbundance.UseVisualStyleBackColor = true;
            this.chkAbundance.CheckedChanged += new System.EventHandler(this.chkAbundance_CheckedChanged);
            // 
            // chkLCMax
            // 
            this.chkLCMax.AutoSize = true;
            this.chkLCMax.Checked = true;
            this.chkLCMax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLCMax.Location = new System.Drawing.Point(10, 69);
            this.chkLCMax.Name = "chkLCMax";
            this.chkLCMax.Size = new System.Drawing.Size(161, 17);
            this.chkLCMax.TabIndex = 35;
            this.chkLCMax.Text = "Glycan LC maximum (minute)";
            this.chkLCMax.UseVisualStyleBackColor = true;
            this.chkLCMax.CheckedChanged += new System.EventHandler(this.chkLCMax_CheckedChanged);
            // 
            // chkLCMin
            // 
            this.chkLCMin.AutoSize = true;
            this.chkLCMin.Checked = true;
            this.chkLCMin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLCMin.Location = new System.Drawing.Point(10, 44);
            this.chkLCMin.Name = "chkLCMin";
            this.chkLCMin.Size = new System.Drawing.Size(158, 17);
            this.chkLCMin.TabIndex = 34;
            this.chkLCMin.Text = "Glycan LC minimum (minute)";
            this.chkLCMin.UseVisualStyleBackColor = true;
            this.chkLCMin.CheckedChanged += new System.EventHandler(this.chkLCMin_CheckedChanged);
            // 
            // txtMinLCTime
            // 
            this.txtMinLCTime.Location = new System.Drawing.Point(175, 42);
            this.txtMinLCTime.Name = "txtMinLCTime";
            this.txtMinLCTime.Size = new System.Drawing.Size(38, 20);
            this.txtMinLCTime.TabIndex = 32;
            this.txtMinLCTime.Text = "0.15";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(450, 24);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eluctionProfileViewerToolStripMenuItem,
            this.massCalculatorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // eluctionProfileViewerToolStripMenuItem
            // 
            this.eluctionProfileViewerToolStripMenuItem.Name = "eluctionProfileViewerToolStripMenuItem";
            this.eluctionProfileViewerToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.eluctionProfileViewerToolStripMenuItem.Text = "Eluction profile viewer";
            this.eluctionProfileViewerToolStripMenuItem.Click += new System.EventHandler(this.eluctionProfileViewerToolStripMenuItem_Click);
            // 
            // massCalculatorToolStripMenuItem
            // 
            this.massCalculatorToolStripMenuItem.Name = "massCalculatorToolStripMenuItem";
            this.massCalculatorToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.massCalculatorToolStripMenuItem.Text = "Mass Calculator";
            this.massCalculatorToolStripMenuItem.Click += new System.EventHandler(this.massCalculatorToolStripMenuItem_Click);
            // 
            // txtGlycanPPM
            // 
            this.txtGlycanPPM.Location = new System.Drawing.Point(170, 0);
            this.txtGlycanPPM.Name = "txtGlycanPPM";
            this.txtGlycanPPM.Size = new System.Drawing.Size(21, 20);
            this.txtGlycanPPM.TabIndex = 11;
            this.txtGlycanPPM.Text = "10";
            this.txtGlycanPPM.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Glycan tol(PPM):";
            this.label2.Visible = false;
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Location = new System.Drawing.Point(308, 334);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(44, 17);
            this.chkLog.TabIndex = 40;
            this.chkLog.Text = "Log";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // frmMainESI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 375);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGlycanPPM);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnMergeTest);
            this.Controls.Add(this.cboCPU);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainESI";
            this.Text = "MultiNglycan-ESI v0.9.2.2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartScan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEndScan;
        private System.Windows.Forms.RadioButton rdoAllRaw;
        private System.Windows.Forms.RadioButton rdoScanNum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.CheckBox chkMergeDffCharge;
        private System.Windows.Forms.ComboBox cboCPU;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnMergeTest;
        private System.Windows.Forms.TextBox txtMaxLCTime;
        private System.Windows.Forms.CheckBox chkAdductNH4;
        private System.Windows.Forms.CheckBox chkAdductNa;
        private System.Windows.Forms.CheckBox chkAdductK;
        private System.Windows.Forms.CheckBox chkAdductUser;
        private System.Windows.Forms.TextBox txtAdductMass;
        private System.Windows.Forms.CheckBox chkAdductProton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtMinLCTime;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eluctionProfileViewerToolStripMenuItem;
        private System.Windows.Forms.TextBox txtGlycanPPM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAbundanceMin;
        private System.Windows.Forms.CheckBox chkAbundance;
        private System.Windows.Forms.CheckBox chkLCMax;
        private System.Windows.Forms.CheckBox chkLCMin;
        private System.Windows.Forms.TextBox txtScanCount;
        private System.Windows.Forms.CheckBox chkScanCount;
        private System.Windows.Forms.ToolStripMenuItem massCalculatorToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkLog;
    }
}