/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：Microunion Foundation Component Library
//
// 创 建 人：zhh_217
// 创建日期：08/31/2011 08:53:27 
// 邮    箱：zhh_217@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司 2009-2015 保留所有权利
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace BJMT.RsspII4net.Utilities
{
    /// <summary>
    /// 产品创建事件参数类
    /// </summary>
    /// <typeparam name="TProduct"></typeparam>
    class ProductCreatedEventArgs<TProduct> : EventArgs
    {
        #region "Filed"
        private List<TProduct> _products = new List<TProduct>();
        #endregion

        #region "Constructor"
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="product">产品引用</param>
        public ProductCreatedEventArgs(TProduct product)
        {
            this._products.Add(product);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="products">产品集</param>
        public ProductCreatedEventArgs(IList<TProduct> products)
        {
            this._products.AddRange(products);
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 获取产品集合
        /// </summary>
        public IList<TProduct> Products
        {
            get { return _products; }            
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        #endregion
    }

}
