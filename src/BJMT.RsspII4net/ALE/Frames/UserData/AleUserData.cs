/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 13:27:53 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using BJMT.RsspII4net.Exceptions;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// ALE用户数据基类。
    /// </summary>
    abstract class AleUserData
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public AleUserData(AleFrameType frameType)
        {
            this.FrameType = frameType;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 获取协议帧类型。
        /// </summary>
        public AleFrameType FrameType { get; private set; }

        /// <summary>
        /// 获取数据长度。
        /// </summary>
        public abstract ushort Length { get; }
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
        public abstract void ParseBytes(byte[] bytes, int startIndex, int endIndex);

        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        public static AleUserData Parse(AleFrameType frameType, byte[] bytes, int startIndex, int endIndex)
        {
            AleUserData result = null;

            if (frameType == AleFrameType.ConnectionRequest)
            {
                result = new AleConnectionRequest();
            }
            else if (frameType == AleFrameType.ConnectionConfirm)
            {
                result = new AleConnectionConfirm();
            }
            else if (frameType == AleFrameType.DataTransmission)
            {
                result = new AleDataTransmission();
            }
            else if (frameType == AleFrameType.Disconnect)
            {
                result = new AleDisconnect();
            }
            else if (frameType == AleFrameType.SwitchN2R)
            {
                result = new AleSwitchN2R();
            }
            else if (frameType == AleFrameType.SwitchR2N)
            {
                result = new AleSwitchR2N();
            }
            else if (frameType == AleFrameType.KANA)
            {
                result = new AleKeepAliveOnNonActiveLink();
            }
            else if (frameType == AleFrameType.KAA)
            {
                result = new AleKeepAliveOnActiveLink();
            }
            else
            {
                throw new AleFrameParsingException(string.Format("无法将指定的字节流解析为ALE层的用户数据，类型 = {0}。", frameType));
            }
            
            result.ParseBytes(bytes, startIndex, endIndex);

            return result;
        }
        #endregion

    }
}
