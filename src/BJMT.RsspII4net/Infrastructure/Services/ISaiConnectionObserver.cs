/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 15:02:35 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;

namespace BJMT.RsspII4net.Infrastructure.Services
{
    interface ISaiConnectionObserver
    {
        void OnSaiConnected(uint localID, uint remoteID);
        void OnSaiDisconnected(uint localID, uint remoteID);

        void OnSaiUserDataArrival(uint remoteID, byte[] userData, Int64 timeDelay, MessageDelayDefenseTech defenseTech);
    }
}
