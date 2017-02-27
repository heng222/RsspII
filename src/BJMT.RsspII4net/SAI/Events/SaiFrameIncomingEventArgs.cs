/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-27 10:39:51 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.SAI.Events
{
    class SaiFrameIncomingEventArgs : EventArgs
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public SaiFrameIncomingEventArgs(SaiFrame frame)
        {
            this.Frame = frame;
        }
        #endregion

        #region "Properties"
        public SaiFrame Frame { get; private set; }
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
