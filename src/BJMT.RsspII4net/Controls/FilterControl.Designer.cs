namespace BJMT.RsspII4net.Controls
{
    partial class FilterControl
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
            this.cbxConfidtion3 = new System.Windows.Forms.ComboBox();
            this.cbxConfidtion2 = new System.Windows.Forms.ComboBox();
            this.cmbCondition3Operator = new System.Windows.Forms.ComboBox();
            this.cmbCondition2Operator = new System.Windows.Forms.ComboBox();
            this.cmbCondition1Operator = new System.Windows.Forms.ComboBox();
            this.nudCondition3Value = new System.Windows.Forms.NumericUpDown();
            this.nudCondition2Value = new System.Windows.Forms.NumericUpDown();
            this.nudCondition3Index = new System.Windows.Forms.NumericUpDown();
            this.nudCondition2Index = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudCondition1Value = new System.Windows.Forms.NumericUpDown();
            this.nudCondition1Index = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkConditionEnabled = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition3Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition2Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition3Index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition2Index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition1Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition1Index)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxConfidtion3
            // 
            this.cbxConfidtion3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxConfidtion3.FormattingEnabled = true;
            this.cbxConfidtion3.Items.AddRange(new object[] {
            "无",
            "且",
            "或"});
            this.cbxConfidtion3.Location = new System.Drawing.Point(7, 59);
            this.cbxConfidtion3.Name = "cbxConfidtion3";
            this.cbxConfidtion3.Size = new System.Drawing.Size(41, 20);
            this.cbxConfidtion3.TabIndex = 14;
            this.cbxConfidtion3.SelectedIndexChanged += new System.EventHandler(this.cbxConfidtion3_SelectedIndexChanged);
            // 
            // cbxConfidtion2
            // 
            this.cbxConfidtion2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxConfidtion2.FormattingEnabled = true;
            this.cbxConfidtion2.Items.AddRange(new object[] {
            "无",
            "且",
            "或"});
            this.cbxConfidtion2.Location = new System.Drawing.Point(7, 35);
            this.cbxConfidtion2.Name = "cbxConfidtion2";
            this.cbxConfidtion2.Size = new System.Drawing.Size(41, 20);
            this.cbxConfidtion2.TabIndex = 16;
            this.cbxConfidtion2.SelectedIndexChanged += new System.EventHandler(this.cbxConfidtion2_SelectedIndexChanged);
            // 
            // cmbCondition3Operator
            // 
            this.cmbCondition3Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondition3Operator.FormattingEnabled = true;
            this.cmbCondition3Operator.Items.AddRange(new object[] {
            "等于",
            "不等于"});
            this.cmbCondition3Operator.Location = new System.Drawing.Point(184, 61);
            this.cmbCondition3Operator.Name = "cmbCondition3Operator";
            this.cmbCondition3Operator.Size = new System.Drawing.Size(60, 20);
            this.cmbCondition3Operator.TabIndex = 28;
            // 
            // cmbCondition2Operator
            // 
            this.cmbCondition2Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondition2Operator.FormattingEnabled = true;
            this.cmbCondition2Operator.Items.AddRange(new object[] {
            "等于",
            "不等于"});
            this.cmbCondition2Operator.Location = new System.Drawing.Point(184, 35);
            this.cmbCondition2Operator.Name = "cmbCondition2Operator";
            this.cmbCondition2Operator.Size = new System.Drawing.Size(60, 20);
            this.cmbCondition2Operator.TabIndex = 27;
            // 
            // cmbCondition1Operator
            // 
            this.cmbCondition1Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondition1Operator.FormattingEnabled = true;
            this.cmbCondition1Operator.Items.AddRange(new object[] {
            "等于",
            "不等于"});
            this.cmbCondition1Operator.Location = new System.Drawing.Point(184, 7);
            this.cmbCondition1Operator.Name = "cmbCondition1Operator";
            this.cmbCondition1Operator.Size = new System.Drawing.Size(60, 20);
            this.cmbCondition1Operator.TabIndex = 26;
            // 
            // nudCondition3Value
            // 
            this.nudCondition3Value.Hexadecimal = true;
            this.nudCondition3Value.Location = new System.Drawing.Point(246, 61);
            this.nudCondition3Value.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudCondition3Value.Name = "nudCondition3Value";
            this.nudCondition3Value.Size = new System.Drawing.Size(42, 21);
            this.nudCondition3Value.TabIndex = 25;
            // 
            // nudCondition2Value
            // 
            this.nudCondition2Value.Hexadecimal = true;
            this.nudCondition2Value.Location = new System.Drawing.Point(246, 34);
            this.nudCondition2Value.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudCondition2Value.Name = "nudCondition2Value";
            this.nudCondition2Value.Size = new System.Drawing.Size(42, 21);
            this.nudCondition2Value.TabIndex = 24;
            // 
            // nudCondition3Index
            // 
            this.nudCondition3Index.Location = new System.Drawing.Point(76, 61);
            this.nudCondition3Index.Maximum = new decimal(new int[] {
            64000,
            0,
            0,
            0});
            this.nudCondition3Index.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCondition3Index.Name = "nudCondition3Index";
            this.nudCondition3Index.Size = new System.Drawing.Size(61, 21);
            this.nudCondition3Index.TabIndex = 23;
            this.nudCondition3Index.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nudCondition2Index
            // 
            this.nudCondition2Index.Location = new System.Drawing.Point(76, 34);
            this.nudCondition2Index.Maximum = new decimal(new int[] {
            64000,
            0,
            0,
            0});
            this.nudCondition2Index.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCondition2Index.Name = "nudCondition2Index";
            this.nudCondition2Index.Size = new System.Drawing.Size(61, 21);
            this.nudCondition2Index.TabIndex = 22;
            this.nudCondition2Index.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "个字节";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "第";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "个字节";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "第";
            // 
            // nudCondition1Value
            // 
            this.nudCondition1Value.Hexadecimal = true;
            this.nudCondition1Value.Location = new System.Drawing.Point(246, 7);
            this.nudCondition1Value.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudCondition1Value.Name = "nudCondition1Value";
            this.nudCondition1Value.Size = new System.Drawing.Size(42, 21);
            this.nudCondition1Value.TabIndex = 17;
            // 
            // nudCondition1Index
            // 
            this.nudCondition1Index.Location = new System.Drawing.Point(76, 6);
            this.nudCondition1Index.Maximum = new decimal(new int[] {
            64000,
            0,
            0,
            0});
            this.nudCondition1Index.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCondition1Index.Name = "nudCondition1Index";
            this.nudCondition1Index.Size = new System.Drawing.Size(61, 21);
            this.nudCondition1Index.TabIndex = 15;
            this.nudCondition1Index.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "个字节";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "第";
            // 
            // chkConditionEnabled
            // 
            this.chkConditionEnabled.AutoSize = true;
            this.chkConditionEnabled.Location = new System.Drawing.Point(7, 8);
            this.chkConditionEnabled.Name = "chkConditionEnabled";
            this.chkConditionEnabled.Size = new System.Drawing.Size(48, 16);
            this.chkConditionEnabled.TabIndex = 11;
            this.chkConditionEnabled.Text = "启用";
            this.chkConditionEnabled.UseVisualStyleBackColor = true;
            this.chkConditionEnabled.CheckedChanged += new System.EventHandler(this.chkConditionEnabled_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(348, 174);
            this.tabControl1.TabIndex = 29;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nudCondition1Index);
            this.tabPage1.Controls.Add(this.cbxConfidtion3);
            this.tabPage1.Controls.Add(this.chkConditionEnabled);
            this.tabPage1.Controls.Add(this.cbxConfidtion2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cmbCondition3Operator);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cmbCondition2Operator);
            this.tabPage1.Controls.Add(this.nudCondition1Value);
            this.tabPage1.Controls.Add(this.cmbCondition1Operator);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.nudCondition3Value);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.nudCondition2Value);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.nudCondition3Index);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.nudCondition2Index);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(340, 149);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "字节过滤";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // FilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "FilterControl";
            this.Size = new System.Drawing.Size(475, 293);
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition3Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition2Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition3Index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition2Index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition1Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCondition1Index)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxConfidtion3;
        private System.Windows.Forms.ComboBox cbxConfidtion2;
        private System.Windows.Forms.ComboBox cmbCondition3Operator;
        private System.Windows.Forms.ComboBox cmbCondition2Operator;
        private System.Windows.Forms.ComboBox cmbCondition1Operator;
        private System.Windows.Forms.NumericUpDown nudCondition3Value;
        private System.Windows.Forms.NumericUpDown nudCondition2Value;
        private System.Windows.Forms.NumericUpDown nudCondition3Index;
        private System.Windows.Forms.NumericUpDown nudCondition2Index;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudCondition1Value;
        private System.Windows.Forms.NumericUpDown nudCondition1Index;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkConditionEnabled;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
    }
}
