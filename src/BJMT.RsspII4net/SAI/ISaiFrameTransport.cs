/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-27 10:37:32 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.SAI.Events;

namespace BJMT.RsspII4net.SAI
{
    interface ISaiFrameTransport
    {
        uint NextSendSeq();

        void SendSaiFrame(SaiFrame saiFrame);

        event EventHandler<SaiFrameIncomingEventArgs> SaiFrameReceived;
    }
}
