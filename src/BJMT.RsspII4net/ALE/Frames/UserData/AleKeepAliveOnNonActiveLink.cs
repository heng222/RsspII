/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 15:04:51 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// 非活动链路上的生命信息
    /// </summary>
    class AleKeepAliveOnNonActiveLink : AleUserData
    {
        public override ushort Length
        {
            get { return 0; }
        }

        public AleKeepAliveOnNonActiveLink()
            : base(AleFrameType.KANA)
        {

        }

        public override byte[] GetBytes()
        {
            return null;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
        }
    }
}
