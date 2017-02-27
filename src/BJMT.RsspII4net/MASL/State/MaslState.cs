/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 21:01:31 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System.Diagnostics;
using System.Linq;
using BJMT.RsspII4net.Exceptions;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.MASL.State
{
    abstract class MaslState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        protected MaslState(IMaslStateContext context)
        {
            LogUtility.Info(string.Format("{0}：Masl层新状态= {1}", context.RsspEP.ID, this.GetType().Name));

            this.Context = context;
        }
        #endregion

        #region "Properties"
        protected IMaslStateContext Context { get; private set; }
        #endregion

        #region "Virtual methods"
        public virtual void HandleAu1Frame(MaslAu1Frame frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }


        public virtual void HandleAu2Frame(MaslAu2Frame frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }


        public virtual void HandleAu3Frame(MaslAu3Frame frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }


        public virtual void HandleArFrame(MaslArFrame frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }


        public virtual void HandleDtFrame(MaslDtFrame frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        public virtual void HandleDiFrame(MaslDiFrame frame)
        {
            // DO NOTHING!
        }

        public virtual void SendUserData(byte[] saiPacket)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        public virtual void Disconnect(MaslErrorCode majorReason, byte minorReason)
        {
            var bytes = this.Context.AuMessageBuilder.BuildDiPacket((byte)majorReason, minorReason);

            this.Context.AleConnection.Disconnect(bytes);
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
