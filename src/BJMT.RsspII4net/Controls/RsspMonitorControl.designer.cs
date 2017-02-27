namespace BJMT.RsspII4net.Controls
{
    partial class RsspMonitorControl
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                _productCache.Close();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RsspMonitorControl));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeViewNetwork = new System.Windows.Forms.TreeView();
            this.contextMenuCommStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListState = new System.Windows.Forms.ImageList(this.components);
            this.listViewLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.listViewConnectionLog = new System.Windows.Forms.ListView();
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.splitContainer13 = new System.Windows.Forms.SplitContainer();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.chkOutputStreamVisable = new System.Windows.Forms.CheckBox();
            this.chkInputStreamVisable = new System.Windows.Forms.CheckBox();
            this.cbxCurrentNodeID = new System.Windows.Forms.ComboBox();
            this.treeViewDataSummary = new System.Windows.Forms.TreeView();
            this.contextMenuAliveData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListData = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.txtDataDetailed = new System.Windows.Forms.RichTextBox();
            this.chkSyncRefresh = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuCommStatus.SuspendLayout();
            this.contextMenuLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).BeginInit();
            this.splitContainer13.Panel1.SuspendLayout();
            this.splitContainer13.Panel2.SuspendLayout();
            this.splitContainer13.SuspendLayout();
            this.contextMenuAliveData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imgListState;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 371);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.ImageKey = "NetworkState";
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 344);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "网络状态";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(610, 338);
            this.splitContainer1.SplitterDistance = 371;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(15, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeViewNetwork);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listViewLog);
            this.splitContainer2.Size = new System.Drawing.Size(226, 318);
            this.splitContainer2.SplitterDistance = 230;
            this.splitContainer2.TabIndex = 2;
            // 
            // treeViewNetwork
            // 
            this.treeViewNetwork.ContextMenuStrip = this.contextMenuCommStatus;
            this.treeViewNetwork.ImageIndex = 0;
            this.treeViewNetwork.ImageList = this.imgListState;
            this.treeViewNetwork.Location = new System.Drawing.Point(39, 28);
            this.treeViewNetwork.Name = "treeViewNetwork";
            this.treeViewNetwork.SelectedImageIndex = 0;
            this.treeViewNetwork.Size = new System.Drawing.Size(118, 65);
            this.treeViewNetwork.TabIndex = 1;
            this.treeViewNetwork.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnCommStateTreeViewNodeMouseClick);
            // 
            // contextMenuCommStatus
            // 
            this.contextMenuCommStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemExpandAll,
            this.ToolStripMenuItemCollapseAll});
            this.contextMenuCommStatus.Name = "contextMenuCommStatus";
            this.contextMenuCommStatus.Size = new System.Drawing.Size(142, 48);
            // 
            // ToolStripMenuItemExpandAll
            // 
            this.ToolStripMenuItemExpandAll.Name = "ToolStripMenuItemExpandAll";
            this.ToolStripMenuItemExpandAll.Size = new System.Drawing.Size(141, 22);
            this.ToolStripMenuItemExpandAll.Text = "全部打开(&-)";
            this.ToolStripMenuItemExpandAll.Click += new System.EventHandler(this.ToolStripMenuItemExpandAll_Click);
            // 
            // ToolStripMenuItemCollapseAll
            // 
            this.ToolStripMenuItemCollapseAll.Name = "ToolStripMenuItemCollapseAll";
            this.ToolStripMenuItemCollapseAll.Size = new System.Drawing.Size(141, 22);
            this.ToolStripMenuItemCollapseAll.Text = "全部折叠(&+)";
            this.ToolStripMenuItemCollapseAll.Click += new System.EventHandler(this.ToolStripMenuItemCollapseAll_Click);
            // 
            // imgListState
            // 
            this.imgListState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListState.ImageStream")));
            this.imgListState.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListState.Images.SetKeyName(0, "DeviceOnline");
            this.imgListState.Images.SetKeyName(1, "DeviceOffline");
            this.imgListState.Images.SetKeyName(2, "connecting");
            this.imgListState.Images.SetKeyName(3, "fail");
            this.imgListState.Images.SetKeyName(4, "success");
            this.imgListState.Images.SetKeyName(5, "listen");
            this.imgListState.Images.SetKeyName(6, "AliveData");
            this.imgListState.Images.SetKeyName(7, "NetworkState");
            // 
            // listViewLog
            // 
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewLog.ContextMenuStrip = this.contextMenuLog;
            this.listViewLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLog.FullRowSelect = true;
            this.listViewLog.GridLines = true;
            this.listViewLog.Location = new System.Drawing.Point(0, 0);
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(224, 82);
            this.listViewLog.TabIndex = 24;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "    时 间";
            this.columnHeader1.Width = 130;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类 型";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "  信 息";
            this.columnHeader3.Width = 1000;
            // 
            // contextMenuLog
            // 
            this.contextMenuLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearlogToolStripMenuItem});
            this.contextMenuLog.Name = "contextMenuLog";
            this.contextMenuLog.Size = new System.Drawing.Size(141, 26);
            // 
            // clearlogToolStripMenuItem
            // 
            this.clearlogToolStripMenuItem.Name = "clearlogToolStripMenuItem";
            this.clearlogToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.clearlogToolStripMenuItem.Text = "清空日志(&C)";
            this.clearlogToolStripMenuItem.Click += new System.EventHandler(this.clearlogToolStripMenuItem_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.MinimumSize = new System.Drawing.Size(204, 308);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.listViewConnectionLog);
            this.splitContainer3.Size = new System.Drawing.Size(235, 338);
            this.splitContainer3.SplitterDistance = 219;
            this.splitContainer3.TabIndex = 26;
            // 
            // listViewConnectionLog
            // 
            this.listViewConnectionLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTime,
            this.chInfo});
            this.listViewConnectionLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewConnectionLog.FullRowSelect = true;
            this.listViewConnectionLog.Location = new System.Drawing.Point(0, 0);
            this.listViewConnectionLog.Name = "listViewConnectionLog";
            this.listViewConnectionLog.Size = new System.Drawing.Size(233, 217);
            this.listViewConnectionLog.TabIndex = 25;
            this.listViewConnectionLog.UseCompatibleStateImageBehavior = false;
            this.listViewConnectionLog.View = System.Windows.Forms.View.Details;
            // 
            // chTime
            // 
            this.chTime.Text = "    时 间";
            this.chTime.Width = 130;
            // 
            // chInfo
            // 
            this.chInfo.Text = "  信 息";
            this.chInfo.Width = 300;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer8);
            this.tabPage2.ImageKey = "AliveData";
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(616, 344);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "实况数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer8
            // 
            this.splitContainer8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer8.Location = new System.Drawing.Point(3, 3);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.splitContainer13);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer8.Size = new System.Drawing.Size(610, 338);
            this.splitContainer8.SplitterDistance = 324;
            this.splitContainer8.TabIndex = 1;
            // 
            // splitContainer13
            // 
            this.splitContainer13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer13.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer13.Location = new System.Drawing.Point(0, 0);
            this.splitContainer13.Name = "splitContainer13";
            this.splitContainer13.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer13.Panel1
            // 
            this.splitContainer13.Panel1.Controls.Add(this.groupBoxFilter);
            this.splitContainer13.Panel1.Controls.Add(this.chkOutputStreamVisable);
            this.splitContainer13.Panel1.Controls.Add(this.chkInputStreamVisable);
            this.splitContainer13.Panel1.Controls.Add(this.cbxCurrentNodeID);
            // 
            // splitContainer13.Panel2
            // 
            this.splitContainer13.Panel2.Controls.Add(this.treeViewDataSummary);
            this.splitContainer13.Size = new System.Drawing.Size(322, 336);
            this.splitContainer13.SplitterDistance = 178;
            this.splitContainer13.TabIndex = 0;
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFilter.Location = new System.Drawing.Point(3, 48);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(314, 125);
            this.groupBoxFilter.TabIndex = 5;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "过滤器";
            // 
            // chkOutputStreamVisable
            // 
            this.chkOutputStreamVisable.AutoSize = true;
            this.chkOutputStreamVisable.Location = new System.Drawing.Point(97, 26);
            this.chkOutputStreamVisable.Name = "chkOutputStreamVisable";
            this.chkOutputStreamVisable.Size = new System.Drawing.Size(84, 16);
            this.chkOutputStreamVisable.TabIndex = 4;
            this.chkOutputStreamVisable.Text = "显示输出流";
            this.chkOutputStreamVisable.UseVisualStyleBackColor = true;
            this.chkOutputStreamVisable.CheckedChanged += new System.EventHandler(this.chkOutputStreamVisable_CheckedChanged);
            // 
            // chkInputStreamVisable
            // 
            this.chkInputStreamVisable.AutoSize = true;
            this.chkInputStreamVisable.Location = new System.Drawing.Point(1, 26);
            this.chkInputStreamVisable.Name = "chkInputStreamVisable";
            this.chkInputStreamVisable.Size = new System.Drawing.Size(84, 16);
            this.chkInputStreamVisable.TabIndex = 3;
            this.chkInputStreamVisable.Text = "显示输入流";
            this.chkInputStreamVisable.UseVisualStyleBackColor = true;
            this.chkInputStreamVisable.CheckedChanged += new System.EventHandler(this.chkInputStreamVisable_CheckedChanged);
            // 
            // cbxCurrentNodeID
            // 
            this.cbxCurrentNodeID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxCurrentNodeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCurrentNodeID.FormattingEnabled = true;
            this.cbxCurrentNodeID.Location = new System.Drawing.Point(0, 0);
            this.cbxCurrentNodeID.Name = "cbxCurrentNodeID";
            this.cbxCurrentNodeID.Size = new System.Drawing.Size(320, 20);
            this.cbxCurrentNodeID.TabIndex = 2;
            this.cbxCurrentNodeID.SelectedIndexChanged += new System.EventHandler(this.OnCurrentNodeSelectedIndexChanged);
            // 
            // treeViewDataSummary
            // 
            this.treeViewDataSummary.AllowDrop = true;
            this.treeViewDataSummary.ContextMenuStrip = this.contextMenuAliveData;
            this.treeViewDataSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDataSummary.HideSelection = false;
            this.treeViewDataSummary.ImageIndex = 0;
            this.treeViewDataSummary.ImageList = this.imgListData;
            this.treeViewDataSummary.Location = new System.Drawing.Point(0, 0);
            this.treeViewDataSummary.Name = "treeViewDataSummary";
            this.treeViewDataSummary.SelectedImageIndex = 0;
            this.treeViewDataSummary.Size = new System.Drawing.Size(320, 152);
            this.treeViewDataSummary.TabIndex = 1;
            this.treeViewDataSummary.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnCommStatusTreeViewAfterSelect);
            this.treeViewDataSummary.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnCommStateTreeViewNodeMouseClick);
            // 
            // contextMenuAliveData
            // 
            this.contextMenuAliveData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.contextMenuAliveData.Name = "contextMenuAliveData";
            this.contextMenuAliveData.Size = new System.Drawing.Size(117, 26);
            this.contextMenuAliveData.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuAliveData_Opening);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.clearToolStripMenuItem.Text = "清空(&C)";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // imgListData
            // 
            this.imgListData.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListData.ImageStream")));
            this.imgListData.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListData.Images.SetKeyName(0, "LogRecords_Collapsed");
            this.imgListData.Images.SetKeyName(1, "LogRecords_Expanded");
            this.imgListData.Images.SetKeyName(2, "InputStream");
            this.imgListData.Images.SetKeyName(3, "OutputStream");
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.txtDataDetailed);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.chkSyncRefresh);
            this.splitContainer4.Size = new System.Drawing.Size(282, 338);
            this.splitContainer4.SplitterDistance = 299;
            this.splitContainer4.TabIndex = 3;
            // 
            // txtDataDetailed
            // 
            this.txtDataDetailed.Location = new System.Drawing.Point(3, 3);
            this.txtDataDetailed.Name = "txtDataDetailed";
            this.txtDataDetailed.Size = new System.Drawing.Size(100, 96);
            this.txtDataDetailed.TabIndex = 3;
            this.txtDataDetailed.Text = "";
            // 
            // chkSyncRefresh
            // 
            this.chkSyncRefresh.AutoSize = true;
            this.chkSyncRefresh.Location = new System.Drawing.Point(12, 9);
            this.chkSyncRefresh.Name = "chkSyncRefresh";
            this.chkSyncRefresh.Size = new System.Drawing.Size(72, 16);
            this.chkSyncRefresh.TabIndex = 0;
            this.chkSyncRefresh.Text = "同步刷新";
            this.chkSyncRefresh.UseVisualStyleBackColor = true;
            this.chkSyncRefresh.CheckedChanged += new System.EventHandler(this.chkSyncRefresh_CheckedChanged);
            // 
            // RsspMonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "RsspMonitorControl";
            this.Size = new System.Drawing.Size(624, 371);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuCommStatus.ResumeLayout(false);
            this.contextMenuLog.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.splitContainer13.Panel1.ResumeLayout(false);
            this.splitContainer13.Panel1.PerformLayout();
            this.splitContainer13.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).EndInit();
            this.splitContainer13.ResumeLayout(false);
            this.contextMenuAliveData.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imgListState;
        private System.Windows.Forms.TreeView treeViewNetwork;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.ComboBox cbxCurrentNodeID;
        private System.Windows.Forms.TreeView treeViewDataSummary;
        private System.Windows.Forms.SplitContainer splitContainer13;
        private System.Windows.Forms.ContextMenuStrip contextMenuCommStatus;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExpandAll;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCollapseAll;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewConnectionLog;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuAliveData;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip contextMenuLog;
        private System.Windows.Forms.ToolStripMenuItem clearlogToolStripMenuItem;
        private System.Windows.Forms.ImageList imgListData;
        private System.Windows.Forms.CheckBox chkOutputStreamVisable;
        private System.Windows.Forms.CheckBox chkInputStreamVisable;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.CheckBox chkSyncRefresh;
        private System.Windows.Forms.RichTextBox txtDataDetailed;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.SplitContainer splitContainer3;

    }
}
