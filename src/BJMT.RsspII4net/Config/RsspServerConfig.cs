/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-25 10:20:58 
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
    /// RSSP-II服务器配置
    /// </summary>
    public class RsspServerConfig : RsspConfig
    {
        /// <summary>
        /// 传输层监听终结点。
        /// </summary>
        public IEnumerable<IPEndPoint> ListenEndPoints { get; private set; }
        
        /// <summary>
        /// 获取可接受的客户端列表。 
        /// </summary>
        public IEnumerable<KeyValuePair<uint, List<IPEndPoint>>> AcceptableClients { get; private set; }

        /// <summary>
        /// 构造一个服务器配置对象。
        /// </summary>
        /// <param name="localID">本地节点ID。</param>
        /// <param name="localEquipType">本地设备类型。</param>
        /// <param name="appType">应用类型。</param>
        /// <param name="ep">监听终结点列表。</param>
        /// <param name="acceptableClients">可接受的客户端列表，为空引用时表示接收所有客户端；不为空时表示可以接受的客户端列表。</param>
        public RsspServerConfig(uint localID, byte localEquipType,
            byte appType, IEnumerable<IPEndPoint> ep, 
            IEnumerable<KeyValuePair<uint, List<IPEndPoint>>> acceptableClients)
        {
            this.IsInitiator = false;
            this.LocalID = localID;
            this.LocalEquipType = localEquipType;
            this.ApplicationType = appType;

            this.ListenEndPoints = new List<IPEndPoint>(ep);

            if (acceptableClients != null)
            {
                this.AcceptableClients = new List<KeyValuePair<uint, List<IPEndPoint>>>(acceptableClients);
            }
        }
    }
}
