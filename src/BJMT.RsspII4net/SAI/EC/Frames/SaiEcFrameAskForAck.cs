/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 15:49:33 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/
using System;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.SAI.EC.Frames
{
    class SaiEcFrameAskForAck : SaiEcFrameApplication
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SaiEcFrameAskForAck()
        {
            this.FrameType = SaiFrameType.EC_AppDataAskForAck;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaiEcFrameAskForAck(ushort seqNo, uint ecValue, byte[] userData)
            :base(seqNo, ecValue, userData)
        {
            this.FrameType = SaiFrameType.EC_AppDataAskForAck;
        }
    }
}
