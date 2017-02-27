/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 11:39:33 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net
{
    /// <summary>
    /// 表示使用RSSP-II协议接收的数据包。
    /// </summary>
    public class IncomingPackage
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="remoteID">远程节点ID</param>
        /// <param name="userData">用户数据</param>
        /// <param name="timeDelay">传输时延。</param>
        /// <param name="defenseTech">消息延迟检测使用的防御技术。</param>
        public IncomingPackage(uint remoteID, byte[] userData, long timeDelay, MessageDelayDefenseTech defenseTech)
        {
            this.CreationTime = DateTime.Now;
            this.RemoteID = remoteID;
            this.UserData = userData;
            this.TransmissionDelay = timeDelay;
            this.DefenseTech = defenseTech;
        }

        /// <summary>
        /// 获取此参数对象的创建时间。
        /// </summary>
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// 获取数据发送方的节点ID。
        /// </summary>
        public uint RemoteID { get; private set; }

        /// <summary>
        /// 获取收到的用户数据。
        /// </summary>
        public byte[] UserData { get; private set; }

        /// <summary>
        /// 获取用户数据的传输时延。
        /// 此值小于零时无效，大于0时表示数据的发送时延（单位：10毫秒）。
        /// </summary>
        public long TransmissionDelay { get; private set; }

        /// <summary>
        /// 获取用户数据在本地接收队列中的排队时延（单位：10毫秒）。
        /// </summary>
        public long QueuingDelay { get; internal set; }

        /// <summary>
        /// 获取消息延迟使用的防御技术。
        /// </summary>
        public MessageDelayDefenseTech DefenseTech { get; private set; }


        /// <summary>
        /// 返回表示输入包的字符串描述信息。
        /// </summary>
        /// <returns>字符串描述信息</returns>
        public override string ToString()
        {
            var sb = new StringBuilder(512);

            sb.AppendFormat("发送方ID：{0}\r\n", this.RemoteID);
            sb.AppendFormat("防御技术：{0}\r\n", this.DefenseTech);
            sb.AppendFormat("传输时延：{0}（单位：10毫秒）\r\n", this.TransmissionDelay);
            sb.AppendFormat("排队时延：{0}（单位：10毫秒）\r\n", this.QueuingDelay);       

            if (this.UserData != null)
            {
                sb.AppendFormat("用户数据（{0}）：\r\n", this.UserData.Length);
                foreach (byte data in this.UserData)
                {
                    sb.AppendFormat("{0:X2} ", data);
                }
            }
            else
            {
                sb.AppendFormat("用户数据：无\r\n", this.UserData.Length);
            }

            return sb.ToString();
        }
    }
}
