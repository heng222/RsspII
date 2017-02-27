/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 9:52:29 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using BJMT.RsspII4net.Events;

namespace BJMT.RsspII4net
{
    /// <summary>
    /// 一个接口，用于描述使用RSSP-II协议的节点。
    /// </summary>
    public interface IRsspNode : IDisposable
    {
        /// <summary>
        /// 打开RSSP-II节点。
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭RSSP-II节点并释放所有连接。
        /// </summary>
        void Close();

        /// <summary>
        /// 发送数据。
        /// </summary>
        void Send(OutgoingPackage package);

        /// <summary>
        /// 关闭与指定设备间的连接。
        /// </summary>
        /// <param name="remoteID">将要关闭的设备。</param>
        void Disconnect(UInt32 remoteID);

        /// <summary>
        /// 获取已连接的设备编号。
        /// </summary>
        List<uint> GetConnectedNodeID();

        /// <summary>
        /// 一个事件，当有日志产生时引发。
        /// </summary>
        event EventHandler<LogCreatedEventArgs> LogCreated;        

        /// <summary>
        /// 一个事件，当TCP/IP正在建立连接时引发。
        /// </summary>
        event EventHandler<TcpConnectingEventArgs> TcpConnecting;
        /// <summary>
        /// 一个事件，当TCP/IP连接成功时引发。
        /// </summary>
        event EventHandler<TcpConnectedEventArgs> TcpConnected;
        /// <summary>
        /// 一个事件，当TCP/IP连接失败时引发。
        /// </summary>
        event EventHandler<TcpConnectFailedEventArgs> TcpConnectFailed;
        /// <summary>
        /// 一个事件，当TCP/IP连接断开时引发。
        /// </summary>
        event EventHandler<TcpDisconnectedEventArgs> TcpDisconnected;
        /// <summary>
        /// 一个事件，当TCP/IP终结点点开始监听时引发。
        /// </summary>
        event EventHandler<TcpEndPointListeningEventArgs> TcpEndPointListening;
        /// <summary>
        /// 一个事件，当TCP/IP终结点监听失败时引发。
        /// </summary>
        event EventHandler<TcpEndPointListenFailedEventArgs> TcpEndPointListenFailed;

        /// <summary>
        /// 一个事件，当节点间连接建立时引发。
        /// </summary>
        event EventHandler<NodeConnectedEventArgs> NodeConnected;
        /// <summary>
        /// 一个事件，当节点间连接断开时引发。
        /// </summary>
        event EventHandler<NodeInterruptionEventArgs> NodeDisconnected;

        /// <summary>
        /// 一个事件，当用户数据到达时引发。
        /// </summary>
        event EventHandler<UserDataIncomingEventArgs> UserDataIncoming;
        /// <summary>
        /// 一个事件，当用户数据发送时引发。
        /// </summary>
        event EventHandler<UserDataOutgoingEventArgs> UserDataOutgoing;

        /// <summary>
        /// 一个事件，当发送缓存中的个数发生变化时引发。
        /// </summary>
        event EventHandler<OutgoingCacheCountChangedEventArgs> OutgoingCacheCountChanged;
        /// <summary>
        /// 一个事件，当接收缓存中的个数发生变化时引发。
        /// </summary>
        event EventHandler<IncomingCacheCountChangedEventArgs> IncomingCacheCountChanged;


        /// <summary>
        /// 一个事件，当接收队列发生延迟现象时引发。
        /// </summary>
        event EventHandler<IncomingCacheDelayedEventArgs> IncomingCacheDelayed;
        /// <summary>
        /// 一个事件，当发送队列发生延迟现象时引发。
        /// </summary>
        event EventHandler<OutgoingCacheDelayedEventArgs> OutgoingCacheDelayed;
    }
}
