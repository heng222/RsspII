using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// 切换到冗余链路
    /// </summary>
    class AleSwitchN2R : AleUserData
    {
        /// <summary>
        /// 用户数据。
        /// </summary>
        public byte[] UserData { get; set; }

        public override ushort Length
        {
            get
            {
                int value = 0;
                if (this.UserData != null)
                {
                    value += this.UserData.Length;
                }

                return (ushort)value;
            }
        }

        public AleSwitchN2R()
            : base(AleFrameType.SwitchN2R)
        {

        }

        public override byte[] GetBytes()
        {
            var bytes = new byte[this.Length];
            int startIndex = 0;

            // 用户数据
            if (this.UserData != null)
            {
                Array.Copy(this.UserData, 0, bytes, startIndex, this.UserData.Length);
            }

            return bytes;
        }

        public override void ParseBytes(byte[] bytes, int startIndex, int endIndex)
        {
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
