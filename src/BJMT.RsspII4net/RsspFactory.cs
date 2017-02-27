/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-28 8:59:40 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Config;

namespace BJMT.RsspII4net
{
    /// <summary>
    /// 使用RSSP-II通讯的节点工厂类。
    /// </summary>
    public static class RsspFactory
    {
        /// <summary>
        /// 创建一个RSSP-II客户端。
        /// </summary>
        /// <param name="config">客户端配置信息。</param>
        /// <returns>一个IRsspNode接口。</returns>
        public static IRsspNode CreateClientNode(RsspClientConfig config)
        {
            if (config.ServiceType != ServiceType.D)
            {
                throw new ArgumentException("只支持D类服务类型。");
            }

            return new RsspNodeClient(config);
        }

        /// <summary>
        /// 创建一个RSSP-II服务器。
        /// </summary>
        /// <param name="config">服务器配置信息。</param>
        /// <returns>一个IRsspNode接口。</returns>
        public static IRsspNode CreateServerNode(RsspServerConfig config)
        {
            return new RsspNodeServer(config);
        }
    }
}
