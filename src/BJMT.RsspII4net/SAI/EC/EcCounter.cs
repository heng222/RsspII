/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-15 14:15:40 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;

namespace BJMT.RsspII4net.SAI.EC
{
    /// <summary>
    /// Excution cycle counter.
    /// 执行周期计数器。
    /// </summary>
    class EcCounter : IDisposable
    {
        #region "Filed"
        private bool _disposed = false;
        private System.Timers.Timer _timer;
        #endregion

        #region "Constructor"
        public EcCounter(uint id, uint cycle, uint initialValue)
        {
            if (cycle == 0)
            {
                throw new ArgumentException("EC周期不能为零值。");
            }

            this.ID = id;
            this.ExcutionCycle = cycle;
            this.CurrentValue = initialValue;

            _timer = new System.Timers.Timer(cycle);
            _timer.AutoReset = true;
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        ~EcCounter()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"

        /// <summary>
        /// 获取当前计数器的标识。
        /// </summary>
        public uint ID { get; private set; }

        /// <summary>
        /// 获取当前计数值。
        /// </summary>
        public uint CurrentValue { get; private set; }

        /// <summary>
        /// 获取当前的执行周期（毫秒）。
        /// </summary>
        public uint ExcutionCycle { get; private set; }
        #endregion

        #region "Private methods"
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    if (_timer != null)
                    {
                        _timer.Close();
                        _timer = null;
                    }
                }
            }
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                this.CurrentValue++;
            }
            catch (System.Exception)
            {
                this.CurrentValue = 0;
            }
        }
        #endregion

        #region "Public methods"
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void UpdateCurrentValue(uint newValue)
        {
            this.CurrentValue = newValue;
        }
        #endregion

    }
}
