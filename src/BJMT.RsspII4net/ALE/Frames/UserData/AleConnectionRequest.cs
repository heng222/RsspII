using System;
using BJMT.RsspII4net;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// ALE层连接请求时使用的数据。
    /// </summary>
    class AleConnectionRequest : AleUserData
    {
        /// <summary>
        /// 主叫方编号。
        /// </summary>
        public UInt32 InitiatorID { get; set; }

        /// <summary>
        /// 被叫方编号。
        /// </summary>
        public UInt32 ResponderID { get; set; }

        /// <summary>
        /// 服务类型。
        /// </summary>
        public ServiceType ServiceType { get; set; }

        /// <summary>
        /// 用户数据，即AU1 SaPDU。
        /// </summary>
        public byte[] UserData { get; set; }

        public override ushort Length
        {
            get 
            {
                int value = 9;
                if (this.UserData != null)
                {
                    value += this.UserData.Length;
                }

                return (ushort)value; 
            }
        }

        public AleConnectionRequest()
            : base(AleFrameType.ConnectionRequest)
        {

        }

        public AleConnectionRequest(uint initiatorID, uint responderID, ServiceType serviceType, byte[] userData)
            : this()
        {
            this.InitiatorID = initiatorID;
            this.ResponderID = responderID;
            this.ServiceType = serviceType;
            this.UserData = userData;
        }

        public override byte[] GetBytes()
        {
            var bytes = new byte[this.Length];
            int startIndex = 0;

            // 主叫编号
            var tempBuf = RsspEncoding.ToNetUInt32(this.InitiatorID);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // 被叫编号
            tempBuf = RsspEncoding.ToNetUInt32(this.ResponderID);
            Array.Copy(tempBuf, 0, bytes, startIndex, tempBuf.Length);
            startIndex += 4;

            // 服务类型
            bytes[startIndex++] = (byte)this.ServiceType;

            // 用户数据
            if (this.UserData != null)
            {
                Array.Copy(this.UserData, 0, bytes, startIndex, this.UserData.Length);
            }

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
            // 主叫编号
            this.InitiatorID = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

            // 被叫编号
            this.ResponderID = RsspEncoding.ToHostUInt32(bytes, startIndex);
            startIndex += 4;

            // 服务类型
            this.ServiceType = (ServiceType)bytes[startIndex++];

            // 用户数据
            var len = endIndex - startIndex + 1;
            if (len > 0)
            {
                this.UserData = new byte[len];
                Array.Copy(bytes, startIndex, this.UserData, 0, len);
            }
        }
    }
}
