/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 15:49:25 
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
    /// RSSP节点配置类。
    /// </summary>
    public class RsspConfig
    {
        /// <summary>
        /// 本地编号。
        /// </summary>
        public uint LocalID { get; set; }

        /// <summary>
        /// 应用类型。
        /// </summary>
        public byte ApplicationType { get; set; }

        /// <summary>
        /// 本地设备类型。
        /// </summary>
        public byte LocalEquipType { get; set; }

        /// <summary>
        /// 是否为主动发起方。
        /// </summary>
        public bool IsInitiator { get; set; }

        /// <summary>
        /// 获取/设置消息延迟防御技术，默认EC技术。
        /// </summary>
        public MessageDelayDefenseTech DefenseTech { get; set; }

        /// <summary>
        /// 获取/设置服务类型，默认D类服务。
        /// </summary>
        public ServiceType @ServiceType { get; set; }

        /// <summary>
        /// 消息鉴定使用的加密算法。默认值为3DES。
        /// </summary>
        public EncryptionAlgorithm Algorithm { get; set; }

        /// <summary>
        /// 获取/设置192位的验证密钥（24字节）。
        /// </summary>
        public byte[] AuthenticationKeys { get; set; }

        /// <summary>
        /// 获取/设置EC周期值，默认值1000毫秒。（仅当使用EC防御技术时有效）
        /// </summary>
        public ushort EcInterval { get; set; }

        /// <summary>
        /// 获取/设置一个值，用于表示消息序列检查参数N。N-1是代表允许丢失消息的数量。
        /// 需要配置N值，N值等于或大于1。默认值3。
        /// </summary>
        public byte SeqNoThreshold { get; set; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public RsspConfig()
        {
            this.DefenseTech = MessageDelayDefenseTech.EC;
            this.Algorithm = EncryptionAlgorithm.TripleDES;
            this.ServiceType = ServiceType.D;
            this.EcInterval = 1000;
            this.SeqNoThreshold = 3;

            // 默认值。
            this.AuthenticationKeys = new byte[] 
            { 
                0xA3, 0x45, 0x34, 0x68, 0x98, 0x01, 0x2A, 0xBF,
                0xCD, 0xBE, 0x34, 0x56, 0x78, 0xBF, 0xEA, 0x32,
                0x12, 0xAE, 0x34, 0x21, 0x45, 0x78, 0x98, 0x50
            };
        }
    }
}
