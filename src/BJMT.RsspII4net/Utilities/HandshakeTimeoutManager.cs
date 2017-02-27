/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-22 18:06:03 
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
    interface IHandshakeTimeoutObserver
    {
        void OnHandshakeTimeout();
    }

    /// <summary>
    /// 握手超时管理器。
    /// </summary>
    class HandshakeTimeoutManager : IDisposable
    {
        #region "Filed"
        private bool _disposed = false;
        /// <summary>
        /// 一个计时器，用于检测握手是否超时。
        /// </summary>
        private System.Timers.Timer _handshakeTimer;

        private IHandshakeTimeoutObserver _observer;
        #endregion

        #region "Constructor"
        public HandshakeTimeoutManager(int timeout, IHandshakeTimeoutObserver observer)
        {
            _observer = observer;

            _handshakeTimer = new System.Timers.Timer(timeout);
            _handshakeTimer.AutoReset = false;
            _handshakeTimer.Elapsed += OnTimerElapsed;
        }

        ~HandshakeTimeoutManager()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    CloseTimer();
                }
            }
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        private void CloseTimer()
        {
            if (_handshakeTimer != null)
            {
                _handshakeTimer.Close();
                _handshakeTimer = null;
            }
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                _observer.OnHandshakeTimeout();
            }
            catch (System.Exception /*ex*/)
            {
            }
        }
        #endregion

        #region "Public methods"
        public void Start()
        {
            if (_handshakeTimer != null)
            {
                _handshakeTimer.Start();
            }
        }

        public void Stop()
        {
            if (_handshakeTimer != null)
            {
                _handshakeTimer.Stop();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
