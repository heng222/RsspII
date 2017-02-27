/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 10:14:00 
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
    class MaslAu2Frame : MaslFrame
    {
        private const int FrameLength = 21;

        /// <summary>
        /// 消息应答方ID的后三个字节。
        /// </summary>
        public uint ServerID { get; set; }

        /// <summary>
        /// 使用的加密算法/安全特征。
        /// </summary>
        public EncryptionAlgorithm EncryAlgorithm { get; set; }

        /// <summary>
        /// 随机数。
        /// </summary>
        public byte[] RandomA { get; private set; }

        /// <summary>
        /// 消息验证码。
        /// </summary>
        public byte[] MAC { get; set; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public MaslAu2Frame()
        {
            this.RandomA = new byte[8];
            this.MAC = new byte[8];
        }

        public MaslAu2Frame(byte initiatorType, uint clientID, EncryptionAlgorithm encryAlgorithm, byte[] randomA)
            :base(MaslFrameType.AU2, MaslFrameDirection.Server2Client, initiatorType)
        {
            if (randomA == null || randomA.Length != 8)
            {
                throw new ArgumentException("RandomA的长度必须为8字节。");
            }

            this.ServerID = clientID;
            this.EncryAlgorithm = encryAlgorithm;
            this.RandomA = randomA;
        }

        public override byte[] GetBytes()
        {
            int index = 0;
            var bytes = new byte[MaslAu2Frame.FrameLength];

            // ETY + MTI + DF
            bytes[index++] = this.GetHeaderByte();

            // 消息发起方ID
            var tempBuf = RsspEncoding.ToNetUInt32(this.ServerID);
            Array.Copy(tempBuf, 1, bytes, index, 3);
            index += 3;

            // 安全特征
            bytes[index++] = (byte)this.EncryAlgorithm;

            // 随机数
            Array.Copy(this.RandomA, 0, bytes, index, 8);
            index += 8;

            // MAC
            Array.Copy(this.MAC, 0, bytes, index, 8);
            index += 8;

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            if ((endIndex - startIndex + 1) < MaslAu2Frame.FrameLength)
            {
                throw new Au2LengthException();
            }

            var orgStartIndex = startIndex;

            // ETY + MTI + DF
            this.ParseHeaderByte(bytes[startIndex++]);

            // 消息发起方ID
            this.ServerID = (RsspEncoding.ToHostUInt32(bytes, orgStartIndex) & 0xFFFFFF);
            startIndex += 3;

            // 安全特征
            this.EncryAlgorithm = (EncryptionAlgorithm)bytes[startIndex++];

            // 随机数
            Array.Copy(bytes, startIndex, this.RandomA, 0, 8);
            startIndex += 8;

            // MAC
            Array.Copy(bytes, startIndex, this.MAC, 0, 8);
            startIndex += 8;
        }
    }
}
