/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-25 10:11:54 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace BJMT.RsspII4net.Config
{
    /// <summary>
    /// RSSP-II传输层链路配置类。
    /// </summary>
    public class RsspTcpLinkConfig
    {
        /// <summary>
        /// 客户端IP地址。
        /// </summary>
        public IPAddress ClientIpAddress { get; set; }

        /// <summary>
        /// 服务器终结点。
        /// </summary>
        public IPEndPoint ServerEndPoint { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientIP">客户端IP地址。</param>
        /// <param name="serverEP">服务器终结点。</param>
        public RsspTcpLinkConfig(IPAddress clientIP, IPEndPoint serverEP)
        {
            this.ClientIpAddress = clientIP;
            this.ServerEndPoint = serverEP;
        }
    }
}
