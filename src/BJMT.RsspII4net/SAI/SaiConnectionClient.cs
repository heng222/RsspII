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
using BJMT.RsspII4net.Config;
using BJMT.RsspII4net.Infrastructure.Services;
using BJMT.RsspII4net.SAI.EC;
using BJMT.RsspII4net.SAI.EC.State;
using BJMT.RsspII4net.SAI.TTS;
using BJMT.RsspII4net.SAI.TTS.State;

namespace BJMT.RsspII4net.SAI
{
    class SaiConnectionClient : SaiConnection
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        /// <summary>
        /// 创建一个适用于主动方的SAI Connection。
        /// </summary>
        public SaiConnectionClient(RsspEndPoint rsspEP, IEnumerable<RsspTcpLinkConfig> linkConfig,
            ISaiConnectionObserver observer,
            IAleTunnelEventNotifier tunnelEventNotifier)
            : base(rsspEP, linkConfig, observer, tunnelEventNotifier)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override SaiState GetInitialState(DefenseStrategy strategy)
        {
            if (this.RsspEP.DefenseTech == MessageDelayDefenseTech.EC)
            {
                var ecStrategy = strategy as EcDefenseStrategy;
                if (ecStrategy == null)
                {
                    throw new InvalidCastException("指定的策略无法转换为EcDefenseStrategy。");
                }

                return new EcDisconnectedState(this, ecStrategy);
            }
            else if (this.RsspEP.DefenseTech == MessageDelayDefenseTech.TTS)
            {
                var ttsStrategy = strategy as TtsDefenseStrategy;
                if (ttsStrategy == null)
                {
                    throw new InvalidCastException("指定的策略无法转换为TtsDefenseStrategy。");
                }
                
                return new TtsDisconnectedState(this, ttsStrategy);
            }
            else
            {
                throw new InvalidOperationException("主动方必须指定一个有效的消息延迟防御技术。");
            }
        }

        protected override DefenseStrategy GetDefenseStrategy()
        {
            if (this.RsspEP.DefenseTech == MessageDelayDefenseTech.EC)
            {
                return new EcDefenseStrategy(this.RsspEP.LocalID, this.RsspEP.EcInterval);
            }
            else if (this.RsspEP.DefenseTech == MessageDelayDefenseTech.TTS)
            {
                return new TtsDefenseStrategy(this, true);
            }
            else
            {
                throw new InvalidOperationException("指定的消息延迟防御技术无效。");
            }
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
