/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 9:51:18 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Exceptions;

namespace BJMT.RsspII4net.MASL.Frames
{
    /// <summary>
    /// MASL frame.
    /// </summary>
    abstract class MaslFrame
    {
        /// <summary>
        /// ETY, Equipment TYpe. 
        /// </summary>
        public byte DeviceType { get; private set; }
        /// <summary>
        /// MTI, Message Type ID.
        /// </summary>
        public MaslFrameType FrameType { get; private set; }
        /// <summary>
        /// DF, Direction Flag.
        /// </summary>
        public MaslFrameDirection Direction { get; private set; }

        protected MaslFrame()
        {

        }

        protected MaslFrame(MaslFrameType frameType, MaslFrameDirection direction, byte deviceType)
        {
            this.FrameType = frameType;
            this.Direction = direction;
            this.DeviceType = deviceType;
        }

        public byte GetHeaderByte()
        {
            return (byte)(((byte)this.DeviceType << 5) + ((byte)this.FrameType << 1) + (byte)this.Direction);
        }

        protected void ParseHeaderByte(byte value)
        {
            this.DeviceType = (byte)((value >> 5) & 0x07);
            this.FrameType = (MaslFrameType)((value >> 1) & 0x0F);
            this.Direction = (MaslFrameDirection)(value & 0x01);
        }

        /// <summary>
        /// 序列化为字节流。
        /// </summary>
        public abstract byte[] GetBytes();

        /// <summary>
        /// 将字节流反序列化。
        /// </summary>
        public abstract void ParseBytes(byte[] bytes, int startIndex, int endIndex);

        /// <summary>
        /// 将字节流反序列化。
        /// </summary>
        public static MaslFrame Parse(byte[] bytes, int startIndex, int endIndex)
        {
            var frameType = (MaslFrameType)((bytes[startIndex] >> 1) & 0x0F);

            MaslFrame result;
            if (frameType == MaslFrameType.AU1)
            {
                result = new MaslAu1Frame();
            }
            else if (frameType == MaslFrameType.AU2)
            {
                result = new MaslAu2Frame();
            }
            else if (frameType == MaslFrameType.AU3)
            {
                result = new MaslAu3Frame();
            }
            else if (frameType == MaslFrameType.AR)
            {
                result = new MaslArFrame();
            }
            else if (frameType == MaslFrameType.DT)
            {
                result = new MaslDtFrame();
            }
            else if (frameType == MaslFrameType.DI)
            {
                result = new MaslDiFrame();
            }
            else
            {
                throw new MaslFrameParsingException(string.Format("不可识别的Masl帧类型（{0}）。", frameType));
            }

            result.ParseBytes(bytes, startIndex, endIndex);

            return result;
        }
    }
}
