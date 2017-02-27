using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.Infrastructure.Services
{
    /// <summary>
    /// 一个接口，用于生成AU消息。
    /// </summary>
    interface IAuMessageBuilder
    {
        byte[] BuildAu1Packet();
        byte[] BuildAu2Packet();
        byte[] BuildAu3Packet();
        byte[] BuildArPacket();
        byte[] BuildDtPacket(byte[] userData);
        byte[] BuildDiPacket(byte majorReason, byte minorReason);

        void CheckAu1Packet(byte[] au1Bytes);
    }

    /// <summary>
    /// 一个接口，用于计算AU消息的MAC值。
    /// </summary>
    interface IAuMessageMacCaculator
    {
        byte[] CalcAu2MAC(MaslAu2Frame frame, uint destAddress);
        byte[] CalcAu3MAC(MaslAu3Frame frame, uint destAddress);
        byte[] CalcArMAC(MaslArFrame frame, uint destAddress);
        byte[] CalcDtMAC(MaslDtFrame frame, uint destAddress);
    }
}
