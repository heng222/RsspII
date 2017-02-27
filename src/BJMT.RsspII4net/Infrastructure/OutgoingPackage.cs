/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 10:56:39 
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
    /// 表示使用RSSP-II协议发送的数据包。
    /// </summary>
    public class OutgoingPackage
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="DestID">目的地址。</param>
        /// <param name="userData">用户数据。</param>
        public OutgoingPackage(IEnumerable<UInt32> DestID, byte[] userData)
        {
            this.CreationTime = DateTime.Now;
            this.UserData = userData;
            this.DestID = new List<UInt32>(DestID);
        }

        /// <summary>
        /// 获取此参数对象的创建时间。
        /// </summary>
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// 将要发送的用户数据
        /// </summary>
        public byte[] UserData { get; private set; }

        /// <summary>
        /// 用户数据的目的地址列表
        /// </summary>
        public IEnumerable<UInt32> DestID { get; private set; }

        /// <summary>
        /// 用户数据的附加时延（转发数据时可能用到），单位：10ms
        /// </summary>
        public UInt32 ExtraDelay { get; private set; }

        /// <summary>
        /// 获取用户数据在本地接收队列中的排队时延（单位：10毫秒）。
        /// </summary>
        public uint QueuingDelay { get; internal set; }


        /// <summary>
        /// 返回表示输出包的字符串描述信息。
        /// </summary>
        /// <returns>字符串描述信息</returns>
        public override string  ToString()
        {
            var sb = new StringBuilder(512);

            sb.AppendFormat("目的地址：{0}\r\n", string.Join(",", this.DestID.ToArray()));
            sb.AppendFormat("附加时延：{0}（单位：10毫秒）\r\n", this.ExtraDelay);
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
