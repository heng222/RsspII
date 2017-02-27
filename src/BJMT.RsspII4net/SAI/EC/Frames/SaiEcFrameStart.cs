using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.SAI.EC.Frames
{
    class SaiEcFrameStart : SaiEcFrame
    {
        /// <summary>
        /// 初始值
        /// </summary>
        public UInt32 InitialValue
        {
            get { return this.EcValue; }
            set { this.EcValue = value; }
        }

        /// <summary>
        /// 版本
        /// </summary>
        public UInt32 Version { get; set; }

        /// <summary>
        /// EC周期
        /// </summary>
        public UInt16 Interval { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaiEcFrameStart()
            : base(SaiFrameType.EC_Start, 0)
        {
            this.Version = 1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="seqNo">Seq no.</param>
        /// <param name="initValue">EC初始值</param>
        /// <param name="version">版本</param>
        /// <param name="ecInterval">EC周期值</param>
        public SaiEcFrameStart(ushort seqNo, uint initValue, ushort version, ushort ecInterval)
            : base(SaiFrameType.EC_Start, seqNo)
        {
            this.InitialValue = initValue;
            this.Version = version;
            this.Interval = ecInterval;
        }

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

            // Padding
            startIndex += SaiFrame.TtsPaddingLength;

            // 初始值
            tempBuf = RsspEncoding.ToNetUInt32(this.InitialValue);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // 版本
            tempBuf = RsspEncoding.ToNetUInt32(this.Version);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // EC周期
            tempBuf = RsspEncoding.ToNetUInt16(this.Interval);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);

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

            // 初始值
            this.InitialValue = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

            // 版本
            this.Version = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

            // EC周期
            this.Interval = RsspEncoding.ToHostUInt16(bytes, startIndex);
        }
    }
}
