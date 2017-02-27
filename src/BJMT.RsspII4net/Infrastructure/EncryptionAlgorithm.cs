/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 9:02:49 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net
{
    /// <summary>
    /// 加密算法
    /// </summary>
    public enum EncryptionAlgorithm : byte
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 三重DES。
        /// </summary>
        TripleDES = 1,
    }
}
