/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 15:01:30 
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
    interface IAleConnectionObserver
    {
        /// <summary>
        /// 当ALE层连接时调用。
        /// </summary>
        void OnAleConnected();

        /// <summary>
        /// 当ALE层断开时调用。
        /// </summary>
        void OnAleDisconnected();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aleUserData"></param>
        void OnAleUserDataArrival(byte[] aleUserData);
    }
}
