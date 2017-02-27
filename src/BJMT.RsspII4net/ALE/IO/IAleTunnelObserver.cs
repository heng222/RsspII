/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 14:44:25 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.ALE.Frames;

namespace BJMT.RsspII4net.ALE
{
    interface IAleTunnelObserver
    {
        /// <summary>
        /// 当TCP连接断开时。
        /// </summary>
        /// <param name="theConnection"></param>
        /// <param name="reason"></param>
        void OnTcpDisconnected(AleTunnel theConnection, string reason);

        /// <summary>
        /// 当收到ALE协议帧时。
        /// </summary>
        /// <param name="theConnection"></param>
        /// <param name="theFrame"></param>
        void OnAleFrameArrival(AleTunnel theConnection, AleFrame theFrame);
    }

    interface IAleClientTunnelObserver : IAleTunnelObserver
    {
        /// <summary>
        /// 当TCP客户端正在连接服务器时。
        /// </summary>
        /// <param name="theConnection"></param>
        void OnTcpConnecting(AleClientTunnel theConnection);

        /// <summary>
        /// 当TCP客户端连接到服务器时。
        /// </summary>
        void OnTcpConnected(AleClientTunnel theConnection);

        /// <summary>
        /// 当TCP客户端连接到服务器失败时。
        /// </summary>
        /// <param name="theConnection"></param>
        /// <param name="reason"></param>
        void OnTcpConnectFailure(AleClientTunnel theConnection, string reason);
    }

    interface IAleServerTunnelObserver : IAleTunnelObserver
    {

    }
}
