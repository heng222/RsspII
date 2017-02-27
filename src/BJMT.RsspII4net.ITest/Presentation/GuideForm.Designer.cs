namespace BJMT.RsspII4net.ITest
{
    partial class GuideForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuideForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnServer = new System.Windows.Forms.RadioButton();
            this.rbtnClient = new System.Windows.Forms.RadioButton();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnServer);
            this.groupBox1.Controls.Add(this.rbtnClient);
            this.groupBox1.Location = new System.Drawing.Point(26, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模式选择";
            // 
            // rbtnServer
            // 
            this.rbtnServer.AutoSize = true;
            this.rbtnServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbtnServer.Location = new System.Drawing.Point(32, 79);
            this.rbtnServer.Name = "rbtnServer";
            this.rbtnServer.Size = new System.Drawing.Size(107, 16);
            this.rbtnServer.TabIndex = 2;
            this.rbtnServer.Text = "Rssp-II 服务器";
            this.rbtnServer.UseVisualStyleBackColor = true;
            // 
            // rbtnClient
            // 
            this.rbtnClient.AutoSize = true;
            this.rbtnClient.Checked = true;
            this.rbtnClient.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbtnClient.Location = new System.Drawing.Point(32, 41);
            this.rbtnClient.Name = "rbtnClient";
            this.rbtnClient.Size = new System.Drawing.Size(107, 16);
            this.rbtnClient.TabIndex = 1;
            this.rbtnClient.TabStop = true;
            this.rbtnClient.Text = "Rssp-II 客户端";
            this.rbtnClient.UseVisualStyleBackColor = true;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(36, 154);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(75, 28);
            this.btnLaunch.TabIndex = 4;
            this.btnLaunch.Text = "启动(&L)";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(136, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // GuideForm
            // 
            this.AcceptButton = this.btnLaunch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(253, 193);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GuideForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "启动向导";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnServer;
        private System.Windows.Forms.RadioButton rbtnClient;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnCancel;
    }
}