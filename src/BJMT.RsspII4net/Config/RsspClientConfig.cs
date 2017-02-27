/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-25 10:20:45 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.Config
{
    /// <summary>
    /// RSSP客户端配置。
    /// </summary>
    public class RsspClientConfig : RsspConfig
    {
        /// <summary>
        /// 与服务器的链路配置。
        /// Key = 服务器编号。
        /// </summary>
        public Dictionary<uint, List<RsspTcpLinkConfig>> LinkInfo { get; set; }
        
        /// <summary>
        /// 构造一个RSSP客户端配置。
        /// </summary>
        /// <param name="localID">本地编号。</param>
        /// <param name="localEquipType">本地设备类型。</param>
        /// <param name="appType">应用类型</param>
        /// <param name="defenseTech"></param>
        /// <param name="links">链路配置信息。</param>
        public RsspClientConfig(uint localID, byte localEquipType, 
            byte appType,
            MessageDelayDefenseTech defenseTech,
            Dictionary<uint, List<RsspTcpLinkConfig>> links)
        {
            if (links == null || links.Count == 0)
            {
                throw new ArgumentException("必须至少指定一条TCP链路。");
            }

            this.IsInitiator = true;
            this.LocalID = localID;
            this.LocalEquipType = localEquipType;
            this.ApplicationType = appType;
            this.DefenseTech = defenseTech;

            this.LinkInfo = new Dictionary<uint, List<RsspTcpLinkConfig>>(links);
        }
    }
}
