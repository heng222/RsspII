/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 19:05:21 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.Utilities
{
    static class ArrayHelper
    {
        public static bool Equals(byte[] value1, byte[] value2)
        {
            if (value1 == null && value2 != null)
            {
                return false;
            }

            if (value2 == null && value1 != null)
            {
                return false;
            }

            if (value1.Equals(value2))
            {
                return true;
            }

            if (value1.Length != value2.Length)
            {
                return false;
            }

            for (int i = 0; i < value1.Length; i++)
            {
                if (value1[i] != value2[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
