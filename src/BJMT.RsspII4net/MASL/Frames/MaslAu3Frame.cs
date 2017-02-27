/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 10:14:34 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.Exceptions;

namespace BJMT.RsspII4net.MASL.Frames
{
    class MaslAu3Frame : MaslFrame
    {
        private const int FrameLength = 9;
        
        /// <summary>
        /// 消息验证码。
        /// </summary>
        public byte[] MAC { get; set; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public MaslAu3Frame()
            : base(MaslFrameType.AU3, MaslFrameDirection.Client2Server, 0)
        {
        }

        public override byte[] GetBytes()
        {
            int index = 0;
            var bytes = new byte[MaslAu3Frame.FrameLength];

            // ETY + MTI + DF
            bytes[index++] = this.GetHeaderByte();

            // MAC
            Array.Copy(this.MAC, 0, bytes, index, 8);
            index += 8;

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            if ((endIndex - startIndex + 1) < MaslAu3Frame.FrameLength)
            {
                throw new Au3LengthException();
            }

            // ETY + MTI + DF
            this.ParseHeaderByte(bytes[startIndex++]);

            // MAC
            this.MAC = new byte[8];
            Array.Copy(bytes, startIndex, this.MAC, 0, 8);
            startIndex += 8;
        }
    }
}
