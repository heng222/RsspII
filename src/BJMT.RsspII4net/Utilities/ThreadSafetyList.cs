/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-24 8:53:10 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;
using System.ComponentModel;
using System.Reflection;

namespace BJMT.RsspII4net.Utilities
{
    /// <summary>
    /// 线程安全的链表类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ThreadSafetyList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        #region __Enumerator1类
        /// <summary>
        /// 重载枚举接口(支持在泛型集合上进行简单迭代),实现在枚举过程中break时释放互斥
        /// </summary>
        internal class __Enumerator1 : IEnumerator<T>, IDisposable//, IEnumerator
        {
            /// <summary>
            /// 当前状态
            /// </summary>
            int __state;
            /// <summary>
            /// 当前枚举到的成员
            /// </summary>
            T __current;
            /// <summary>
            /// ThreadSafetyList类实例
            /// </summary>
            ThreadSafetyList<T> __this;
            /// <summary>
            /// 枚举的索引
            /// </summary>
            int i;            

            /// <summary>
            /// Track whether Dispose has been called.
            /// </summary>
            private bool disposed = false;

            #region __Enumerator1
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="__this">ThreadSafetyList类对象</param>
            internal __Enumerator1(ThreadSafetyList<T> __this)
            {
                this.__this = __this;
            }
            #endregion
            
            
            #region 析构函数
            /// <summary>
            /// 析构函数
            /// </summary>
            ~__Enumerator1()
            {
                Dispose(false);
            }
            #endregion

            #region Current属性
            /// <summary>
            /// 获取集合中位于枚举数当前位置的元素
            /// </summary>
            public T Current
            {
                get { return __current; }
            }
            #endregion

            #region IEnumerator.Current属性
            /// <summary>
            /// 获取集合中的当前元素
            /// </summary>
            object IEnumerator.Current
            {
                get { return __current; }
            }
            #endregion

            #region MoveNext方法
            /// <summary>
            /// 将枚举数推进到集合的下一个元素
            /// </summary>
            /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false</returns>
            public bool MoveNext()
            {
                switch (__state)
                {
                    case 1: goto __state1;
                    case 2: goto __state2;
                }
                i = 0;
            __loop:
                if (i >= __this.Count) goto __state2;
                __current = __this[i];
                __state = 1;
                return true;
            __state1:
                ++i;
                goto __loop;
            __state2:
                __state = 2;
                return false;
            }
            #endregion

            #region Dispose方法
            /// <summary>
            /// 释放使用资源
            /// </summary>
            public void Dispose()
            {
                //__state = 2;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            #endregion

            #region 重载Dispose
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {                   
                    // If disposing equals true, dispose all managed 
                    // and unmanaged resources.
                    if (disposing)
                    {
                        // Dispose managed resources.                    
                    }
                    // Release unmanaged resources. If disposing is false, 
                    // only the following code is executed.
                    //CloseHandle(handle);
                }
                disposed = true;
            }
            #endregion

            #region Reset方法
            /// <summary>
            /// 将枚举数设置为其初始位置，该位置位于集合中第一个元素之前
            /// </summary>
            public void Reset()
            {
                __state = 0;
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 链表类对象
        /// </summary>
        private List<T> _list;
        /// <summary>
        /// 用于控制链表操作排它锁的对象
        /// </summary>
        private object _syncRoot = new object();                   

        #region 构造函数
        /// <summary>
        /// 初始化.ThreadSafetyList类的新实例，该实例为空并且具有默认初始容量.
        /// </summary>
        public ThreadSafetyList()
        {
            _list = new List<T>();            
        }

        /// <summary>
        /// 初始化ThreadSafetyList类的新实例，该实例包含从指定集合复制的元素并且具有足够的容量来容纳所复制的元素。
        /// </summary>
        /// <param name="collection">一个集合，其元素被复制到新列表中</param>
        public ThreadSafetyList(IEnumerable<T> collection)
        {
            _list = new List<T>(collection);
        }

        /// <summary>
        /// 初始化ThreadSafetyList类的新实例，该实例为空并且具有指定的初始容量。
        /// </summary>
        /// <param name="capacity">新列表最初可以存储的元素数</param>
        public ThreadSafetyList(int capacity)
        {
            _list = new List<T>(capacity);
        }
        #endregion

        #region Capacity
        /// <summary>
        /// 获取或设置该内部数据结构在不调整大小的情况下能够保存的元素总数
        /// </summary>
        public int Capacity
        {
            get { return _list.Capacity; }
            set { _list.Capacity = value; }
        }
        #endregion

        #region Count
        /// <summary>
        /// 获取ThreadSafetyList中实际包含的元素数
        /// </summary>
        public int Count
        {
            get
            {
                lock (this.SyncRoot)
                {
                    return _list.Count;
                }
            }
        }
        #endregion

        #region this
        /// <summary>
        /// 获取或设置指定索引处的元素。
        /// </summary>
        /// <param name="index"> 要获得或设置的元素从零开始的索引。</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                lock (this.SyncRoot) { return _list[index]; }
            }

