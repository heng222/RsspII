using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using BJMT.Log.Presentation;
using BJMT.RsspII4net.Controls;
using BJMT.RsspII4net.Events;
using BJMT.RsspII4net.ITest.Infrastructure;
using BJMT.RsspII4net.ITest.Utilities;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ITest.Presentation
{
    partial class MainForm : Form
    {
        #region "Field"
        /// <summary>
        /// Comm接口生成器
        /// </summary>
        private CommFactory _commFactory;
        /// <summary>
        /// 通讯接口
        /// </summary>
        private IRsspNode _rsspNodeComm = null;

        /// <summary>
        /// 配置页面
        /// </summary>
        private ICommConfigProvider _settingPage;

        /// <summary>
        /// 通讯接口状态显示控件
        /// </summary>
        private RsspMonitorControl _commMonitorControl = new RsspMonitorControl() { Dock = DockStyle.Fill };
        /// <summary>
        /// 数据发送控件
        /// </summary>
        private DataSendingControl _dataSendingControl = new DataSendingControl() { Dock = DockStyle.Fill };

        private CommSnapshotUserControl _snapshotControl = new CommSnapshotUserControl() { Dock = DockStyle.Fill };

        private FileManager _fileManager = new FileManager();
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tcpClient">true表示显示Client界面 ，否则显示Server界面</param>
        public MainForm(bool tcpClient)
        {
            InitializeComponent();
            this.CreateGraphics();

            // 创建相关子控件
            CreateSubControls(tcpClient);

            // 设置标题
            this.UpdateCaptital();
        }

        #region "控件事件"
                         
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (_rsspNodeComm != null)
                {
                    throw new ApplicationException("通信接口已经处于打开状态。");
                }    
                
                // Create
                _rsspNodeComm = _commFactory.Create();
                _rsspNodeComm.LogCreated += OnRsspCommLogCreated;
                _rsspNodeComm.UserDataIncoming += this.OnUserDataIncoming;

                _commMonitorControl.ClearCommHandler();
                _commMonitorControl.AddCommHandler(_rsspNodeComm);

                // Open
                _rsspNodeComm.Open();

                // 设置数据发送控件
                _dataSendingControl.NodeComm = _rsspNodeComm;
                _snapshotControl.CommNode = _rsspNodeComm;

                // 更新界面
                _settingPage.UpdateControl(true);

                // 
                btnOpen.Enabled = false;
            }
            catch (System.Exception ex)
            {
                _settingPage.UpdateControl(false);
                btnOpen.Enabled = true;

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                CloseResource();

                // 更新界面
                _settingPage.UpdateControl(false);
                btnOpen.Enabled = true;
            }
            catch (System.Exception /*ex*/)
            {
                btnOpen.Enabled = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                _dataSendingControl.StartSending();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// 关闭窗口时
        /// </summary>
        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseResource();
        }

        #endregion

        #region "private methods"

        private void CreateSubControls(bool isTcpClient)
        {
            // 配置页面与工厂
            if (isTcpClient)
            {
                _settingPage = new ClientConfigControl();
                _commFactory = new ClientCommFactory(_settingPage as IClientConfigProvider);
            }
            else
            {
                _settingPage = new ServerConfigControl();
                _commFactory = new ServerCommFactory(_settingPage as IServerConfigProvider);
            }
            _settingPage.LocalNodeIdChanged += this.OnLocalNodeIdChanged;
            this.AddTabPage("配置", _settingPage.View);


            // 创建状态控件并设置相关参数
            _commMonitorControl.CreateGraphics();
            _commMonitorControl.Dock = DockStyle.Fill;
            this.AddTabPage("状态", _commMonitorControl);
            //this.tabControlMain.ImageList = _commMonitorControl.ImageList;
            //this.tabControlMain.TabPages.AddRange(_commMonitorControl.TabPages);

            // 创建数据发送控件
            this.AddTabPage("数据发送", _dataSendingControl);

            // 创建日志控件
            this.AddTabPage("日志", new LogControlMultiPages() { Dock = DockStyle.Fill });
            
            // 快照
            this.AddTabPage("快照", _snapshotControl);
        }

        private void AddTabPage(string text, Control control)
        {
            var newTabPage = new TabPage(text);
            newTabPage.Controls.Add(control);
            this.tabControlMain.TabPages.Add(newTabPage);
        }

        private void OnLocalNodeIdChanged(object sender, EventArgs args)
        {
            try
            {
                UpdateCaptital();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
        }

        private void UpdateCaptital()
        {
            this.Text = String.Format("RSSP-II通信组件测试 v{0}（{1}）",
                Assembly.GetExecutingAssembly().GetName().Version, _settingPage.Title);
        }
        
        private void CloseResource()
        {
            try
            {
                _dataSendingControl.StopSending();

                if (_rsspNodeComm != null)
                {
                    _rsspNodeComm.Dispose();
                    _rsspNodeComm.LogCreated -= OnRsspCommLogCreated;
                    _rsspNodeComm.UserDataIncoming -= this.OnUserDataIncoming;
                }

                _commMonitorControl.ClearCommHandler();

                _fileManager.Clear();
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                _rsspNodeComm = null;
            }
        }
        #endregion


        #region "IRsspNode事件处理函数"

        private void OnRsspCommLogCreated(object sender, LogCreatedEventArgs e)
        {
            if (e.IsInfo)
            {
                LogUtility.Info(e.Message);
            }
            else if (e.IsWarning)
            {
                LogUtility.Warning(e.Message);
            }
            else
            {
                LogUtility.Error(e.Message);
            }
        }

        /// <summary>
        /// 处理收到的用户数据
        /// </summary>
        private void OnUserDataIncoming(object sender, UserDataIncomingEventArgs args)
        {
            try
            {
                // 保存
                if (_settingPage.UserDataStorageEnabled)
                {
                    _fileManager.SaveUserData(args.Package.RemoteID, args.Package.UserData);
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex);
            }
        }
        #endregion
    }
}