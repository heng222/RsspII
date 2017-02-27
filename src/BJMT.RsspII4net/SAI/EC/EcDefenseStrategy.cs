/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-15 14:36:18 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System.Text;
using BJMT.RsspII4net.SAI.EC.Frames;

namespace BJMT.RsspII4net.SAI.EC
{
    /// <summary>
    /// EC防御策略。
    /// </summary>
    class EcDefenseStrategy : DefenseStrategy
    {
        #region "Filed"
        private bool _disposed = false;

        /// <summary>
        /// 本地计数器。
        /// </summary>
        private EcCounter _localCounter;

        /// <summary>
        /// 远程计数器。
        /// </summary>
        private EcCounter _remoteCounter;

        /// <summary>
        /// Delta值处于状态3的次数。
        /// </summary>
        private byte _state3Count;
        #endregion

        #region "Constructor"
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localID">本地编号</param>
        /// <param name="localCycle">EC周期</param>
        public EcDefenseStrategy(uint localID, uint localCycle)
        {
            _localCounter = new EcCounter(localID, localCycle, 0);
        }
        #endregion

        #region "Properties"
        
        #endregion

        #region "Override methods"

        protected override long CalcEcTimeDelay(SaiEcFrame ecFrame)
        {
            var actualRemoteEcValue = ecFrame.EcValue;
            var delta = (long)_remoteCounter.CurrentValue - (long)actualRemoteEcValue;

            if (delta > 3)
            {
                _state3Count++;
            }

            // 如果Delta小于0或者连接5个周期Delta差值在3以上，则执行修正程序。
            if (delta < 0)
            {
                return 0;
            }
            else if (_state3Count > 5)
            {
                _remoteCounter.UpdateCurrentValue(actualRemoteEcValue);
                _state3Count = 0;
                return 0;
            }
            else
            {
                return (delta * _remoteCounter.ExcutionCycle) / 10;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    if (_localCounter != null)
                    {
                        _localCounter.Dispose();
                        _localCounter = null;
                    }

                    if (_remoteCounter != null)
                    {
                        _remoteCounter.Dispose();
                        _remoteCounter = null;
                    }
                }

                base.Dispose(disposing);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(200);

            sb.AppendFormat("EC防御，");

            if (_localCounter != null)
            {
                sb.AppendFormat("本地EC周期={0}，当前EC值={1}。", _localCounter.ExcutionCycle, _localCounter.CurrentValue);
            }

            if (_remoteCounter != null)
            {
                sb.AppendFormat("远程EC周期= {0}，期望的EC值= {1}。\r\n", _remoteCounter.ExcutionCycle, _remoteCounter.CurrentValue);
            }

            return sb.ToString();
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"

        public void StartRemoteCounter(uint remoteID, uint interval, uint initialValue)
        {
            _remoteCounter = new EcCounter(remoteID, interval, initialValue);
        }

        /// <summary>
        /// 获取本地EC的当前值。
        /// </summary>
        /// <returns></returns>
        public uint GetLocalEcValue()
        {
            return _localCounter.CurrentValue;
        }
        #endregion

    }
}
