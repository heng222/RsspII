/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 15:44:40 
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

namespace BJMT.RsspII4net.SAI.TTS.Frames
{
    class SaiTtsFrameEstimate : SaiTtsFrame
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public SaiTtsFrameEstimate()
            : base(SaiFrameType.TTS_OffsetEstimate, 0, 0, 0, 0)
        {
        }

        public SaiTtsFrameEstimate(ushort seqNo,
            uint senderTimestamp, uint receiverLastSendTimestamp, uint senderLastRecvTimestamp, 
            Int64 offsetMin, Int64 offsetMax)
            : base(SaiFrameType.TTS_OffsetEstimate, seqNo, senderTimestamp, receiverLastSendTimestamp, senderLastRecvTimestamp)
        {
            this.OffsetMin = offsetMin;
            this.OffsetMax = offsetMax;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 获取/设置最小时钟偏移值
        /// </summary>
        public Int64 OffsetMin { get; set; }

        /// <summary>
        /// 获取/设置最大时钟偏移值
        /// </summary>
        public Int64 OffsetMax { get; set; }
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"


        public override byte[] GetBytes()
        {
            var bytes = new byte[25];
            int startIndex = 0;

            // 消息类型
            bytes[startIndex++] = (byte)this.FrameType;

            // 序列号
            var tempBuf = RsspEncoding.ToNetUInt16(this.SequenceNo);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 2;

            // sender timestamp
            tempBuf = RsspEncoding.ToNetUInt32(this.SenderTimestamp);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // Responsor last timestamp
            tempBuf = RsspEncoding.ToNetUInt32(this.ReceiverLastSendTimestamp);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // sender last timestamp
            tempBuf = RsspEncoding.ToNetUInt32(this.SenderLastRecvTimestamp);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // 偏移标志
            bytes[startIndex++] = (this.OffsetMin >= 0) ? (byte)0 : (byte)1;

            // |最小偏移值|
            tempBuf = RsspEncoding.ToNetUInt32((uint)Math.Abs(this.OffsetMin));
            Array.Copy(tempBuf, 0, bytes, startIndex, 4);
            startIndex += 4;

            // 偏移标志
            bytes[startIndex++] = (this.OffsetMax >= 0) ? (byte)0 : (byte)1;

            // |最大偏移值|
            tempBuf = RsspEncoding.ToNetUInt32((uint)Math.Abs(this.OffsetMax));
            Array.Copy(tempBuf, 0, bytes, startIndex, 4);
            startIndex += 4;

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

            // sender timestamp
            this.SenderTimestamp = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

            // Responsor last timestamp
            this.ReceiverLastSendTimestamp = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

            // sender last timestamp
            this.SenderLastRecvTimestamp = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

            // 偏移标志
            bool negative = (bytes[startIndex] == 1);
            startIndex++;

            // |最小偏移值|
            this.OffsetMin = RsspEncoding.ToHostUInt32(bytes, startIndex);
            if (negative)
            {
                this.OffsetMin = -this.OffsetMin;
            }
            startIndex += 4;

            // 偏移标志
            negative = (bytes[startIndex] == 1);
            startIndex++;

            // |最大偏移值|
            this.OffsetMax = RsspEncoding.ToHostUInt32(bytes, startIndex);
            if (negative)
            {
                this.OffsetMax = -this.OffsetMax;
            }
            startIndex += 4;
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion
    }
}
