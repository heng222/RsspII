/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 9:45:26 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using BJMT.RsspII4net.Exceptions;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// ALE层数据流解析器（将字节流解析为ALE协议帧）
    /// </summary>
    class AleStreamParser
    {
        /// <summary>
        /// ALE层数据流的最大长度。
        /// </summary>
        public const ushort AleStreamMaxLength = 16 * 1024;

        #region "Filed"
        /// <summary>
        /// 数据接收缓冲区
        /// </summary>
        private byte[] _recvBuffer = new byte[AleStreamMaxLength]; 
        
        /// <summary>
        /// 接收缓冲区的有效数据
        /// </summary>
        private int _recvBufLen = 0;

        /// <summary>
        /// 指示是否收到起始标志
        /// </summary>
        private bool _startFlagRecved = false;

        /// <summary>
        /// 期望收到的数据长度
        /// </summary>
        private int _expectedLen = 0;
        #endregion

        #region "Constructor"
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"

        private ushort GetPacketLength(byte[] buffer, int startIndex)
        {
            if ((buffer == null) || (buffer.Length - startIndex < 2))
            {
                return 0;
            }

            return RsspEncoding.ToHostUInt16(buffer, startIndex);
        }
        #endregion

        #region "Public methods"
        /// <summary>
        /// 复位接收机制。
        /// </summary>
        public void Reset()
        {
            _recvBuffer.Initialize();
            _recvBufLen = 0;
            _startFlagRecved = false;
            _expectedLen = 0;
        }

        /// <summary>
        /// 从TCP无边界字节流中分析出ALE流。
        /// </summary>
        public List<byte[]> ParseTcpStream(byte[] tcpStream, int length)
        {
            var result = new List<byte[]>();

            try
            {
                for (int i = 0; i < length; i++)
                {
                    var data = tcpStream[i];

                    if (!_startFlagRecved)
                    {
                        var pktLen = this.GetPacketLength(tcpStream, i);
                        if (pktLen >= AleFrame.HeadLength - 2)
                        {
                            this.Reset();

                            // 计算期望的长度。
                            _expectedLen = pktLen + 2;
                            if (_expectedLen > _recvBuffer.Length)
                            {
                                throw new AleFrameParsingException(string.Format("ALE解析器缓冲长度({0})不足，期望的长度是{1}。", 
                                    _recvBuffer.Length, _expectedLen));
                            }

                            _startFlagRecved = true;
                            _recvBuffer[_recvBufLen++] = data;
                        }
                    }
                    else
                    {
                        if (_expectedLen > _recvBufLen)
                        {
                            _recvBuffer[_recvBufLen++] = data;
                        }
                        
                        // 收到的数据长度与期望的值一致
                        if (_expectedLen == _recvBufLen)
                        {
                            // 保存收到的数据
                            var newAleStream = new byte[_recvBufLen];
                            Array.Copy(_recvBuffer, 0, newAleStream, 0, _recvBufLen);
                            result.Add(newAleStream);

                            // 复位。
                            this.Reset();
                        }
                    }
                }
            }
            catch (Exception)
            {
                this.Reset();

                throw;
            }

            return result;
        }
        #endregion

    }
}
