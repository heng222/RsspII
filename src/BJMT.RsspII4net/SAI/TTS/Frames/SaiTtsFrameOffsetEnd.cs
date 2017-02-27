using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.SAI.TTS.Frames
{
    class SaiTtsFrameOffsetEnd : SaiTtsFrame
    {
        /// <summary>
        /// 获取/设置一个值，用于表示检查结果是否用效。
        /// </summary>
        public bool Valid { get; set; }
        
        public SaiTtsFrameOffsetEnd()
            : base(SaiFrameType.TTS_OffsetEnd, 0, 0, 0, 0)
        {
        }

        public SaiTtsFrameOffsetEnd(ushort seqNo,
            uint senderTimestamp, uint receiverLastSendTimestamp, uint senderLastRecvTimestamp, 
            bool valid)
            : base(SaiFrameType.TTS_OffsetEnd, seqNo, senderTimestamp, receiverLastSendTimestamp, senderLastRecvTimestamp)
        {
            this.Valid = valid;
        }


        public override byte[] GetBytes()
        {
            var bytes = new byte[16];
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

            // valid
            bytes[startIndex++] = Valid ? (byte)1 : (byte)0;

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

            // valid
            this.Valid = (bytes[startIndex++] == 1);
        }
    }
}
