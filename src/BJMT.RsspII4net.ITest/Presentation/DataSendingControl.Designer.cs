namespace BJMT.RsspII4net.ITest.Presentation
{
    partial class DataSendingControl
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
            this.chkFileBeginFlag = new System.Windows.Forms.CheckBox();
            this.rdoCustomFile = new System.Windows.Forms.RadioButton();
            this.nudPacketSize = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.ctrlRepeatNum = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtTestFile = new System.Windows.Forms.TextBox();
            this.openTestFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.clbDests = new System.Windows.Forms.CheckedListBox();
            this.chkAsciiData = new System.Windows.Forms.CheckBox();
            this.chkHexData = new System.Windows.Forms.CheckBox();
            this.rdoUIData = new System.Windows.Forms.RadioButton();
            this.txtUIData = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudPacketSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlRepeatNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // chkFileBeginFlag
            // 
            this.chkFileBeginFlag.AutoSize = true;
            this.chkFileBeginFlag.Checked = true;
            this.chkFileBeginFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFileBeginFlag.Location = new System.Drawing.Point(120, 16);
            this.chkFileBeginFlag.Name = "chkFileBeginFlag";
            this.chkFileBeginFlag.Size = new System.Drawing.Size(120, 16);
            this.chkFileBeginFlag.TabIndex = 25;
            this.chkFileBeginFlag.Text = "发送文件始末标示";
            this.chkFileBeginFlag.UseVisualStyleBackColor = true;
            this.chkFileBeginFlag.CheckedChanged += new System.EventHandler(this.chkFileBeginFlag_CheckedChanged);
            // 
            // rdoCustomFile
            // 
            this.rdoCustomFile.AutoSize = true;
            this.rdoCustomFile.Checked = true;
            this.rdoCustomFile.ForeColor = System.Drawing.Color.Blue;
            this.rdoCustomFile.Location = new System.Drawing.Point(16, 16);
            this.rdoCustomFile.Name = "rdoCustomFile";
            this.rdoCustomFile.Size = new System.Drawing.Size(83, 16);
            this.rdoCustomFile.TabIndex = 23;
            this.rdoCustomFile.TabStop = true;
            this.rdoCustomFile.Text = "自定义文件";
            this.rdoCustomFile.UseVisualStyleBackColor = true;
            this.rdoCustomFile.CheckedChanged += new System.EventHandler(this.rdoCustomFile_CheckedChanged);
            // 
            // nudPacketSize
            // 
            this.nudPacketSize.Location = new System.Drawing.Point(335, 12);
            this.nudPacketSize.Maximum = new decimal(new int[] {
            10240,
            0,
            0,
            0});
            this.nudPacketSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPacketSize.Name = "nudPacketSize";
            this.nudPacketSize.Size = new System.Drawing.Size(74, 21);
            this.nudPacketSize.TabIndex = 16;
            this.nudPacketSize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPacketSize.ValueChanged += new System.EventHandler(this.nudPacketSize_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(265, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 15;
            this.label12.Text = "数据包大小";
            // 
            // ctrlRepeatNum
            // 
            this.ctrlRepeatNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlRepeatNum.Location = new System.Drawing.Point(108, 47);
            this.ctrlRepeatNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ctrlRepeatNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ctrlRepeatNum.Name = "ctrlRepeatNum";
            this.ctrlRepeatNum.Size = new System.Drawing.Size(97, 21);
            this.ctrlRepeatNum.TabIndex = 18;
            this.ctrlRepeatNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ctrlRepeatNum.ValueChanged += new System.EventHandler(this.ctrlRepeatNum_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 17;
            this.label11.Text = "发送次数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "选择目标";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "发送间隔（毫秒）";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(388, 38);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(25, 60);
            this.btnSelectFile.TabIndex = 9;
            this.btnSelectFile.Text = "选择文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtTestFile
            // 
            this.txtTestFile.Location = new System.Drawing.Point(33, 38);
            this.txtTestFile.Multiline = true;
            this.txtTestFile.Name = "txtTestFile";
            this.txtTestFile.ReadOnly = true;
            this.txtTestFile.Size = new System.Drawing.Size(353, 60);
            this.txtTestFile.TabIndex = 8;
            // 
            // openTestFileDialog
            // 
            this.openTestFileDialog.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtUIData);
            this.splitContainer1.Size = new System.Drawing.Size(649, 414);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.TabIndex = 10;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.nudInterval);
            this.splitContainer2.Panel1.Controls.Add(this.clbDests);
            this.splitContainer2.Panel1.Controls.Add(this.label10);
            this.splitContainer2.Panel1.Controls.Add(this.label11);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.ctrlRepeatNum);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chkAsciiData);
            this.splitContainer2.Panel2.Controls.Add(this.chkHexData);
            this.splitContainer2.Panel2.Controls.Add(this.rdoUIData);
            this.splitContainer2.Panel2.Controls.Add(this.label12);
            this.splitContainer2.Panel2.Controls.Add(this.chkFileBeginFlag);
            this.splitContainer2.Panel2.Controls.Add(this.rdoCustomFile);
            this.splitContainer2.Panel2.Controls.Add(this.btnSelectFile);
            this.splitContainer2.Panel2.Controls.Add(this.nudPacketSize);
            this.splitContainer2.Panel2.Controls.Add(this.txtTestFile);
            this.splitContainer2.Size = new System.Drawing.Size(649, 204);
            this.splitContainer2.SplitterDistance = 220;
            this.splitContainer2.TabIndex = 0;
            // 
            // nudInterval
            // 
            this.nudInterval.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudInterval.Location = new System.Drawing.Point(108, 15);
            this.nudInterval.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(97, 21);
            this.nudInterval.TabIndex = 21;
            this.nudInterval.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudInterval.ValueChanged += new System.EventHandler(this.nudInterval_ValueChanged);
            // 
            // clbDests
            // 
            this.clbDests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbDests.CheckOnClick = true;
            this.clbDests.FormattingEnabled = true;
            this.clbDests.Location = new System.Drawing.Point(3, 111);
            this.clbDests.MultiColumn = true;
            this.clbDests.Name = "clbDests";
            this.clbDests.Size = new System.Drawing.Size(212, 84);
            this.clbDests.TabIndex = 20;
            // 
            // chkAsciiData
            // 
            this.chkAsciiData.AutoSize = true;
            this.chkAsciiData.Enabled = false;
            this.chkAsciiData.Location = new System.Drawing.Point(205, 146);
            this.chkAsciiData.Name = "chkAsciiData";
            this.chkAsciiData.Size = new System.Drawing.Size(54, 16);
            this.chkAsciiData.TabIndex = 27;
            this.chkAsciiData.Text = "ASCII";
            this.chkAsciiData.UseVisualStyleBackColor = true;
            this.chkAsciiData.CheckedChanged += new System.EventHandler(this.chkAsciiData_CheckedChanged);
            // 
            // chkHexData
            // 
            this.chkHexData.AutoSize = true;
            this.chkHexData.Checked = true;
            this.chkHexData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHexData.Enabled = false;
            this.chkHexData.Location = new System.Drawing.Point(141, 146);
            this.chkHexData.Name = "chkHexData";
            this.chkHexData.Size = new System.Drawing.Size(42, 16);
            this.chkHexData.TabIndex = 27;
            this.chkHexData.Text = "Hex";
            this.toolTip1.SetToolTip(this.chkHexData, "以空格分隔，例如：1A 2C");
            this.chkHexData.UseVisualStyleBackColor = true;
            this.chkHexData.CheckedChanged += new System.EventHandler(this.chkHexData_CheckedChanged);
            // 
            // rdoUIData
            // 
            this.rdoUIData.AutoSize = true;
            this.rdoUIData.ForeColor = System.Drawing.Color.Blue;
            this.rdoUIData.Location = new System.Drawing.Point(16, 145);
            this.rdoUIData.Name = "rdoUIData";
            this.rdoUIData.Size = new System.Drawing.Size(119, 16);
            this.rdoUIData.TabIndex = 26;
            this.rdoUIData.TabStop = true;
            this.rdoUIData.Text = "界面上指定的数据";
            this.rdoUIData.UseVisualStyleBackColor = true;
            this.rdoUIData.CheckedChanged += new System.EventHandler(this.rdoUIData_CheckedChanged);
            // 
            // txtUIData
            // 
            this.txtUIData.Location = new System.Drawing.Point(257, 31);
            this.txtUIData.Multiline = true;
            this.txtUIData.Name = "txtUIData";
            this.txtUIData.Size = new System.Drawing.Size(197, 101);
            this.txtUIData.TabIndex = 0;
            this.txtUIData.Text = "1A 2B 3C";
            // 
            // DataSendingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DataSendingControl";
            this.Size = new System.Drawing.Size(649, 414);
            ((System.ComponentModel.ISupportInitialize)(this.nudPacketSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlRepeatNum)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudPacketSize;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown ctrlRepeatNum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtTestFile;
        private System.Windows.Forms.RadioButton rdoCustomFile;
        private System.Windows.Forms.OpenFileDialog openTestFileDialog;
        private System.Windows.Forms.CheckBox chkFileBeginFlag;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RadioButton rdoUIData;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkAsciiData;
        private System.Windows.Forms.CheckBox chkHexData;
        private System.Windows.Forms.TextBox txtUIData;
        private System.Windows.Forms.CheckedListBox clbDests;
        private System.Windows.Forms.NumericUpDown nudInterval;
    }
}
