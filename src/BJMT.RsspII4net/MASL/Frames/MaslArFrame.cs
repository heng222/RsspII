/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 10:16:02 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.Exceptions;

namespace BJMT.RsspII4net.MASL.Frames
{
    class MaslArFrame : MaslFrame
    {
        private const int FrameLength = 9;
        
        /// <summary>
        /// 消息验证码。
        /// </summary>
        public byte[] MAC { get; set; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public MaslArFrame()
            : base(MaslFrameType.AR, MaslFrameDirection.Server2Client, 0)
        {
        }

        public override byte[] GetBytes()
        {
            int index = 0;
            var bytes = new byte[MaslArFrame.FrameLength];

            // ETY + MTI + DF
            bytes[index++] = this.GetHeaderByte();

            // MAC
            Array.Copy(this.MAC, 0, bytes, index, 8);
            index += 8;

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            if ((endIndex - startIndex + 1) < MaslArFrame.FrameLength)
            {
                throw new ArLengthException();
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
