/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 15:43:10 
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
    class SaiTtsFrameOffsetAnswer1 : SaiTtsFrame
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public SaiTtsFrameOffsetAnswer1()
            : base(SaiFrameType.TTS_OffsetAnswer1, 0, 0, 0, 0)
        {
        }

        public SaiTtsFrameOffsetAnswer1(ushort seqNo,
            uint senderTimestamp, uint receiverLastSendTimestamp, uint senderLastRecvTimestamp, 
            uint responseCycle)
            : base(SaiFrameType.TTS_OffsetAnswer1, seqNo, senderTimestamp, receiverLastSendTimestamp, senderLastRecvTimestamp)
        {
            this.ResponseCycle = responseCycle;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 获取/设置应答方周期
        /// </summary>
        public UInt32 ResponseCycle { get; set; }
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        public override byte[] GetBytes()
        {
            var bytes = new byte[19];
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

            // 应答方周期
            tempBuf = RsspEncoding.ToNetUInt32(this.ResponseCycle);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
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

            // 应答方周期
            this.ResponseCycle = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion
    }
}
