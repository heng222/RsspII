
/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-16 10:32:07 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.SAI.EC.Frames;
using BJMT.RsspII4net.SAI.TTS.Frames;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.SAI
{
    /// <summary>
    /// 消息延迟检测策略类。
    /// </summary>
    abstract class DefenseStrategy : IDisposable
    {
        #region "Filed"
        private bool _disposed = false;
        #endregion

        #region "Constructor"
        protected DefenseStrategy()
        {
        }

        ~DefenseStrategy()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"
        #endregion

        #region "Abstract methods"
        #endregion

        #region "Virtual methods"
        protected virtual long CalcEcTimeDelay(SaiEcFrame ecFrame) { return 0; }

        protected virtual long CalcTtsTimeDelay(SaiTtsFrame ttsFrame) { return 0; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                }
            }
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"

        /// <summary>
        /// 计算指定Sai协议帧的时延。（单位：10毫秒）
        /// </summary>
        /// <param name="saiFrame"></param>
        /// <returns></returns>
        public long CalcTimeDelay(SaiFrame saiFrame)
        {
            if (SaiFrame.IsEcFrame(saiFrame.FrameType))
            {
                return this.CalcEcTimeDelay(saiFrame as SaiEcFrame);
            }
            else if (SaiFrame.IsTtsFrame(saiFrame.FrameType))
            {
                return this.CalcTtsTimeDelay(saiFrame as SaiTtsFrame);
            }
            else
            {
                throw new InvalidOperationException("指定的SaiFrame不可识别，无法计算时延。");
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
