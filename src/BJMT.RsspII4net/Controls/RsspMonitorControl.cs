/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-22 15:42:55 
// 邮    箱：zhh_217@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using BJMT.RsspII4net.Events;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.Controls
{
    /// <summary>
    /// RSSP-II通讯监视控件。
    /// </summary>
    public partial class RsspMonitorControl : UserControl
    {
        #region "Field"
        /// <summary>
        /// 通讯接口列表
        /// </summary>
        private List<IRsspNode> _commList = new List<IRsspNode>();

        /// <summary>
        /// 事件缓冲池。
        /// </summary>
        private ProductCache<EventArgs> _productCache = new ProductCache<EventArgs>();

        /// <summary>
        /// 当前选择的远程设备名称。
        /// </summary>
        private string _selectedDeviceName = "";

        /// <summary>
        /// 过滤器控件。
        /// </summary>
        private FilterControl _filterControl = null;

        /// <summary>
        /// 缓存个数显示控件。
        /// </summary>
        private CacheCountControl _cacheCountCtrl = new CacheCountControl() { Dock = DockStyle.Fill };

        #endregion

        #region "Property"

        /// <summary>
        /// 邻机节点ID。
        /// </summary>
        public uint SiblingID { get; private set; }

        /// <summary>
        /// 是否显示输入流。
        /// </summary>
        public bool IncomingStreamVisable
        {
            get { return _IncomingStreamVisable; }
            set
            {
                _IncomingStreamVisable = value;
                this.chkInputStreamVisable.Checked = value;
            }
        }
        private bool _IncomingStreamVisable = false;

        /// <summary>
        /// 是否显示输出流。
        /// </summary>
        public bool OutgoingStreamVisable
        {
            get { return _OutgoingStreamVisable; }
            set
            {
                _OutgoingStreamVisable = value;
                this.chkOutputStreamVisable.Checked = value;
            }
        }
        private bool _OutgoingStreamVisable = false;

        /// <summary>
        /// 获取/设置一个值，用于表示是否同步刷新协议帧的详细信息。
        /// </summary>
        public bool SynchronousRefresh
        {
            get { return this.chkSyncRefresh.Checked; }
            set { this.chkSyncRefresh.Checked = value; }
        }

        /// <summary>
        /// 获取网络状态控件上下文菜单的所有项
        /// </summary>
        public ToolStripItemCollection StateContextMenuItems
        {
            get { return this.contextMenuCommStatus.Items; }
        }
        /// <summary>
        /// 获取实时数据控件上下文菜单的所有项
        /// </summary>
        public ToolStripItemCollection AliveDataContextMenuItems { get { return this.contextMenuAliveData.Items; } }
        /// <summary>
        /// 获取日志控件上下文菜单的所有项
        /// </summary>
        public ToolStripItemCollection LogContextMenuItems { get { return this.contextMenuLog.Items; } }

        /// <summary>
        /// 获取所有TabPage。
        /// </summary>
        public TabPage[] TabPages
        {
            get 
            {
                var result = new List<TabPage>();
                var count = this.tabControl1.TabPages.Count;

                for (int i = 0; i < count;  i++)
                {
                    result.Add(this.tabControl1.TabPages[i]);
                }
                
                return result.ToArray();
            }
        }

        /// <summary>
        /// 获取本控件关联的图形列表 
        /// </summary>
        public ImageList ImageList { get { return this.imgListState; } }


        /// <summary>
        /// 获取/设置设备名称解析器接口。
        /// </summary>
        public IRsspNodeNameResolver NameResolver { get; set; }
        /// <summary>
        /// 获取/设置用户协议帧解析器接口。
        /// </summary>
        public IRsspUserDataResolver UserDataResolver { get; set; }
        #endregion


        #region "Constructor && Destructor"
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RsspMonitorControl()
        {
            InitializeComponent();

            this.splitContainer2.Dock = DockStyle.Fill;
            this.treeViewNetwork.Dock = DockStyle.Fill;
            this.listViewConnectionLog.Dock = DockStyle.Fill;
            this.treeViewDataSummary.Dock = DockStyle.Fill;
            this.txtDataDetailed.Dock = DockStyle.Fill;
            this.listViewLog.Dock = DockStyle.Fill;
            this.splitContainer4.IsSplitterFixed = true;

            if (cbxCurrentNodeID.Items.Count > 0)
            {
                cbxCurrentNodeID.SelectedIndex = 0;
            }
            
            _filterControl = new FilterControl() { Dock = DockStyle.Fill };
            groupBoxFilter.Controls.Add(_filterControl);
            
            this.splitContainer3.Panel2.Controls.Add(_cacheCountCtrl);

            // 打开缓存池。
            _productCache.ThreadName = "RsspMonitorControl缓冲池线程";
            _productCache.ProductCreated += OnCacheProductCreated;
            _productCache.Open();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="siblingId">邻机设备ID，如果不存在邻机，则使用空字符串或空引用。</param>
        /// <param name="nameReolver">设备名称解析器接口，可以为空引用。</param>
        /// <param name="frameResolver">协议解析器接口，可以为空引用。</param>
        public RsspMonitorControl(uint siblingId,
            IRsspNodeNameResolver nameReolver,
            IRsspUserDataResolver frameResolver) : this()
        {
            NameResolver = nameReolver;
            UserDataResolver = frameResolver;
            this.SiblingID = siblingId;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="siblingId">邻机设备ID，如果不存在邻机，则使用空字符串或空引用。</param>
        /// <param name="nameReolver">设备名称解析器接口，可以为空引用。</param>
        /// <param name="frameResolver">协议解析器接口，可以为空引用。</param>
        /// <param name="filterProvider">过滤器提供者接口，为空引用时使用默认过滤控件。</param>
        public RsspMonitorControl(uint siblingId,
            IRsspNodeNameResolver nameReolver,
            IRsspUserDataResolver frameResolver,
            IFilterControlProvider filterProvider)
            : this(siblingId, nameReolver, frameResolver)
        {
            if (filterProvider == null) throw new ArgumentNullException();            

            // 初始化过滤控件。
            _filterControl.AddCustomFilter(filterProvider);
        }
        #endregion


        #region "public mehtods"
        
        /// <summary>
        /// 添加通讯接口
        /// </summary>
        /// <param name="handlers">将要添加的通讯接口。</param>
        public void AddCommHandler(IEnumerable<IRsspNode> handlers)
        {
            foreach (var item in handlers)
            {
                this.AddCommHandler(item);
            }
        }

        /// <summary>
        /// 添加通讯接口
        /// </summary>
        /// <param name="handler"></param>
        public void AddCommHandler(IRsspNode handler)
        {
            if (handler != null)
            {
                if (!_commList.Contains(handler))
                {
                    _commList.Add(handler);

                    handler.TcpEndPointListening += OnTcpEndPointListening;
                    handler.TcpEndPointListenFailed += OnTcpEndPointListenFailed;
                    
                    handler.TcpConnecting += OnTcpConnecting;
                    handler.TcpConnected += OnTcpConnected;
                    handler.TcpConnectFailed += OnTcpConnectFailed;
                    handler.TcpDisconnected += OnTcpDisconnected;

                    handler.NodeConnected += OnNodeConnected;
                    handler.NodeDisconnected += OnNodeInterruption;

                    handler.UserDataIncoming += OnUserDataIncoming;
                    handler.UserDataOutgoing += OnUserDataOutgoing;
                    
                    handler.IncomingCacheCountChanged += OnIncomingCacheCountChanged;
                    handler.OutgoingCacheCountChanged += OnOutgoingCacheCountChanged;
                }
            }
        }

        /// <summary>
        /// 移除指定的通讯接口
        /// </summary>
        /// <param name="handler">将要移除的通讯接口。</param>
        public void RemoveCommHandler(IRsspNode handler)
        {
            if (_commList != null)
            {
                if (_commList.Contains(handler))
                {
                    handler.TcpEndPointListening -= OnTcpEndPointListening;
                    handler.TcpEndPointListenFailed -= OnTcpEndPointListenFailed;

                    handler.TcpConnecting -= OnTcpConnecting;
                    handler.TcpConnected -= OnTcpConnected;
                    handler.TcpConnectFailed -= OnTcpConnectFailed;
                    handler.TcpDisconnected -= OnTcpDisconnected;

                    handler.NodeConnected -= OnNodeConnected;
                    handler.NodeDisconnected -= OnNodeInterruption;

                    handler.UserDataIncoming -= OnUserDataIncoming;
                    handler.UserDataOutgoing -= OnUserDataOutgoing;

                    handler.IncomingCacheCountChanged -= OnIncomingCacheCountChanged;
                    handler.OutgoingCacheCountChanged -= OnOutgoingCacheCountChanged;

                    _commList.Remove(handler);
                }
            }
        }
        /// <summary>
        /// 清空通讯接口
        /// </summary>
        public void ClearCommHandler()
        {
            if (_commList != null)
            {
                // 更新UI
                this.treeViewNetwork.Nodes.Clear();
                this.cbxCurrentNodeID.Items.Clear();
                _cacheCountCtrl.Reset();

                // 移除所有通讯接口
                _commList.ForEach(p => this.RemoveCommHandler(p));
                _commList.Clear();
            }
        }
        
        /// <summary>
        /// 增加本地监听点
        /// </summary>
        public TreeNode AddListeningEndPointToTreeview(uint deviceId)
        {
            try
            {
                string deviceListenKey = string.Format("{0}", deviceId);

                // 添加监听设备
                TreeNode[] nodesTemp = treeViewNetwork.Nodes.Find(deviceListenKey, false);

                TreeNode nodeListenDevice = null;
                if (nodesTemp.Length == 0)
                {
                    string showInfo = String.Format("本地监听点: {0}", DecorateDeviceID(deviceId));

                    nodeListenDevice = treeViewNetwork.Nodes.Add(deviceListenKey, showInfo);

                    nodeListenDevice.ForeColor = Color.Blue;
                    nodeListenDevice.ImageKey = "DeviceOnline";
                    nodeListenDevice.SelectedImageKey = "DeviceOnline";
                }
                else
                {
                    nodeListenDevice = nodesTemp[0];
                }

                return nodeListenDevice;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException(string.Format("添加设备监听点{0}时发生错误", deviceId), ex);
            }
        }

        /// <summary>
        /// 更新一个监听点的状态
        /// </summary>
        /// <param name="deviceId">监听点的父级设备ID</param>
        /// <param name="endPoint">监听点</param>
        /// <param name="success">是否监听成功</param>
        public void UpdateListeningPoint(uint deviceId, IPEndPoint endPoint, bool success)
        {
            try
            {
                var deviceListenKey = string.Format("{0}", deviceId);
                var listenKey = String.Format("{0}", endPoint);

                // 查找父级Node
                var nodesTemp = treeViewNetwork.Nodes.Find(deviceListenKey, false);

                TreeNode nodeListenDevice = null;
                if (nodesTemp.Length == 0) // 如果没有找到父级节点，则创建此节点。
                {
                    nodeListenDevice = this.AddListeningEndPointToTreeview(deviceId); 
                }
                else
                {
                    nodeListenDevice = nodesTemp[0];
                }


                // 设置Endpoint节点
                TreeNode endPointListen = null;
                nodesTemp = nodeListenDevice.Nodes.Find(listenKey, false);
                if (nodesTemp.Length == 0) // 如果没有找到Endpoint监听点，则创建此节点
                {
                    endPointListen = nodeListenDevice.Nodes.Add(listenKey, "");
                }
                else
                {
                    endPointListen = nodesTemp[0];
                }

                // 设置监听状态
                if (success)
                {
                    endPointListen.Text = String.Format("Listening {0} since {1}.", endPoint, DateTime.Now);
                    endPointListen.ImageKey = "listen";
                    endPointListen.SelectedImageKey = "listen";
                }
                else
                {
                    endPointListen.Text = String.Format("Listening {0} failed at {1}.", endPoint, DateTime.Now);
                    endPointListen.ImageKey = "fail";
                    endPointListen.SelectedImageKey = "fail";
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(string.Format("添加EndPoint监听点{0}时发生错误", deviceId), ex);
            }
        }

        /// <summary>
        /// 添加一个邻机节点。
        /// </summary>
        public TreeNode AddSibling(uint deviceId)
        {
            try
            {
                string fixedName = DecorateDeviceID(deviceId);

                // 查找Device节点
                TreeNode[] nodesTemp = treeViewNetwork.Nodes.Find(deviceId.ToString(), false);

                // 添加TreeNode
                TreeNode nodeRemoteDevice;
                if (nodesTemp.Length == 0)
                {
                    string showInfo = String.Format("邻机: {0}", fixedName);

                    nodeRemoteDevice = treeViewNetwork.Nodes.Add(deviceId.ToString(), showInfo);

                    // setting
                    nodeRemoteDevice.ForeColor = Color.Red;
                    nodeRemoteDevice.ImageKey = "DeviceOffline";
                    nodeRemoteDevice.SelectedImageKey = "DeviceOffline";
                }
                else
                {
                    nodeRemoteDevice = nodesTemp[0];
                }

                // 添加ComboBox Item
                this.AddSelectableDevice(fixedName);

                return nodeRemoteDevice;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("添加邻机节点时错误", ex);
            }
        }

        /// <summary>
        /// 在TreeView中增加服务器节点
        /// </summary>
        public TreeNode AddServer(uint deviceId)
        {
            try
            {
                string fixedName = DecorateDeviceID(deviceId);

                // 查找Device节点
                TreeNode[] nodesTemp = treeViewNetwork.Nodes.Find(deviceId.ToString(), false);

                // 添加TreeNode
                TreeNode nodeRemoteDevice = null;
                if (nodesTemp.Length == 0)
                {
                    string showInfo = String.Format("服务器: {0}", DecorateDeviceID(deviceId));

                    nodeRemoteDevice = treeViewNetwork.Nodes.Add(deviceId.ToString(), showInfo);

                    // setting
                    nodeRemoteDevice.ForeColor = Color.Red;
                    nodeRemoteDevice.ImageKey = "DeviceOffline";
                    nodeRemoteDevice.SelectedImageKey = "DeviceOffline";
                }
                else
                {
                    nodeRemoteDevice = nodesTemp[0];
                }

                // 添加ComboBox Item
                this.AddSelectableDevice(fixedName);

                return nodeRemoteDevice;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("添加服务器节点时错误", ex);
            }
        }

        /// <summary>
        /// 增加一个客户端节点
        /// </summary>
        public TreeNode AddClient(uint deviceId)
        {
            try
            {
                string fixedName = DecorateDeviceID(deviceId);

                // 查找Device节点
                TreeNode[] nodesTemp = treeViewNetwork.Nodes.Find(deviceId.ToString(), false);

                // 添加TreeNode
                TreeNode nodeRemoteDevice = null;
                if (nodesTemp.Length == 0)
                {
                    string showInfo = String.Format("客户端: {0}", DecorateDeviceID(deviceId));

                    nodeRemoteDevice = treeViewNetwork.Nodes.Add(deviceId.ToString(), showInfo);

                    // setting
                    nodeRemoteDevice.ForeColor = Color.Red;
                    nodeRemoteDevice.ImageKey = "DeviceOffline";
                    nodeRemoteDevice.SelectedImageKey = "DeviceOffline";
                }
                else
                {
                    nodeRemoteDevice = nodesTemp[0];
                }

                // 添加ComboBox Item
                this.AddSelectableDevice(fixedName);
                return nodeRemoteDevice;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("添加客户端节点时错误", ex);
            }
        }

        #endregion


        #region "private methods"
        /// <summary>
        /// 装饰设备ID
        /// </summary>
        private string DecorateDeviceID(uint deviceId)
        {
            if (NameResolver != null)
            {
                return string.Format("{0} - {1}", deviceId, NameResolver.Convert(deviceId));
            }
            else
            {
                return deviceId.ToString();
            }
        }
        /// <summary>
        /// 查找指定的可选设备
        /// </summary>
        private object FindSelectableDevice(string deviceId)
        {
            object result = null;

            foreach (object item in cbxCurrentNodeID.Items)
            {
                if (item.ToString().CompareTo(deviceId) == 0)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 添加一个可选设备
        /// </summary>
        private void AddSelectableDevice(string fixedName)
        {
            if (this.FindSelectableDevice(fixedName) == null)
            {
                cbxCurrentNodeID.Items.Add(fixedName);
            }

            if (cbxCurrentNodeID.Items.Count > 0 && cbxCurrentNodeID.SelectedIndex == -1)
            {
                cbxCurrentNodeID.SelectedIndex = 0;
            }
        }
        


        /// <summary>
        /// 将指定的日志显示到控件
        /// </summary>
        private void ShowLog(string log)
        {
            try
            {
                // clear old data.
                if (listViewLog.Items.Count > 200)
                {
                    listViewLog.Items.Clear();
                }

                // Create one item and three sets of subitems for it. 
                ListViewItem item = new ListViewItem(DateTime.Now.ToString());
                item.SubItems.Add("Info");
                item.SubItems.Add(log);
                listViewLog.Items.Add(item);
            }
            catch (System.Exception)
            {
            }
        }



        private void OnTcpConnecting(object sender, TcpConnectingEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }
        private void OnTcpConnected(object sender, TcpConnectedEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }
        private void OnTcpConnectFailed(object sender, TcpConnectFailedEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }
        private void OnTcpDisconnected(object sender, TcpDisconnectedEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }
        private void OnTcpEndPointListening(object sender, TcpEndPointListeningEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }
        private void OnTcpEndPointListenFailed(object sender, TcpEndPointListenFailedEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }

        private void OnNodeConnected(object sender, NodeConnectedEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }
        private void OnNodeInterruption(object sender, NodeInterruptionEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }

        private void OnUserDataOutgoing(object sender, UserDataOutgoingEventArgs args)
        {
            try
            {
                _productCache.AddTail(args);
            }
            catch (System.Exception)
            {
            }
        }
        private void OnUserDataIncoming(object sender, UserDataIncomingEventArgs e)
        {
            try
            {
                _productCache.AddTail(e);
            }
            catch (System.Exception)
            {
            }
        }

        private void OnOutgoingCacheCountChanged(object sender, OutgoingCacheCountChangedEventArgs args)
        {
            try
            {
                var name = string.Format("{0}发送缓存", args.Name);
                _cacheCountCtrl.ShowCount(name, args.Count);
            }
            catch (System.Exception)
            {
            }
        }
        private void OnIncomingCacheCountChanged(object sender, IncomingCacheCountChangedEventArgs args)
        {
            try
            {
                var name = string.Format("{0}接收缓存", args.Name);
                _cacheCountCtrl.ShowCount(name, args.Count);
            }
            catch (System.Exception)
            {
            }
        }

        private void OnCacheProductCreated(object sender, ProductCreatedEventArgs<EventArgs> e)
        {
            try
            {
                foreach (var args in e.Products)
                {
                    if (args is TcpConnectingEventArgs)
                    {
                        this.ShowTcpConnectingEvent(args as TcpConnectingEventArgs);
                    }
                    else if (args is TcpConnectedEventArgs)
                    {
                        this.ShowTcpConnectedEvent(args as TcpConnectedEventArgs);
                    }
                    else if (args is TcpConnectFailedEventArgs)
                    {
                        this.ShowTcpConnectFailedEvent(args as TcpConnectFailedEventArgs);
                    }
                    else if (args is TcpDisconnectedEventArgs)
                    {
                        this.ShowTcpDisconnectedEvent(args as TcpDisconnectedEventArgs);
                    }
                    else if (args is TcpEndPointListeningEventArgs)
                    {
                        this.ShowTcpEndPointListeningEvent(args as TcpEndPointListeningEventArgs);
                    }
                    else if (args is TcpEndPointListenFailedEventArgs)
                    {
                        this.ShowTcpEndPointListenFailedEvent(args as TcpEndPointListenFailedEventArgs);
                    }
                    else if (args is NodeConnectedEventArgs)
                    {
                        var theArgs = args as NodeConnectedEventArgs;
                        this.ShowNodeConnectedEvent(theArgs);
                        this.ShowNodeConnectionChangedOnLogListView(theArgs.LocalID, theArgs.RemoteID, true);
                    }
                    else if (args is NodeInterruptionEventArgs)
                    {
                        var theArgs = args as NodeInterruptionEventArgs;
                        this.ShowNodeInterruptionEvent(theArgs);
                        this.ShowNodeConnectionChangedOnLogListView(theArgs.LocalID, theArgs.RemoteID, false);
                    }
                    else if (args is UserDataOutgoingEventArgs)
                    {
                        this.ShowOutgoingUserDataEvent(args as UserDataOutgoingEventArgs);
                    }
                    else if (args is UserDataIncomingEventArgs)
                    {
                        this.ShowIncomingUserDataEvent(args as UserDataIncomingEventArgs);
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }
        
        private void ShowTcpEndPointListeningEvent(TcpEndPointListeningEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    this.UpdateListeningPoint(args.LocalID, args.EndPoint, true);
                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }
        private void ShowTcpEndPointListenFailedEvent(TcpEndPointListenFailedEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    this.UpdateListeningPoint(args.LocalID, args.EndPoint, false);
                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }
        private void ShowTcpConnectingEvent(TcpConnectingEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    // 添加Device节点
                    TreeNode nodeRemoteDevice = null;
                    if (args.RemoteID == this.SiblingID)
                    {
                        nodeRemoteDevice = this.AddSibling(this.SiblingID);
                    }
                    else
                    {
                        nodeRemoteDevice = this.AddServer(args.RemoteID);
                    }

                    // 添加Connection节点
                    var nodeKey = args.ConnectionID;
                    var linkContent = String.Format("尝试连接中: LEP_{0} ---> REP_{1} 开始于 {2}",
                                args.LocalEndPoint.Address, args.RemoteEndPoint, DateTime.Now.ToString("HH:mm:ss"));
                    var nodesTemp = nodeRemoteDevice.Nodes.Find(nodeKey, false);
                    TreeNode nodeConnection = null;
                    if (nodesTemp.Length > 0)
                    {
                        nodeConnection = nodesTemp[0];
                        nodeConnection.Text = linkContent;
                    }
                    else
                    {
                        nodeConnection = nodeRemoteDevice.Nodes.Add(nodeKey, linkContent);
                    }
                    nodeConnection.ForeColor = Color.Gray;
                    nodeConnection.ImageKey = "connecting";
                    nodeConnection.SelectedImageKey = "connecting";

                    //
                    nodeRemoteDevice.ExpandAll();
                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }
        private void ShowTcpConnectedEvent(TcpConnectedEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    var nodeKey = args.ConnectionID;
                    var nodeText = String.Format("LEP_{0} + REP_{1}, Established at {2}.", args.LocalEndPoint, args.RemoteEndPoint, DateTime.Now.ToString());

                    // 添加RemoteNode
                    TreeNode theRemoteNode = null;
                    var theRemoteNodes = treeViewNetwork.Nodes.Find(args.RemoteID.ToString(), false);

                    // 更新网络状态图
                    TreeNode theLinkNode = null;

                    if (theRemoteNodes.Length == 0)
                    {
                        if (args.RemoteID == this.SiblingID)
                        {
                            theRemoteNode = this.AddSibling(this.SiblingID);
                        }
                        else
                        {
                            theRemoteNode = this.AddClient(args.RemoteID);
                        }
                    }
                    else
                    {
                        theRemoteNode = theRemoteNodes[0];
                    }

                    // 查找TcpLink对应的节点。
                    var nodesTemp = theRemoteNode.Nodes.Find(nodeKey, false);
                    if (nodesTemp.Length > 0)
                    {
                        theLinkNode = nodesTemp[0];
                        theLinkNode.Text = nodeText;
                    }
                    else
                    {
                        theLinkNode = theRemoteNode.Nodes.Add(nodeKey, nodeText);
                    }
                    theLinkNode.ForeColor = Color.Black;
                    theLinkNode.ImageKey = "success";
                    theLinkNode.SelectedImageKey = "success";                    
                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }
        private void ShowTcpConnectFailedEvent(TcpConnectFailedEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {

                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }
        private void ShowTcpDisconnectedEvent(TcpDisconnectedEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    var nodeKey = args.ConnectionID;

                    // 更新网络状态图
                    var nodeDevice = treeViewNetwork.Nodes.Find(args.RemoteID.ToString(), false);
                    if (nodeDevice.Length != 0)
                    {
                        TreeNode[] nodeSub = nodeDevice[0].Nodes.Find(nodeKey, false);
                        if (nodeSub.Length > 0)
                        {
                            nodeDevice[0].Nodes.Remove(nodeSub[0]);
                        }
                    }
                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }

        private void ShowNodeConnectedEvent(NodeConnectedEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    // 添加
                    var fixedName = this.DecorateDeviceID(args.RemoteID);
                    this.AddSelectableDevice(fixedName);

                    // 添加/查找Device节点
                    TreeNode theRemoteNode = null;

                    // 更新网络状态图
                    var nodeDevice = this.treeViewNetwork.Nodes.Find(args.RemoteID.ToString(), false);
                    if (nodeDevice.Length != 0)
                    {
                        theRemoteNode = nodeDevice[0];
                    }
                    else
                    {
                        if (args.RemoteID == this.SiblingID)
                        {
                            theRemoteNode = this.AddSibling(args.RemoteID);
                        }
                        else
                        {
                            theRemoteNode = this.AddClient(args.RemoteID);
                        }
                    }

                    theRemoteNode.ImageKey = "DeviceOnline";
                    theRemoteNode.SelectedImageKey = "DeviceOnline";
                    theRemoteNode.ForeColor = Color.Blue;
                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }
        private void ShowNodeInterruptionEvent(NodeInterruptionEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    // 更新网络状态图
                    var nodeDevice = treeViewNetwork.Nodes.Find(args.RemoteID.ToString(), false);
                    if (nodeDevice.Length != 0)
                    {
                        nodeDevice[0].ImageKey = "DeviceOffline";
                        nodeDevice[0].SelectedImageKey = "DeviceOffline";
                        nodeDevice[0].BackColor = treeViewNetwork.BackColor;
                        nodeDevice[0].ForeColor = Color.Red;
                    }
                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }
        private void ShowNodeConnectionChangedOnLogListView(uint localID, uint remoteID, bool connected)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    if (listViewConnectionLog.Items.Count > 200)
                    {
                        listViewConnectionLog.Items.RemoveAt(0);
                    }

                    // 
                    string fixedName = this.DecorateDeviceID(remoteID);

                    var lvItem = new ListViewItem(DateTime.Now.ToString());
                    if (connected)
                    {
                        lvItem.SubItems.Add(string.Format("与设备({0})建立连接", fixedName));
                    }
                    else
                    {
                        lvItem.SubItems.Add(string.Format("与设备({0})断开连接", fixedName));
                        lvItem.BackColor = Color.Yellow;
                    }

                    this.listViewConnectionLog.Items.Add(lvItem);
                }));
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }

        private void ShowIncomingUserDataEvent(UserDataIncomingEventArgs args)
        {
            try
            {
                if (this.IncomingStreamVisable)
                {
                    this.Invoke(new Action(() =>
                    {
                        // 当前选择的设备与args中的设备是否一致
                        string fixedName = this.DecorateDeviceID(args.Package.RemoteID);
                        bool selected = (_selectedDeviceName.CompareTo(fixedName) == 0);

                        if (selected && _filterControl.Filter(args.Package.UserData))
                        {
                            // clear 
                            if (treeViewDataSummary.Nodes.Count > 100)
                            {
                                treeViewDataSummary.Nodes.RemoveAt(0);
                            }

                            // add
                            TreeNode node = new TreeNode();
                            node.Tag = args;

                            // 获取自定义标签
                            string customLable;
                            if (UserDataResolver != null)
                            {
                                customLable = string.Format("{0},{1}",
                                    UserDataResolver.GetLabel(args.Package.UserData, true),
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            }
                            else
                            {
                                customLable = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            }

                            // set node text
                            node.Text = String.Format("{0}, Delay = {1}", customLable, args.Package.TransmissionDelay);

                            // set node image.
                            node.ImageKey = "InputStream";
                            node.SelectedImageKey = node.ImageKey;

                            // Add Node
                            treeViewDataSummary.Nodes.Add(node);
                        }
                    }));
                }
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }        
        private void ShowOutgoingUserDataEvent(UserDataOutgoingEventArgs args)
        {
            try
            {
                if (this.OutgoingStreamVisable)
                {
                    this.Invoke(new Action(() =>
                    {
                        #region "当前选择的设备是否为指定的目标设备之一"
                        bool selected = false;

                        foreach (var item in args.Package.DestID)
                        {
                            var fixedName = this.DecorateDeviceID(item);
                            selected = (_selectedDeviceName.CompareTo(fixedName) == 0);

                            if (selected) break;
                        }
                        #endregion

                        if (selected && _filterControl.Filter(args.Package.UserData))
                        {
                            // clear 
                            if (treeViewDataSummary.Nodes.Count > 100)
                            {
                                treeViewDataSummary.Nodes.RemoveAt(0);
                            }

                            // add
                            var node = new TreeNode();
                            node.Tag = args;

                            // 获取自定义标签
                            if (UserDataResolver != null)
                            {
                                node.Text = string.Format("{0},{1}",
                                    UserDataResolver.GetLabel(args.Package.UserData, false),
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            }
                            else
                            {
                                node.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            }

                            node.ForeColor = Color.Blue;

                            // set node image.
                            node.ImageKey = "OutputStream";
                            node.SelectedImageKey = node.ImageKey;

                            // Add Node
                            treeViewDataSummary.Nodes.Add(node);
                        }
                    }));
                }
            }
            catch (System.Exception ex)
            {
                this.ShowLog(ex.Message);
            }
        }

        private void ShowSelectedNodeAttachedData()
        {
            if (this.treeViewDataSummary.SelectedNode == null) return;

            try
            {
                var iArgs = treeViewDataSummary.SelectedNode.Tag as UserDataIncomingEventArgs;
                IEnumerable<byte> userData = null;

                if (iArgs != null)
                {
                    userData = iArgs.Package.UserData;
                    txtDataDetailed.Text = string.Format("{0}\r\n", iArgs.Package);
                }
                else
                {
                    var oArgs = treeViewDataSummary.SelectedNode.Tag as UserDataOutgoingEventArgs;
                    userData = oArgs.Package.UserData;

                    txtDataDetailed.Text = String.Format("{0} \r\n", oArgs.Package);
                }

                // 显示用户数据内容
                try
                {
                    if (UserDataResolver != null)
                    {
                        txtDataDetailed.Text += UserDataResolver.GetDescription(userData, iArgs != null);
                    }
                }
                catch (System.Exception ex)
                {
                    txtDataDetailed.Text += "解析用户数据失败，" + ex.Message;
                }
            }
            catch (System.Exception ex)
            {
                txtDataDetailed.Text += ex.ToString();
            }
        }
        #endregion


        #region "控件事件"

        /// <summary>
        /// 状态树节点单击事件
        /// </summary>
        private void OnCommStateTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                // 仅当鼠标点击才会响应，键盘的上下键操作不会响应。
            }
            catch (System.Exception)
            {
                //txtDataDetailed.Text += ex.ToString();
            }
        }

        /// <summary>
        /// 当前选中的设备发生变化
        /// </summary>
        private void OnCurrentNodeSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                treeViewDataSummary.Nodes.Clear();
                txtDataDetailed.Clear();
                _selectedDeviceName = cbxCurrentNodeID.Text;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 数据摘要树形控件选择事件
        /// </summary>
        private void OnCommStatusTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.chkSyncRefresh.Checked)
                {
                    ShowSelectedNodeAttachedData();
                }
            }
            catch (System.Exception ex)
            {
                txtDataDetailed.Text += ex.ToString();
            }
        }

        private void chkInputStreamVisable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this._IncomingStreamVisable = this.chkInputStreamVisable.Checked;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkOutputStreamVisable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this._OutgoingStreamVisable = this.chkOutputStreamVisable.Checked;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void chkSyncRefresh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chkSyncRefresh.Checked)
                {
                    ShowSelectedNodeAttachedData();
                }
                else
                {
                    txtDataDetailed.Clear();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region "ContexMenu事件"

        private void contextMenuAliveData_Opening(object sender, CancelEventArgs e)
        {
            try
            {

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 全部展开
        /// </summary>
        private void ToolStripMenuItemExpandAll_Click(object sender, EventArgs e)
        {
            try
            {
                treeViewNetwork.ExpandAll();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 全部折叠
        /// </summary>
        private void ToolStripMenuItemCollapseAll_Click(object sender, EventArgs e)
        {
            try
            {
                treeViewNetwork.CollapseAll();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 清空摘要信息。
        /// </summary>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.treeViewDataSummary.Nodes.Clear();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        private void clearlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.listViewLog.Items.Clear();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
