/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-17 11:08:52 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.Exceptions;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// ALEPKT信息结构。
    /// 参见《RSSP-II铁路信号安全通信协议》P65。
    /// </summary>
    class AleFrame
    {
        /// <summary>
        /// ALE包首部长度。
        /// </summary>
        public const byte HeadLength = 10;

        /// <summary>
        /// 首部长度占用的字节个数。
        /// </summary>
        public const byte SizeofHeadLen = 2;

        #region "Filed"
        #endregion

        #region "Constructor"
        public AleFrame()
        {
            this.Version = 0x01;
        }

        public AleFrame(byte appType, UInt16 seqNo, bool isNormal, AleUserData userData)
            :this()
        {
            if (userData == null)
            {
                throw new ArgumentNullException();
            }

            this.ApplicationType = appType;
            this.SequenceNo = seqNo;
            this.IsNormal = isNormal;
            this.FrameType = userData.FrameType;
            this.UserData = userData;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 包长度（除去本字段后，整个包的长度，以字节为单位。）
        /// </summary>
        public UInt16 PacketLength { get { return (UInt16)(this.UserDataLength + 8); } }

        /// <summary>
        /// 版本号。
        /// </summary>
        public byte Version { get; set; }

        /// <summary>
        /// 应用类型。
        /// </summary>
        public byte ApplicationType { get; set; }

        /// <summary>
        /// 传输序号。
        /// </summary>
        public UInt16 SequenceNo { get; set; }

        /// <summary>
        /// N/R标志。true表示“正常-正常”连接，false表示“冗余-冗余”连接。
        /// </summary>
        public bool IsNormal { get; set; }

        /// <summary>
        /// 帧类型。
        /// </summary>
        public AleFrameType FrameType { get; set; }

        /// <summary>
        /// 用户数据。
        /// </summary>
        public AleUserData UserData { get; set; }

        /// <summary>
        /// 获取用户数据的长度。
        /// </summary>
        public int UserDataLength 
        {
            get
            {
                if (this.UserData != null)
                {
                    return this.UserData.Length;
                }
                else
                {
                    return 0;
                }
            } 
        }
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"

        /// <summary>
        /// 获取序列化后的字节流。
        /// </summary>
        public byte[] GetBytes()
        {
            var bytes = new byte[AleFrame.HeadLength + this.UserDataLength];
            var startIndex = 0;

            // 包长度
            var tempBuf = RsspEncoding.ToNetUInt16(this.PacketLength);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += AleFrame.SizeofHeadLen;

            // 版本
            bytes[startIndex++] = this.Version;

            // 应用类型
            bytes[startIndex++] = this.ApplicationType;

            // 传输序列号
            tempBuf = RsspEncoding.ToNetUInt16(this.SequenceNo);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 2;

            // N/R标志
            bytes[startIndex++] = (byte)(this.IsNormal ? 1 : 0);

            // 包类型
            bytes[startIndex++] = (byte)this.FrameType;

            // 校验和
            var crcValue = CrcTool.CaculateCCITT16(bytes, 0, 8);
            //tempBuf = BitConverter.GetBytes(crcValue);
            tempBuf = RsspEncoding.ToNetUInt16(crcValue);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 2;

            // 用户数据
            var userData = this.UserData.GetBytes();
            if (userData != null)
            {
                Array.Copy(userData, 0, bytes, startIndex, this.UserDataLength);
            }

            return bytes;
        }

        /// <summary>
        /// 解析指定的字节流。
        /// </summary>
        public void ParseBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length < AleFrame.HeadLength)
            {
                throw new AleFrameParsingException("无法将指定的字节流解析为AleFrame，长度不够。");
            }

            int startIndex = 0;

            // 包长度
            var pktLen = RsspEncoding.ToHostUInt16(bytes, startIndex);
            startIndex += AleFrame.SizeofHeadLen;

            // 版本
            this.Version = bytes[startIndex++];

            // 应用类型
            this.ApplicationType = bytes[startIndex++];

            // 传输序列号
            this.SequenceNo = RsspEncoding.ToHostUInt16(bytes, startIndex);
            startIndex += 2;

            // N/R标志
            this.IsNormal = (bytes[startIndex++] == 1);

            // 包类型
            this.FrameType = (AleFrameType)bytes[startIndex++];

            // 校验和
            if (this.FrameType != AleFrameType.KAA && this.FrameType != AleFrameType.KANA) // 生命信息不需要校验
            {
                //var actualCrcValue = BitConverter.ToUInt16(bytes, startIndex);
                var actualCrcValue = RsspEncoding.ToHostUInt16(bytes, startIndex);
                var expectedCrcValue = CrcTool.CaculateCCITT16(bytes, 0, 8);
                if (actualCrcValue != expectedCrcValue)
                {
                    throw new AleFrameParsingException(string.Format("解析Ale协议帧时发生异常，CRC检验不一致，期望值是{0}，实际值是{1}。",
                        expectedCrcValue, actualCrcValue));
                }
            }
            startIndex += 2;

            // 用户数据
            var userDataLen = pktLen - (AleFrame.HeadLength - AleFrame.SizeofHeadLen);
            if (userDataLen < 0)
            {
                throw new AleFrameParsingException(string.Format("将字节流解析为ALE帧时发生异常，用户数据长度为负值{0}。", userDataLen));
            }
            this.UserData = AleUserData.Parse(this.FrameType, bytes, startIndex, startIndex + userDataLen - 1);
        }

        /// <summary>
        /// 将指定的字节流解析为AleFrame。
        /// </summary>
        public static AleFrame Parse(byte[] bytes)
        {
            var frame = new AleFrame();
            
            frame.ParseBytes(bytes);

            return frame;
        }
        #endregion

    }
}
