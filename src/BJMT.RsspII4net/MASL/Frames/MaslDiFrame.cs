/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 10:16:18 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.MASL.Frames
{
    /// <summary>
    /// Disconnect frame.
    /// </summary>
    class MaslDiFrame : MaslFrame
    {
        /// <summary>
        /// 除用户数据外的固定长度。
        /// </summary>
        private const int FrameLen = 3;

        /// <summary>
        /// 断开主原因。
        /// </summary>
        public byte MajorReason { get; private set; }
        
        /// <summary>
        /// 断开子原因。
        /// </summary>
        public byte MinorReason { get; private set; }
        
        /// <summary>
        /// 构造函数。
        /// </summary>
        public MaslDiFrame()
        {

        }

        public MaslDiFrame(MaslFrameDirection direction, byte majorReason, byte minorReason)
            : base(MaslFrameType.DI, direction, 0)
        {
            this.MajorReason = majorReason;
            this.MinorReason = minorReason;
        }

        public override byte[] GetBytes()
        {
            int index = 0;
            var bytes = new byte[MaslDiFrame.FrameLen];

            // ETY + MTI + DF
            bytes[index++] = this.GetHeaderByte();

            // Major reason.
            bytes[index++] = this.MajorReason;

            // Minor reason.
            bytes[index++] = this.MinorReason;

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            // ETY + MTI + DF
            this.ParseHeaderByte(bytes[startIndex++]);

            // Major reason.
            this.MajorReason = bytes[startIndex++];

            // Minor reason.
            this.MinorReason = bytes[startIndex++];
        }

    }
}
