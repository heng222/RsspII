/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-21 13:25:51 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.MASL
{
    interface IMacCalculator
    {
        byte[] RandomA { get; set; }
        byte[] RandomB { get; set; }

        void UpdateSessionKeys();

        byte[] CalcMac(byte[] userData);
    }
}
