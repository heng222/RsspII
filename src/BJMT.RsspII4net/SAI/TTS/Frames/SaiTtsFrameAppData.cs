/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 16:38:27 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.SAI.TTS.Frames
{
    class SaiTtsFrameAppData : SaiTtsFrame
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public SaiTtsFrameAppData()
            :base(SaiFrameType.TTS_AppData, 0, 0, 0, 0)
        {
        }

        public SaiTtsFrameAppData(ushort seqNo,
            uint senderTimestamp, uint receiverLastSendTimestamp, uint senderLastRecvTimestamp, 
            byte[] userData)
            : base(SaiFrameType.TTS_AppData, seqNo, senderTimestamp, receiverLastSendTimestamp, senderLastRecvTimestamp)
        {
            this.UserData = userData;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 用户数据，为空引用时表示本数据报为时钟偏移修正请求或响应报文。
        /// </summary>
        public byte[] UserData { get; set; }

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

        public override byte[] GetBytes()
        {
            if (this.UserDataLength > SaiFrame.MaxUserDataLength)
            {
                throw new ArgumentException(string.Format("SAI层用户数据长度不能超过{0}。", SaiFrame.MaxUserDataLength));
            }

            var bytes = new byte[15 + this.UserDataLength];
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

            // user data
            if (this.UserData != null)
            {
                Array.Copy(this.UserData, 0, bytes, startIndex, this.UserData.Length);
            }

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

            // user data
            var len = bytes.Length - startIndex;
            if (len > 0)
            {
                this.UserData = new byte[len];
                Array.Copy(bytes, startIndex, this.UserData, 0, len);
            }
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