            set
            {
                lock (this.SyncRoot) { _list[index] = value; }
            }
        }
        #endregion
        
        #region Add(T item) 
        /// <summary>
        /// 将对象添加到 ThreadSafetyList的结尾处。
        /// </summary>
        /// <param name="item">要添加到末尾处的对象。对于引用类型，该值可以为null</param>
        public void Add(T item)
        {
            lock(this.SyncRoot)
            {
                _list.Add(item);
            }
        }
        #endregion

        #region AddRange(IEnumerable<T> collection) 
        /// <summary>
        /// 将指定集合的元素添加到 ThreadSafetyList的末尾。
        /// </summary>
        /// <param name="collection"> 一个集合，其元素应被添加到ThreadSafetyList 的末尾。集合自身不能为null，但它可以包含为null的元素（如果类型 T 为引用类型）</param>
        public void AddRange(IEnumerable<T> collection)
        {
            lock (this.SyncRoot)
            {
                _list.AddRange(collection);
            }
        }
        #endregion

        #region AsReadOnly() 
        /// <summary>
        /// 回当前集合的只读ThreadSafetyList包装
        /// </summary>
        /// <returns>作为当前 ThreadSafetyList周围的只读包装的 System.Collections.Generic.ReadOnlyCollection</returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            lock (this.SyncRoot)
            {
                return _list.AsReadOnly();
            }
        }
        #endregion

        #region BinarySearch
        /// <summary>
        /// 使用默认的比较器在整个已排序的ThreadSafetyList中搜索元素，并返回该元素从零开始的索引。
        /// </summary>
        /// <param name="item">要定位的对象。对于引用类型，该值可以为null</param>
        /// <returns>如果找到 item，则为已排序的ThreadSafetyList中 item 的从零开始的索引；否则为一个负数，该负数是大于
        ///     item 的第一个元素的索引的按位求补。如果没有更大的元素，则为ThreadSafetyList.Count的按位求补。</returns> 
        public int BinarySearch(T item)
        {
            lock (this.SyncRoot)
            {
                return _list.BinarySearch(item);
            }
        }

        /// <summary>
        /// 使用指定的比较器在整个已排序的ThreadSafetyList中搜索元素，并返回该元素从零开始的索引
        /// </summary>
        /// <param name="item">要定位的对象。对于引用类型，该值可以为null</param>
        /// <param name="comparer">比较元素时要使用的 System.Collections.Generic.IComparer实现。- 或 - 为null 以使用默认比较器
        /// ystem.Collections.Generic.Comparer.Default</param>
        /// <returns>如果找到 item，则为已排序的ThreadSafetyList中 item 的从零开始的索引；否则为一个负数，该负数是大于
        ///     item 的第一个元素的索引的按位求补。如果没有更大的元素，则为ThreadSafetyList.Count的按位求补。</returns>
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            lock (this.SyncRoot)
            {
                return _list.BinarySearch(item, comparer);
            }
        }

        /// <summary>
        /// 使用指定的比较器在已排序ThreadSafetyList 的某个元素范围中搜索元素，并返回该元素从零开始的索引
        /// </summary>
        /// <param name="index">要搜索的范围从零开始的起始索引</param>
        /// <param name="count">要搜索的范围的长度</param>
        /// <param name="item">要定位的对象。对于引用类型，该值可以为null</param>
        /// <param name="comparer">比较元素时要使用的 System.Collections.Generic.IComparer实现。- 或 - 为null 以使用默认比较器
        /// ystem.Collections.Generic.Comparer.Default</param>
        /// <returns>如果找到 item，则为已排序的ThreadSafetyList中 item 的从零开始的索引；否则为一个负数，该负数是大于
        ///     item 的第一个元素的索引的按位求补。如果没有更大的元素，则为ThreadSafetyList.Count的按位求补。</returns>
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            lock (this.SyncRoot)
            {
                return _list.BinarySearch(index, count, item, comparer);
            }
        }
        #endregion

        #region Clear() 
        /// <summary>
        /// 从ThreadSafetyList中移除所有元素
        /// </summary>
        public void Clear()
        {
            lock (this.SyncRoot)
            {
                _list.Clear();
            }
        }
        #endregion
        
        #region Contains(T item)
        /// <summary>
        /// 确定某元素是否在ThreadSafetyList中
        /// </summary>
        /// <param name="item">要在ThreadSafetyList中定位的对象。对于引用类型，该值可以为null</param>
        /// <returns>如果找到 item，则为 true，否则为 false</returns>
        public bool Contains(T item)
        {
            lock(this.SyncRoot)
            {
                return _list.Contains(item);
            }
        }
        #endregion
                
        #region CopyTo
        /// <summary>
        /// 将整个ThreadSafetyList复制到兼容的一维数组中，从目标数组的开头开始放置
        /// </summary>
        /// <param name="array">作为从ThreadSafetyList复制的元素的目标位置的一维 System.Array。System.Array 必须具有从零开始的索引</param>
        public void CopyTo(T[] array)
        {
            lock (this.SyncRoot)
            {
                _list.CopyTo(array);
            }
        }

        /// <summary>
        /// 将整个ThreadSafetyList复制到兼容的一维数组中，从目标数组的指定索引位置开始放置
        /// </summary>
        /// <param name="array">作为从ThreadSafetyList复制的元素的目标位置的一维 System.Array。System.Array 必须具有从零开始的索引</param>
        /// <param name="arrayIndex">array 中从零开始的索引，在此处开始复制</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this.SyncRoot)
            {
                _list.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// 将一定范围的元素从ThreadSafetyList复制到兼容的一维数组中，从目标数组的指定索引位置开始放置
        /// </summary>
        /// <param name="index">源 ThreadSafetyList中复制开始位置的从零开始的索引</param>
        /// <param name="array">作为从ThreadSafetyList复制的元素的目标位置的一维 System.Array。System.Array 必须具有从零开始的索引</param>
        /// <param name="arrayIndex">array 中从零开始的索引，在此处开始复制</param>
        /// <param name="count">要复制的元素数</param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            lock (this.SyncRoot)
            {
                _list.CopyTo(index, array, arrayIndex, count);
            }
        }
        #endregion

        #region IndexOf(T item) 
        /// <summary>
        /// 搜索指定的对象，并返回整个ThreadSafetyList中第一个匹配项的从零开始的索引。
        /// </summary>
        /// <param name="item">要在ThreadSafetyList中定位的对象。对于引用类型，该值可以为null。</param>
        /// <returns>如果在整个ThreadSafetyList中找到 item 的第一个匹配项，则为该项的从零开始的索引；否则为-1</returns>
        public int IndexOf(T item)
        {
            lock (this.SyncRoot)
            {
                return _list.IndexOf(item);
            }
        }
        #endregion
        
        #region Insert(int index, T item) 
        /// <summary>
        /// 将元素插入ThreadSafetyList 的指定索引处
        /// </summary>
        /// <param name="index">从零开始的索引，应在该位置插入 item</param>
        /// <param name="item">要插入的对象。对于引用类型，该值可以为null</param>
        public void Insert(int index, T item)
        {
            lock (this.SyncRoot)
            {
                _list.Insert(index, item);
            }
        }
        #endregion
        
        #region Remove(T item) 
        /// <summary>
        ///  从ThreadSafetyList中移除特定对象的第一个匹配项
        /// </summary>
        /// <param name="item">要从ThreadSafetyList中移除的对象。对于引用类型，该值可以为null</param>
        /// <returns>如果成功移除 item，则为 true；否则为 false。如果没有找到item，该方法也会返回 false</returns>
        public bool Remove(T item)
        {
            lock (this.SyncRoot)
            {
                return _list.Remove(item);
            }
        }
        #endregion
                
        #region RemoveAt(int index) 
        /// <summary>
        /// 移除ThreadSafetyList的指定索引处的元素
        /// </summary>
        /// <param name="index"> 要移除的元素的从零开始的索引</param>
        public void RemoveAt(int index)
        {
            lock (this.SyncRoot)
            {
                _list.RemoveAt(index);
            }
        }
        #endregion
        
        #region RemoveRange(int index, int count) 
        /// <summary>
        /// 从ThreadSafetyList中移除一定范围的元素
        /// </summary>
        /// <param name="index">要移除的元素的范围从零开始的起始索引</param>
        /// <param name="count">要移除的元素数</param>
        public void RemoveRange(int index, int count)
        {
            lock (this.SyncRoot)
            {
                _list.RemoveRange(index, count);
            }
        }
        #endregion
                
        #region ToArray() 
        /// <summary>
        /// 将ThreadSafetyList的元素复制到新数组中
        /// </summary>
        /// <returns>一个数组，它包含ThreadSafetyList的元素的副本</returns>
        public T[] ToArray()
        {
            lock (this.SyncRoot)
            {
                return _list.ToArray();
            }
        }
        #endregion
                
        #region GetEnumerator()
        /// <summary>
        ///  返回循环访问ThreadSafetyList 的枚举数
        /// </summary>
        /// <returns>返回循环访问ThreadSafetyList 的枚举数</returns>
        public IEnumerator<T> GetEnumerator()
        {
            lock (this.SyncRoot)
            {
                using (__Enumerator1 e = new __Enumerator1(this))
                {
                    while (e.MoveNext())
                    {
                        yield return e.Current;
                    }
                }
            }
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举数
        /// </summary>
        /// <returns>可用于循环访问集合的 System.Collections.IEnumerator 对象</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion


        #region AddQueue
        /// <summary>
        /// 将与链表元素相同的队列中的元素,依次添加到互斥链表
        /// </summary>
        /// <param name="q">与链表元素相同的队列</param>
        /// <returns>成功为true;否则为false</returns>
        public bool AddQueue(Queue q)
        {
            if (q == null)
            {
                throw new ArgumentNullException("插入的链表对象为空");
            }

            if (q.Count == 0)
            {
                return true;
            }

            lock (this.SyncRoot)
            {
                Type qType = q.Peek().GetType();
                Type lType = typeof(T);
                if (qType != lType)
                {
                    throw new Exception(string.Format("类型不一致! ThreadSafetyList类型为{0};Queue类型为{1}", lType, qType));
                }

                foreach (T t in q)
                {
                    _list.Add(t);
                }

                return true;
            }
        }
        #endregion

        #region Dequeue 
        /// <summary>
        /// 移除并返回位于ThreadSafetyList开始处的对象
        /// </summary>
        /// <returns>从ThreadSafetyList的开头移除的对象。</returns>
        public T Dequeue()
        {
            lock (this.SyncRoot)
            {
                T t = _list[0];
                _list.RemoveAt(0);
                return t;
            }
        }
        #endregion

        #region Enqueue 
        /// <summary>
        /// 将对象添加到ThreadSafetyList 的结尾处。
        /// </summary>
        /// <param name="item">要添加到ThreadSafetyList的对象</param>
        public void Enqueue(T item)
        {
            lock (this.SyncRoot)
            {
                this.Add(item);
            }
        }
        #endregion
        
        #region IList 成员
        /// <summary>
        /// 将某项添加到 System.Collections.IList 中。
        /// </summary>
        /// <param name="value">要添加到 System.Collections.IList 的 System.Object。</param>
        /// <returns>新元素的插入位置</returns>
        int IList.Add(object value)
        {
            lock(this.SyncRoot)
            {
                return ((IList)_list).Add(value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IList.Contains(object value)
        {
            lock (this.SyncRoot)
            {
                return ((IList)_list).Contains(value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int IList.IndexOf(object value)
        {
            lock (this.SyncRoot)
            {
                return ((IList)_list).IndexOf(value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        void IList.Insert(int index, object value)
        {
            lock (this.SyncRoot)
            {
                ((IList)_list).Insert(index, value);
            }
        }
        /// <summary>
        /// 获取一个值，该值指示 System.Collections.IList 是否具有固定大小。
        /// 如果 System.Collections.IList 具有固定大小，则为 true；否则为 false。
        /// </summary>
        public bool IsFixedSize
        {
            get { return false; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void IList.Remove(object value)
        {
            lock (this.SyncRoot)
            {
                ((IList)_list).Remove(value);
            }
        }

        object IList.this[int index]
        {
            get
            {
                lock (this.SyncRoot)
                {
                    return ((IList)_list)[index];
                }
            }
            set
            {
                lock (this.SyncRoot)
                {
                    ((IList)_list)[index] = value;
                }
            }
        }

        #endregion

        #region ICollection 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        void ICollection.CopyTo(Array array, int index)
        {
            lock (this.SyncRoot)
            {
                ((IList)_list).CopyTo(array, index);
            }
        }
        /// <summary>
        /// 获取一个值，该值指示是否同步对 System.Collections.ICollection 的访问（线程安全）。
        /// </summary>
        public bool IsSynchronized
        {
            get { return true; }
        }
        /// <summary>
        /// 获取可用于同步 System.Collections.ICollection 访问的对象。
        /// </summary>
        public object SyncRoot
        {
            get { return _syncRoot; }
        }

        #endregion

        #region ICollection<T> 成员

        /// <summary>
        /// 获取一个值，该值指示本集合是否为只读。
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion
    }
}

