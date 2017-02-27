/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 15:16:34 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net
{
    /// <summary>
    /// RSSP-II协议终结点定义。
    /// </summary>
    class RsspEndPoint
    {
        /// <summary>
        /// 获取终结点ID
        /// </summary>
        public string ID
        {
            get
            {
                if (this.IsInitiator)
                {
                    return string.Format("Client(LID_{0}, RID_{1})", this.LocalID, this.RemoteID);
                }
                else
                {
                    return string.Format("Server(LID_{0}, RID_{1})", this.LocalID, this.RemoteID);
                }
            }
        }

        /// <summary>
        /// 本节点是否为发起方。
        /// </summary>
        public bool IsInitiator { get; set; }

        /// <summary>
        /// 本地ID。
        /// </summary>
        public uint LocalID { get; set; }
        /// <summary>
        /// 本地设备类型。
        /// </summary>
        public byte LocalEquipType { get; set; }

        /// <summary>
        /// 应用类型。
        /// </summary>
        public byte ApplicatinType { get; set; }

        /// <summary>
        /// 远程ID。
        /// </summary>
        public uint RemoteID { get; set; }
        /// <summary>
        /// 远程设备类型。
        /// </summary>
        public byte RemoteEquipType { get; set; }

        /// <summary>
        /// 服务类型，默认值为D类服务。
        /// </summary>
        public ServiceType ServiceType { get; set; }
        /// <summary>
        /// 消息鉴定使用的加密算法。默认值为3DES。
        /// </summary>
        public EncryptionAlgorithm Algorithm { get; set; }

        /// <summary>
        /// 消息延迟防御技术，默认值是EC。
        /// </summary>
        public MessageDelayDefenseTech DefenseTech { get; set; }

        /// <summary>
        /// 获取/设置192位的验证密钥KeyMAC（24字节）。
        /// </summary>
        public byte[] AuthenticationKeys { get; set; }

        /// <summary>
        /// 获取/设置EC周期值。（仅当使用EC防御技术时有效）
        /// 零值表示无效。
        /// </summary>
        public ushort EcInterval { get; set; }

        /// <summary>
        /// 获取/设置一个值，用于表示消息序列检查参数N。N-1是代表允许丢失消息的数量。需要配置N值，N值等于或大于1。
        /// </summary>
        public byte SeqNoThreshold { get; set; }


        /// <summary>
        /// 获取可接受的客户端列表。 （仅在服务器端有效）
        /// </summary>
        public IEnumerable<KeyValuePair<uint, List<IPEndPoint>>> AcceptableClients { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RsspEndPoint(uint localID, uint remoteID,
            byte appType,
            byte localEquipType,
            bool isInitiator,
            byte seqWinLen,
            ushort ecInterval,
            byte[] macKey, IEnumerable<KeyValuePair<uint, List<IPEndPoint>>> acceptableClients/* = null*/)
        {
            this.ServiceType = ServiceType.D;
            this.Algorithm = EncryptionAlgorithm.TripleDES;
            this.DefenseTech = MessageDelayDefenseTech.EC;
            this.SeqNoThreshold = seqWinLen;

            this.LocalID = localID;
            this.RemoteID = remoteID;
            this.ApplicatinType = appType;
            this.LocalEquipType = localEquipType;
            this.IsInitiator = isInitiator;            
            this.EcInterval = ecInterval;

            this.AuthenticationKeys = new byte[macKey.Length];
            Array.Copy(macKey, this.AuthenticationKeys, this.AuthenticationKeys.Length);

            if (acceptableClients != null && acceptableClients.Count() > 0)
            {
                this.AcceptableClients = new List<KeyValuePair<uint, List<IPEndPoint>>>(acceptableClients);
            }
        }

        public bool IsClientAcceptable(uint clientId, IPEndPoint clientEP)
        {
            if (this.AcceptableClients == null) return true;

            if (this.AcceptableClients.Count() == 0) return true;

            foreach (var item in this.AcceptableClients)
            {
                if(item.Key == clientId)
                {
                    return HelperTools.IsEndPointValid(item.Value, clientEP);
                }
            }

            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(200);

            sb.AppendFormat("本地ID={0}，远程ID={1}，是否发起方={2}，", 
                this.LocalID, this.RemoteID, this.IsInitiator);

            sb.AppendFormat("本地设备类型={0}, 远程设备类型={1}, 应用类型={2}，服务类型={3}，",
                this.LocalEquipType, this.RemoteEquipType, this.ApplicatinType, this.ServiceType);

            sb.AppendFormat("消息序列检查参数N={0}，",
                this.SeqNoThreshold);

            if (this.DefenseTech == MessageDelayDefenseTech.EC)
            {
                sb.AppendFormat("防御技术={0}（周期={1}）。\r\n", this.DefenseTech, this.EcInterval);
            }
            else
            {
                sb.AppendFormat("防御技术={0}。\r\n", this.DefenseTech);
            }

            return sb.ToString();
        }

    }
}
