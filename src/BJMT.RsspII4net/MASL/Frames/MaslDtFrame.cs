/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 10:15:11 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.Exceptions;

namespace BJMT.RsspII4net.MASL.Frames
{
    /// <summary>
    /// Data transmission frame.
    /// </summary>
    class MaslDtFrame : MaslFrame
    {
        /// <summary>
        /// 除用户数据外的固定长度。
        /// </summary>
        private const int FixedLenExceptUserData = 9;

        /// <summary>
        /// 用户数据。
        /// </summary>
        public byte[] UserData { get; private set; }
        
        /// <summary>
        /// 消息验证码。
        /// </summary>
        public byte[] MAC { get; set; }

        /// <summary>
        /// 获取用户数据的长度。
        /// </summary>
        public int UserDataLen
        {
            get 
            { 
                if (this.UserData == null)
                {
                    return 0;
                }
                else
                {
                    return this.UserData.Length;
                }
            }
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public MaslDtFrame()
        {
            this.MAC = new byte[8];
        }

        public MaslDtFrame(MaslFrameDirection direction, byte[] userData)
            : base(MaslFrameType.DT, direction, 0)
        {
            this.UserData = userData;
        }

        public override byte[] GetBytes()
        {
            int index = 0;
            var bytes = new byte[FixedLenExceptUserData + this.UserDataLen];

            // ETY + MTI + DF
            bytes[index++] = this.GetHeaderByte();

            // UserData
            var uLen = this.UserDataLen;
            if (uLen != 0)
            {
                Array.Copy(this.UserData, 0, bytes, index, uLen);
                index += uLen;
            }

            // MAC
            Array.Copy(this.MAC, 0, bytes, index, 8);
            index += 8;

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            if ((endIndex - startIndex + 1) < MaslDtFrame.FixedLenExceptUserData)
            {
                throw new DtLengthException();
            }

            var totalLen = endIndex - startIndex + 1;

            // ETY + MTI + DF
            this.ParseHeaderByte(bytes[startIndex++]);

            // UserData
            var uLen = totalLen - FixedLenExceptUserData;
            this.UserData = new byte[uLen];
            Array.Copy(bytes, startIndex, this.UserData, 0, uLen);
            startIndex += uLen;

            // MAC
            Array.Copy(bytes, startIndex, this.MAC, 0, 8);
            startIndex += 8;
        }

    }
}
