/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 16:01:58 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.SAI.TTS.Frames
{
    abstract class SaiTtsFrame : SaiFrame
    {
        /// <summary>
        /// 获取/设置发送方当前时间戳
        /// </summary>
        public UInt32 SenderTimestamp { get; set; }

        /// <summary>
        /// 获取/设置接收方上一次的发送时间戳
        /// </summary>
        public UInt32 ReceiverLastSendTimestamp { get; set; }

        /// <summary>
        /// 获取/设置发送方收到上一条消息时的时间戳
        /// </summary>
        public UInt32 SenderLastRecvTimestamp { get; set; }

        protected SaiTtsFrame(SaiFrameType type, ushort seqNo, 
            uint senderTimestamp, uint receiverLastSendTimestamp, uint senderLastRecvTimestamp)
            : base(type, seqNo)
        {
            this.SenderTimestamp = senderTimestamp;
            this.ReceiverLastSendTimestamp = receiverLastSendTimestamp;
            this.SenderLastRecvTimestamp = senderLastRecvTimestamp;
        }
    }
}
