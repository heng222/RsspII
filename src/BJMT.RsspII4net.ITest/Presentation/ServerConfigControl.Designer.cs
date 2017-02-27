namespace BJMT.RsspII4net.ITest.Presentation
{
    partial class ServerConfigControl
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtAuthKeys = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkAcceptableClients = new System.Windows.Forms.CheckBox();
            this.chkSaveFile = new System.Windows.Forms.CheckBox();
            this.txtAcceptableClients = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx1ListenIP = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx2ListenIP = new System.Windows.Forms.ComboBox();
            this.txt1ListenPort = new System.Windows.Forms.TextBox();
            this.txt2ListenPort = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudSeqNoThreshold = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudAppType = new System.Windows.Forms.NumericUpDown();
            this.cbxEquipmentType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudEcInterval = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudLocalID = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeqNoThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAppType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEcInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocalID)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtAuthKeys);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.chkAcceptableClients);
            this.groupBox3.Controls.Add(this.chkSaveFile);
            this.groupBox3.Controls.Add(this.txtAcceptableClients);
            this.groupBox3.Location = new System.Drawing.Point(13, 173);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(496, 197);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "其它选项";
            // 
            // txtAuthKeys
            // 
            this.txtAuthKeys.Location = new System.Drawing.Point(109, 142);
            this.txtAuthKeys.Multiline = true;
            this.txtAuthKeys.Name = "txtAuthKeys";
            this.txtAuthKeys.Size = new System.Drawing.Size(372, 31);
            this.txtAuthKeys.TabIndex = 22;
            this.txtAuthKeys.Text = "A3 45 34 68 98 01 2A BF CD BE 34 56 78 BF EA 32 12 AE 34 21 45 78 98 50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 24);
            this.label7.TabIndex = 21;
            this.label7.Text = "Authentication\r\nKeys 验证密钥";
            // 
            // chkAcceptableClients
            // 
            this.chkAcceptableClients.AutoSize = true;
            this.chkAcceptableClients.Location = new System.Drawing.Point(19, 64);
            this.chkAcceptableClients.Name = "chkAcceptableClients";
            this.chkAcceptableClients.Size = new System.Drawing.Size(84, 16);
            this.chkAcceptableClients.TabIndex = 2;
            this.chkAcceptableClients.Text = "指定客户端";
            this.chkAcceptableClients.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkAcceptableClients.UseVisualStyleBackColor = true;
            // 
            // chkSaveFile
            // 
            this.chkSaveFile.AutoSize = true;
            this.chkSaveFile.Location = new System.Drawing.Point(19, 29);
            this.chkSaveFile.Name = "chkSaveFile";
            this.chkSaveFile.Size = new System.Drawing.Size(108, 16);
            this.chkSaveFile.TabIndex = 18;
            this.chkSaveFile.Text = "保存收到的文件";
            this.chkSaveFile.UseVisualStyleBackColor = true;
            this.chkSaveFile.CheckedChanged += new System.EventHandler(this.chkSaveBussinessData_CheckedChanged);
            // 
            // txtAcceptableClients
            // 
            this.txtAcceptableClients.Location = new System.Drawing.Point(109, 61);
            this.txtAcceptableClients.Multiline = true;
            this.txtAcceptableClients.Name = "txtAcceptableClients";
            this.txtAcceptableClients.Size = new System.Drawing.Size(372, 63);
            this.txtAcceptableClients.TabIndex = 1;
            this.txtAcceptableClients.Text = "1;2;3";
            this.toolTip1.SetToolTip(this.txtAcceptableClients, "示例1：1;2;3 （只限定ID） \r\n示例2：1,10.0.0.1;2,10.0.0.2;20.0.0.2 （限定ID与IP）\r\n示例3：1,10.0.0.1:" +
        "3003,20.0.0.1:3003（限定ID、IP与端口）\r\n");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "监听端口";
            // 
            // cbx1ListenIP
            // 
            this.cbx1ListenIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx1ListenIP.FormattingEnabled = true;
            this.cbx1ListenIP.Location = new System.Drawing.Point(74, 85);
            this.cbx1ListenIP.Name = "cbx1ListenIP";
            this.cbx1ListenIP.Size = new System.Drawing.Size(216, 20);
            this.cbx1ListenIP.TabIndex = 1;
            this.cbx1ListenIP.DropDown += new System.EventHandler(this.cbx1ListenIP_DropDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "监听端口";
            // 
            // cbx2ListenIP
            // 
            this.cbx2ListenIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx2ListenIP.FormattingEnabled = true;
            this.cbx2ListenIP.Location = new System.Drawing.Point(74, 119);
            this.cbx2ListenIP.Name = "cbx2ListenIP";
            this.cbx2ListenIP.Size = new System.Drawing.Size(216, 20);
            this.cbx2ListenIP.TabIndex = 4;
            this.cbx2ListenIP.DropDown += new System.EventHandler(this.cbx1ListenIP_DropDown);
            // 
            // txt1ListenPort
            // 
            this.txt1ListenPort.Location = new System.Drawing.Point(355, 86);
            this.txt1ListenPort.Name = "txt1ListenPort";
            this.txt1ListenPort.Size = new System.Drawing.Size(75, 21);
            this.txt1ListenPort.TabIndex = 2;
            this.txt1ListenPort.Text = "2100";
            // 
            // txt2ListenPort
            // 
            this.txt2ListenPort.Location = new System.Drawing.Point(355, 120);
            this.txt2ListenPort.Name = "txt2ListenPort";
            this.txt2ListenPort.Size = new System.Drawing.Size(75, 21);
            this.txt2ListenPort.TabIndex = 5;
            this.txt2ListenPort.Text = "2101";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(21, 88);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "选择";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(21, 122);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "选择";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "设备ID";
            this.label5.Click += new System.EventHandler(this.OnLabelServerIdClicked);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 58);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 12);
            this.label21.TabIndex = 7;
            this.label21.Text = "应用类型";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudSeqNoThreshold);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.nudAppType);
            this.groupBox1.Controls.Add(this.cbxEquipmentType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudEcInterval);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.nudLocalID);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.txt2ListenPort);
            this.groupBox1.Controls.Add(this.txt1ListenPort);
            this.groupBox1.Controls.Add(this.cbx2ListenIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbx1ListenIP);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(13, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 152);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "监听设置";
            // 
            // nudSeqNoThreshold
            // 
            this.nudSeqNoThreshold.Location = new System.Drawing.Point(412, 27);
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
            this.nudSeqNoThreshold.TabIndex = 24;
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
            this.label11.Location = new System.Drawing.Point(353, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "序号阈值";
            // 
            // nudAppType
            // 
            this.nudAppType.Location = new System.Drawing.Point(74, 54);
            this.nudAppType.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAppType.Name = "nudAppType";
            this.nudAppType.Size = new System.Drawing.Size(108, 21);
            this.nudAppType.TabIndex = 19;
            this.nudAppType.Value = new decimal(new int[] {
            26,
            0,
            0,
            0});
            // 
            // cbxEquipmentType
            // 
            this.cbxEquipmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEquipmentType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbxEquipmentType.FormattingEnabled = true;
            this.cbxEquipmentType.Location = new System.Drawing.Point(248, 26);
            this.cbxEquipmentType.Name = "cbxEquipmentType";
            this.cbxEquipmentType.Size = new System.Drawing.Size(96, 20);
            this.cbxEquipmentType.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "设备类型";
            // 
            // nudEcInterval
            // 
            this.nudEcInterval.Location = new System.Drawing.Point(248, 54);
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
            this.nudEcInterval.Size = new System.Drawing.Size(96, 21);
            this.nudEcInterval.TabIndex = 11;
            this.toolTip1.SetToolTip(this.nudEcInterval, "仅当客户端使用EC技术时有效。");
            this.nudEcInterval.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(201, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "EC周期";
            // 
            // nudLocalID
            // 
            this.nudLocalID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudLocalID.Location = new System.Drawing.Point(74, 25);
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
            this.nudLocalID.Size = new System.Drawing.Size(108, 21);
            this.nudLocalID.TabIndex = 9;
            this.toolTip1.SetToolTip(this.nudLocalID, "点击前面的标签可在10进制与16进制间切换。");
            this.nudLocalID.Value = new decimal(new int[] {
            143900673,
            0,
            0,
            0});
            this.nudLocalID.ValueChanged += new System.EventHandler(this.nudLocalID_ValueChanged);
            // 
            // ServerConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "ServerConfigControl";
            this.Size = new System.Drawing.Size(530, 399);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeqNoThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAppType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEcInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocalID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkSaveFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx1ListenIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx2ListenIP;
        private System.Windows.Forms.TextBox txt1ListenPort;
        private System.Windows.Forms.TextBox txt2ListenPort;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAcceptableClients;
        private System.Windows.Forms.TextBox txtAcceptableClients;
        private System.Windows.Forms.NumericUpDown nudLocalID;
        private System.Windows.Forms.TextBox txtAuthKeys;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudEcInterval;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxEquipmentType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAppType;
        private System.Windows.Forms.NumericUpDown nudSeqNoThreshold;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
