/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 15:01:52 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.Infrastructure.Services
{
    interface IMaslConnectionObserver
    {
        /// <summary>
        /// 通知Masl层连接建立。
        /// </summary>
        void OnMaslConnected();

        void OnMaslDisconnected();

        void OnMaslUserDataArrival(byte[] maslUserData);
    }
}
