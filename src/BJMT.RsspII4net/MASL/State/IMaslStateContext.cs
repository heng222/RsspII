/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-13 8:41:06 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.ALE;
using BJMT.RsspII4net.Infrastructure.Services;

namespace BJMT.RsspII4net.MASL.State
{
    interface IMaslStateContext
    {
        MaslState CurrentState { get; set; }

        bool Connected { get; set; }
        IMaslConnectionObserver Observer { get; }

        IMacCalculator MacCalculator { get; }

        IAuMessageBuilder AuMessageBuilder { get; }
        IAuMessageMacCaculator AuMessageMacCalculator { get; }

        RsspEndPoint RsspEP { get; }
        AleConnection AleConnection { get; }

        void StartHandshakeTimer();
        void StopHandshakeTimer();
    }
}
