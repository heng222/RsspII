/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-14 9:09:44 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using BJMT.RsspII4net.MASL;
using BJMT.RsspII4net.SAI.EC;
using BJMT.RsspII4net.Utilities;
using BJMT.RsspII4net.Infrastructure.Services;

namespace BJMT.RsspII4net.SAI
{
    interface ISaiStateContext
    {
        SaiState CurrentState { get; set; }

        bool Connected { get; set; }

        RsspEndPoint RsspEP { get; }

        SeqNoManager SeqNoManager { get; }

        MaslConnection NextLayer { get; }

        DefenseStrategy DefenseStrategy { get; set; }

        ISaiFrameTransport FrameTransport { get; }

        ISaiConnectionObserver Observer { get; }
        
        void StartHandshakeTimer();
        void StopHandshakeTimer();
    }
}
