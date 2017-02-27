/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 13:48:05 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// ALE层数据传输结构定义。
    /// </summary>
    class AleDataTransmission : AleUserData
    {
        /// <summary>
        /// 用户数据，即AU3/AR/DT SaPDU。
        /// </summary>
        public byte[] UserData { get; set; }

        public override ushort Length
        {
            get
            {
                int value = 0;
                if (this.UserData != null)
                {
                    value += this.UserData.Length;
                }

                return (ushort)value;
            }
        }

        public AleDataTransmission()
            : base(AleFrameType.DataTransmission)
        {

        }

        public AleDataTransmission(byte[] userData)
            : this()
        {
            this.UserData = userData;
        }

        public override byte[] GetBytes()
        {
            var bytes = new byte[this.Length];
            int startIndex = 0;

            // 用户数据
            if (this.UserData != null)
            {
                Array.Copy(this.UserData, 0, bytes, startIndex, this.UserData.Length);
            }

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            // 用户数据
            var len = endIndex - startIndex + 1;
            if (len > 0)
            {
                this.UserData = new byte[len];
                Array.Copy(bytes, startIndex, this.UserData, 0, len);
            }
        }
    }
}
