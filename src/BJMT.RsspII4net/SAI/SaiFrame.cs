/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 15:41:31 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.SAI.EC.Frames;
using BJMT.RsspII4net.SAI.TTS.Frames;

namespace BJMT.RsspII4net.SAI
{
    /// <summary>
    /// SAI层协议帧基类定义。
    /// </summary>
    abstract class SaiFrame
    {
        /// <summary>
        /// TTS填充位的长度
        /// </summary>
        public const byte TtsPaddingLength = 12;

        /// <summary>
        /// SAI层用户数据最大长度。
        /// </summary>
        public const ushort MaxUserDataLength = 8 * 1024;

        #region "Filed"
        #endregion

        #region "Constructor"

        protected SaiFrame(SaiFrameType type, ushort seqNo)
        {
            this.FrameType = type;
            this.SequenceNo = seqNo;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 消息类型
        /// </summary>
        public SaiFrameType FrameType { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public UInt16 SequenceNo { get; set; }
        #endregion

        #region "Virtual methods"

        /// <summary>
        /// 序列化为字节流。
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetBytes();

        /// <summary>
        /// 将字节流反序列化。
        /// </summary>
        /// <param name="bytes"></param>
        public abstract void ParseBytes(byte[] bytes);
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        public static bool IsEcFrame(SaiFrameType type)
        {
            return (type == SaiFrameType.EC_Start
                || type == SaiFrameType.EC_AppDataAskForAck
                || type == SaiFrameType.EC_AppDataAcknowlegment
                || type == SaiFrameType.EC_AppData) ;
        }

        public static bool IsTtsFrame(SaiFrameType type)
        {
            return (type == SaiFrameType.TTS_OffsetStart
                || type == SaiFrameType.TTS_OffsetAnswer1
                || type == SaiFrameType.TTS_OffsetAnswer2
                || type == SaiFrameType.TTS_OffsetEstimate
                || type == SaiFrameType.TTS_OffsetEnd
                || type == SaiFrameType.TTS_AppData) ;
        }

        public static SaiFrame Parse(byte[] bytes)
        {
            SaiFrame theFrame = null;

            var theFrameType = (SaiFrameType)bytes[0];

            if (theFrameType == SaiFrameType.TTS_OffsetStart)
            {
                theFrame = new SaiTtsFrameOffsetStart();
            }
            else if (theFrameType == SaiFrameType.TTS_OffsetAnswer1)
            {
                theFrame = new SaiTtsFrameOffsetAnswer1();
            }
            else if (theFrameType == SaiFrameType.TTS_OffsetAnswer2)
            {
                theFrame = new SaiTtsFrameOffsetAnswer2();
            }
            else if (theFrameType == SaiFrameType.TTS_OffsetEstimate)
            {
                theFrame = new SaiTtsFrameEstimate();
            }
            else if (theFrameType == SaiFrameType.TTS_OffsetEnd)
            {
                theFrame = new SaiTtsFrameOffsetEnd();
            }
            else if (theFrameType == SaiFrameType.TTS_AppData)
            {
                theFrame = new SaiTtsFrameAppData();
            }
            else if (theFrameType == SaiFrameType.EC_Start)
            {
                theFrame = new SaiEcFrameStart();
            }
            else if (theFrameType == SaiFrameType.EC_AppData)
            {
                theFrame = new SaiEcFrameApplication();
            }
            else if (theFrameType == SaiFrameType.EC_AppDataAskForAck)
            {
                theFrame = new SaiEcFrameAskForAck();
            }
            else if (theFrameType == SaiFrameType.EC_AppDataAcknowlegment)
            {
                theFrame = new SaiEcFrameAcknowlegment();
            }
            else
            {
                throw new InvalidOperationException(string.Format("无法解析指定的Sai帧，不可识别的类型{0}。", theFrameType));
            }

            theFrame.ParseBytes(bytes);

            return theFrame;
        }
        #endregion

    }
}
