namespace BJMT.RsspII4net.ITest.Presentation
{
    partial class ClientConfigControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudSeqNoThreshold = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudAppType = new System.Windows.Forms.NumericUpDown();
            this.nudLocalID = new System.Windows.Forms.NumericUpDown();
            this.nudEcInterval = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxDefenseTech = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxEquipmentType = new System.Windows.Forms.ComboBox();
            this.labelAppType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nudServer2ID = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.txt4ServerIP = new System.Windows.Forms.ComboBox();
            this.txt3ServerIP = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.cbx3LocalIP = new System.Windows.Forms.ComboBox();
            this.cbx4LocalIP = new System.Windows.Forms.ComboBox();
            this.txt4ServerPort = new System.Windows.Forms.TextBox();
            this.txt3ServerPort = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nudServer1ID = new System.Windows.Forms.NumericUpDown();
            this.txt2ServerIP = new System.Windows.Forms.ComboBox();
            this.txt1ServerIP = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx1LocalIP = new System.Windows.Forms.ComboBox();
            this.cbx2LocalIP = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.txt2ServerPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt1ServerPort = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtAuthKeys = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSaveFile = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeqNoThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAppType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocalID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEcInterval)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServer2ID)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServer1ID)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudSeqNoThreshold);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.nudAppType);
            this.groupBox1.Controls.Add(this.nudLocalID);
            this.groupBox1.Controls.Add(this.nudEcInterval);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbxDefenseTech);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbxEquipmentType);
            this.groupBox1.Controls.Add(this.labelAppType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(8, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 346);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置信息";
            // 
            // nudSeqNoThreshold
            // 
            this.nudSeqNoThreshold.Location = new System.Drawing.Point(475, 19);
            this.nudSeqNoThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSeqNoThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSeqNoThreshold.Name = "nudSeqNoThreshold";
            this.nudSeqNoThreshold.Size = new System.Drawing.Size(69, 21);
            this.nudSeqNoThreshold.TabIndex = 22;
            this.toolTip1.SetToolTip(this.nudSeqNoThreshold, "允许的丢包个数");
            this.nudSeqNoThreshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(416, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "序号阈值";
            // 
            // nudAppType
            // 
            this.nudAppType.Location = new System.Drawing.Point(135, 47);
            this.nudAppType.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAppType.Name = "nudAppType";
            this.nudAppType.Size = new System.Drawing.Size(97, 21);
            this.nudAppType.TabIndex = 18;
            this.nudAppType.Value = new decimal(new int[] {
            26,
            0,
            0,
            0});
            // 
            // nudLocalID
            // 
            this.nudLocalID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudLocalID.Location = new System.Drawing.Point(135, 19);
            this.nudLocalID.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudLocalID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLocalID.Name = "nudLocalID";
            this.nudLocalID.Size = new System.Drawing.Size(97, 21);
            this.nudLocalID.TabIndex = 8;
            this.toolTip1.SetToolTip(this.nudLocalID, "点击前面的标签可在10进制与16进制间切换。");
            this.nudLocalID.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudLocalID.ValueChanged += new System.EventHandler(this.nudLocalID_ValueChanged);
            // 
            // nudEcInterval
            // 
            this.nudEcInterval.Location = new System.Drawing.Point(475, 47);
            this.nudEcInterval.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEcInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEcInterval.Name = "nudEcInterval";
            this.nudEcInterval.Size = new System.Drawing.Size(69, 21);
            this.nudEcInterval.TabIndex = 7;
            this.nudEcInterval.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(428, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "EC周期";
            // 
            // cbxDefenseTech
            // 
            this.cbxDefenseTech.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDefenseTech.FormattingEnabled = true;
            this.cbxDefenseTech.Location = new System.Drawing.Point(307, 47);
            this.cbxDefenseTech.Name = "cbxDefenseTech";
            this.cbxDefenseTech.Size = new System.Drawing.Size(96, 20);
            this.cbxDefenseTech.TabIndex = 5;
            this.cbxDefenseTech.SelectedIndexChanged += new System.EventHandler(this.cbxDefenseTech_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "防御技术";
            // 
            // cbxEquipmentType
            // 
            this.cbxEquipmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEquipmentType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbxEquipmentType.FormattingEnabled = true;
            this.cbxEquipmentType.Location = new System.Drawing.Point(307, 19);
            this.cbxEquipmentType.Name = "cbxEquipmentType";
            this.cbxEquipmentType.Size = new System.Drawing.Size(96, 20);
            this.cbxEquipmentType.TabIndex = 5;
            // 
            // labelAppType
            // 
            this.labelAppType.AutoSize = true;
            this.labelAppType.Location = new System.Drawing.Point(76, 50);
            this.labelAppType.Name = "labelAppType";
            this.labelAppType.Size = new System.Drawing.Size(53, 12);
            this.labelAppType.TabIndex = 0;
            this.labelAppType.Text = "应用类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "设备类型";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nudServer2ID);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.txt4ServerIP);
            this.groupBox4.Controls.Add(this.txt3ServerIP);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.checkBox4);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.checkBox3);
            this.groupBox4.Controls.Add(this.cbx3LocalIP);
            this.groupBox4.Controls.Add(this.cbx4LocalIP);
            this.groupBox4.Controls.Add(this.txt4ServerPort);
            this.groupBox4.Controls.Add(this.txt3ServerPort);
            this.groupBox4.Location = new System.Drawing.Point(10, 209);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(574, 128);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "服务器2";
            // 
            // nudServer2ID
            // 
            this.nudServer2ID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudServer2ID.Location = new System.Drawing.Point(99, 20);
            this.nudServer2ID.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudServer2ID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudServer2ID.Name = "nudServer2ID";
            this.nudServer2ID.Size = new System.Drawing.Size(97, 21);
            this.nudServer2ID.TabIndex = 17;
            this.toolTip1.SetToolTip(this.nudServer2ID, "点击前面的标签可在10进制与16进制间切换。");
            this.nudServer2ID.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(238, 99);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 12;
            this.label16.Text = "服务器IP";
            // 
            // txt4ServerIP
            // 
            this.txt4ServerIP.Enabled = false;
            this.txt4ServerIP.FormattingEnabled = true;
            this.txt4ServerIP.Items.AddRange(new object[] {
            "10.0.0.2",
            "192.168.3.229",
            "192.168.11."});
            this.txt4ServerIP.Location = new System.Drawing.Point(295, 96);
            this.txt4ServerIP.Name = "txt4ServerIP";
            this.txt4ServerIP.Size = new System.Drawing.Size(189, 20);
            this.txt4ServerIP.TabIndex = 16;
            // 
            // txt3ServerIP
            // 
            this.txt3ServerIP.Enabled = false;
            this.txt3ServerIP.FormattingEnabled = true;
            this.txt3ServerIP.Items.AddRange(new object[] {
            "10.0.0.2",
            "192.168.3.229",
            "192.168.11."});
            this.txt3ServerIP.Location = new System.Drawing.Point(295, 56);
            this.txt3ServerIP.Name = "txt3ServerIP";
            this.txt3ServerIP.Size = new System.Drawing.Size(189, 20);
            this.txt3ServerIP.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(238, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "服务器IP";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(41, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 8;
            this.label15.Text = "服务器ID";
            this.label15.Click += new System.EventHandler(this.OnServer2IdLabelClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(490, 59);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 6;
            this.label14.Text = "端口";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Enabled = false;
            this.checkBox4.Location = new System.Drawing.Point(10, 98);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(60, 16);
            this.checkBox4.TabIndex = 2;
            this.checkBox4.Text = "本端IP";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(490, 99);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 13;
            this.label17.Text = "端口";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(10, 56);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(60, 16);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "本端IP";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // cbx3LocalIP
            // 
            this.cbx3LocalIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx3LocalIP.Enabled = false;
            this.cbx3LocalIP.FormattingEnabled = true;
            this.cbx3LocalIP.Location = new System.Drawing.Point(71, 54);
            this.cbx3LocalIP.Name = "cbx3LocalIP";
            this.cbx3LocalIP.Size = new System.Drawing.Size(161, 20);
            this.cbx3LocalIP.TabIndex = 2;
            this.cbx3LocalIP.DropDown += new System.EventHandler(this.cbxLocalIP_DropDown);
            // 
            // cbx4LocalIP
            // 
            this.cbx4LocalIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx4LocalIP.Enabled = false;
            this.cbx4LocalIP.FormattingEnabled = true;
            this.cbx4LocalIP.Location = new System.Drawing.Point(71, 95);
            this.cbx4LocalIP.Name = "cbx4LocalIP";
            this.cbx4LocalIP.Size = new System.Drawing.Size(161, 20);
            this.cbx4LocalIP.TabIndex = 3;
            this.cbx4LocalIP.DropDown += new System.EventHandler(this.cbxLocalIP_DropDown);
            // 
            // txt4ServerPort
            // 
            this.txt4ServerPort.Enabled = false;
            this.txt4ServerPort.Location = new System.Drawing.Point(522, 94);
            this.txt4ServerPort.Name = "txt4ServerPort";
            this.txt4ServerPort.Size = new System.Drawing.Size(47, 21);
            this.txt4ServerPort.TabIndex = 7;
            this.txt4ServerPort.Text = "2101";
            // 
            // txt3ServerPort
            // 
            this.txt3ServerPort.Enabled = false;
            this.txt3ServerPort.Location = new System.Drawing.Point(522, 54);
            this.txt3ServerPort.Name = "txt3ServerPort";
            this.txt3ServerPort.Size = new System.Drawing.Size(47, 21);
            this.txt3ServerPort.TabIndex = 6;
            this.txt3ServerPort.Text = "2100";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nudServer1ID);
            this.groupBox3.Controls.Add(this.txt2ServerIP);
            this.groupBox3.Controls.Add(this.txt1ServerIP);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cbx1LocalIP);
            this.groupBox3.Controls.Add(this.cbx2LocalIP);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.txt2ServerPort);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txt1ServerPort);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Location = new System.Drawing.Point(10, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(574, 127);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "服务器1";
            // 
            // nudServer1ID
            // 
            this.nudServer1ID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudServer1ID.Location = new System.Drawing.Point(99, 24);
            this.nudServer1ID.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.nudServer1ID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudServer1ID.Name = "nudServer1ID";
            this.nudServer1ID.Size = new System.Drawing.Size(97, 21);
            this.nudServer1ID.TabIndex = 17;
            this.toolTip1.SetToolTip(this.nudServer1ID, "点击前面的标签可在10进制与16进制间切换。");
            this.nudServer1ID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txt2ServerIP
            // 
            this.txt2ServerIP.Enabled = false;
            this.txt2ServerIP.FormattingEnabled = true;
            this.txt2ServerIP.Items.AddRange(new object[] {
            "10.0.0.2",
            "192.168.3.229",
            "192.168.11."});
            this.txt2ServerIP.Location = new System.Drawing.Point(295, 96);
            this.txt2ServerIP.Name = "txt2ServerIP";
            this.txt2ServerIP.Size = new System.Drawing.Size(189, 20);
            this.txt2ServerIP.TabIndex = 16;
            // 
            // txt1ServerIP
            // 
            this.txt1ServerIP.FormattingEnabled = true;
            this.txt1ServerIP.Items.AddRange(new object[] {
            "10.0.0.2",
            "192.168.3.229",
            "192.168.11."});
            this.txt1ServerIP.Location = new System.Drawing.Point(295, 57);
            this.txt1ServerIP.Name = "txt1ServerIP";
            this.txt1ServerIP.Size = new System.Drawing.Size(189, 20);
            this.txt1ServerIP.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(238, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "服务器IP";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "服务器ID";
            this.label9.Click += new System.EventHandler(this.OnServer1IdLabelClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(490, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "端口";
            // 
            // cbx1LocalIP
            // 
            this.cbx1LocalIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx1LocalIP.FormattingEnabled = true;
            this.cbx1LocalIP.Location = new System.Drawing.Point(71, 57);
            this.cbx1LocalIP.Name = "cbx1LocalIP";
            this.cbx1LocalIP.Size = new System.Drawing.Size(161, 20);
            this.cbx1LocalIP.TabIndex = 2;
            this.cbx1LocalIP.DropDown += new System.EventHandler(this.cbxLocalIP_DropDown);
            // 
            // cbx2LocalIP
            // 
            this.cbx2LocalIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx2LocalIP.Enabled = false;
            this.cbx2LocalIP.FormattingEnabled = true;
            this.cbx2LocalIP.Location = new System.Drawing.Point(71, 96);
            this.cbx2LocalIP.Name = "cbx2LocalIP";
            this.cbx2LocalIP.Size = new System.Drawing.Size(161, 20);
            this.cbx2LocalIP.TabIndex = 3;
            this.cbx2LocalIP.DropDown += new System.EventHandler(this.cbxLocalIP_DropDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(238, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "服务器IP";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(10, 98);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "本端IP";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // txt2ServerPort
            // 
            this.txt2ServerPort.Enabled = false;
            this.txt2ServerPort.Location = new System.Drawing.Point(522, 95);
            this.txt2ServerPort.Name = "txt2ServerPort";
            this.txt2ServerPort.Size = new System.Drawing.Size(47, 21);
            this.txt2ServerPort.TabIndex = 7;
            this.txt2ServerPort.Text = "2101";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(490, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "端口";
            // 
            // txt1ServerPort
            // 
            this.txt1ServerPort.Location = new System.Drawing.Point(522, 56);
            this.txt1ServerPort.Name = "txt1ServerPort";
            this.txt1ServerPort.Size = new System.Drawing.Size(47, 21);
            this.txt1ServerPort.TabIndex = 6;
            this.txt1ServerPort.Text = "2100";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(9, 60);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "本端IP";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(64, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "本地设备ID";
            this.toolTip1.SetToolTip(this.label8, "点击切换为10/16进制");
            this.label8.Click += new System.EventHandler(this.OnLocalIDLabelClick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtAuthKeys);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.chkSaveFile);
            this.groupBox5.Location = new System.Drawing.Point(7, 366);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(587, 83);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "其它选项";
            // 
            // txtAuthKeys
            // 
            this.txtAuthKeys.Location = new System.Drawing.Point(302, 23);
            this.txtAuthKeys.Multiline = true;
            this.txtAuthKeys.Name = "txtAuthKeys";
            this.txtAuthKeys.Size = new System.Drawing.Size(274, 31);
            this.txtAuthKeys.TabIndex = 20;
            this.txtAuthKeys.Text = "A3 45 34 68 98 01 2A BF CD BE 34 56 78 BF EA 32 12 AE 34 21 45 78 98 50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(203, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 24);
            this.label7.TabIndex = 19;
            this.label7.Text = "Authentication \r\nKeys 验证密钥";
            // 
            // chkSaveFile
            // 
            this.chkSaveFile.AutoSize = true;
            this.chkSaveFile.Location = new System.Drawing.Point(17, 34);
            this.chkSaveFile.Name = "chkSaveFile";
            this.chkSaveFile.Size = new System.Drawing.Size(108, 16);
            this.chkSaveFile.TabIndex = 17;
            this.chkSaveFile.Text = "保存收到的文件";
            this.chkSaveFile.UseVisualStyleBackColor = true;
            this.chkSaveFile.CheckedChanged += new System.EventHandler(this.chkSaveBussinessData_CheckedChanged);
            // 
            // ClientConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Name = "ClientConfigControl";
            this.Size = new System.Drawing.Size(662, 558);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeqNoThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAppType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocalID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEcInterval)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServer2ID)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServer1ID)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox txt4ServerIP;
        private System.Windows.Forms.ComboBox txt3ServerIP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox cbx3LocalIP;
        private System.Windows.Forms.ComboBox cbx4LocalIP;
        private System.Windows.Forms.TextBox txt4ServerPort;
        private System.Windows.Forms.TextBox txt3ServerPort;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox txt2ServerIP;
        private System.Windows.Forms.ComboBox txt1ServerIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx1LocalIP;
        private System.Windows.Forms.ComboBox cbx2LocalIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox txt2ServerPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt1ServerPort;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label labelAppType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkSaveFile;
        private System.Windows.Forms.ComboBox cbxEquipmentType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDefenseTech;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAuthKeys;
        private System.Windows.Forms.NumericUpDown nudEcInterval;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudAppType;
        private System.Windows.Forms.NumericUpDown nudLocalID;
        private System.Windows.Forms.NumericUpDown nudServer2ID;
        private System.Windows.Forms.NumericUpDown nudServer1ID;
        private System.Windows.Forms.NumericUpDown nudSeqNoThreshold;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
