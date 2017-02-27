/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 15:46:33 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.SAI.EC.Frames
{
    class SaiEcFrameApplication : SaiEcFrame
    {
        /// <summary>
        /// 用户数据
        /// </summary>
        public byte[] UserData { get; set; }

        /// <summary>
        /// 获取用户数据的长度。
        /// </summary>
        public int UserDataLength
        {
            get
            {
                if (this.UserData != null)
                {
                    return this.UserData.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaiEcFrameApplication()
            : base(SaiFrameType.EC_AppData, 0)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaiEcFrameApplication(ushort seqNo, uint ecValue, byte[] userData)
            : base(SaiFrameType.EC_AppData, seqNo)
        {
            this.EcValue = ecValue;
            this.UserData = userData;
        }

        public override byte[] GetBytes()
        {
            if (this.UserDataLength > SaiFrame.MaxUserDataLength)
            {
                throw new ArgumentException(string.Format("SAI层用户数据长度不能超过{0}。", SaiFrame.MaxUserDataLength));
            }

            var bytes = new byte[19 + this.UserDataLength];
            int startIndex = 0;

            // 消息类型
            bytes[startIndex++] = (byte)this.FrameType;

            // 序列号
            var tempBuf = RsspEncoding.ToNetUInt16(this.SequenceNo);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 2;

            // Padding
            startIndex += SaiFrame.TtsPaddingLength;

            // EC计数
            tempBuf = RsspEncoding.ToNetUInt32(this.EcValue);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // user data
            if (this.UserData != null)
            {
                Array.Copy(this.UserData, 0, bytes, startIndex, this.UserData.Length);
            }

            return bytes;
        }

        public override void ParseBytes(byte[] bytes)
        {
            int startIndex = 0;

            // 消息类型
            this.FrameType = (SaiFrameType)bytes[startIndex++];

            // 序列号
            this.SequenceNo = RsspEncoding.ToHostUInt16(bytes, startIndex);
            startIndex += 2;

            // Padding
            startIndex += SaiFrame.TtsPaddingLength;

            // EC计数
            this.EcValue = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

            // user data
            var len = bytes.Length - startIndex;
            if (len > 0)
            {
                this.UserData = new byte[len];
                Array.Copy(bytes, startIndex, this.UserData, 0, len);
            }
        }

    }
}
