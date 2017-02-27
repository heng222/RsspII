/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 10:13:10 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.Exceptions;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.MASL.Frames
{
    /// <summary>
    /// First Authentication message Frame.
    /// </summary>
    class MaslAu1Frame : MaslFrame
    {
        private const int FrameLength = 13;

        /// <summary>
        /// 发起方ID的后三个字节。
        /// </summary>
        public uint ClientID { get; set; }

        /// <summary>
        /// 使用的加密算法。
        /// </summary>
        public EncryptionAlgorithm EncryAlgorithm { get; set; }

        /// <summary>
        /// 随机数。
        /// </summary>
        public byte[] RandomB { get; private set; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public MaslAu1Frame()
        {
            this.RandomB = new byte[8];
        }

        public MaslAu1Frame(byte initiatorType, uint clientID, EncryptionAlgorithm encryAlgorithm, byte[] randomB)
            :base(MaslFrameType.AU1, MaslFrameDirection.Client2Server, initiatorType)
        {
            if (randomB == null || randomB.Length != 8)
            {
                throw new ArgumentException("RandomB的长度必须为8字节。");
            }

            this.ClientID = clientID;
            this.EncryAlgorithm = encryAlgorithm;
            this.RandomB = randomB;
        }

        public override byte[] GetBytes()
        {
            int index = 0;
            var bytes = new byte[MaslAu1Frame.FrameLength];

            // ETY + MTI + DF
            bytes[index++] = this.GetHeaderByte();

            // 消息发起方ID
            var tempBuf = RsspEncoding.ToNetUInt32(this.ClientID);
            Array.Copy(tempBuf, 1, bytes, index, 3);
            index += 3;

            // 安全特征
            bytes[index++] = (byte)this.EncryAlgorithm;

            // 随机数
            Array.Copy(this.RandomB, 0, bytes, index, 8);

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            if ((endIndex - startIndex + 1) < MaslAu1Frame.FrameLength)
            {
                throw new Au1LengthException();
            }

            var orgStartIndex = startIndex;

            // ETY + MTI + DF
            this.ParseHeaderByte(bytes[startIndex++]);

            // 消息发起方ID
            this.ClientID = (RsspEncoding.ToHostUInt32(bytes, orgStartIndex) & 0xFFFFFF);
            startIndex += 3;

            // 安全特征
            this.EncryAlgorithm = (EncryptionAlgorithm)bytes[startIndex++];

            // 随机数
            Array.Copy(bytes, startIndex, this.RandomB, 0, 8);
        }
    }
}
