/*----------------------------------------------------------------
// 公司名称：????科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 13:46:27 
// 邮    箱：heng222_z@163.com
//
// Copyright (C) ????科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// ALE层连接确认时使用的数据
    /// </summary>
    class AleConnectionConfirm : AleUserData
    {
        /// <summary>
        /// 应答方编号。
        /// </summary>
        public UInt32 ServerID { get; set; }
        
        /// <summary>
        /// 用户数据，即AU2 SaPDU。
        /// </summary>
        public byte[] UserData { get; set; }

        public override ushort Length
        {
            get
            {
                int value = 4;
                if (this.UserData != null)
                {
                    value += this.UserData.Length;
                }

                return (ushort)value;
            }
        }

        
        public AleConnectionConfirm()
            : base(AleFrameType.ConnectionConfirm)
        {

        }


        public AleConnectionConfirm(uint serverID, byte[] userData)
            : this()
        {
            this.ServerID = serverID;
            this.UserData = userData;
        }

        public override byte[] GetBytes()
        {
            var bytes = new byte[this.Length];
            int startIndex = 0;

            // 应答方编号
            var tempBuf = RsspEncoding.ToNetUInt32(this.ServerID);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // 用户数据
            if (this.UserData != null)
            {
                Array.Copy(this.UserData, 0, bytes, startIndex, this.UserData.Length);
            }

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            // 应答方编号
            this.ServerID = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

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
