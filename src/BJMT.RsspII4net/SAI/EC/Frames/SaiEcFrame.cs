/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-19 15:40:19 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.SAI.EC.Frames
{
    abstract class SaiEcFrame : SaiFrame
    {
        #region "Filed"
        #endregion

        #region "Constructor"

        protected SaiEcFrame(SaiFrameType type, ushort seqNo)
            : base(type, seqNo)
        {
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// EC计数
        /// </summary>
        public UInt32 EcValue { get; set; }
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
