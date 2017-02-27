/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 15:37:56 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.IO;
using BJMT.RsspII4net.Infrastructure.Services;
using BJMT.RsspII4net.MASL.Frames;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.MASL
{
    /// <summary>
    /// Authentication消息产生器。
    /// </summary>
    class AuMessageBuilder : IAuMessageBuilder, IAuMessageMacCaculator
    {
        #region "Fields"

        private RsspEndPoint _rsspEndPoint = null;

        private IMacCalculator _macCalc;
        #endregion

        #region "Construct"
        public AuMessageBuilder(RsspEndPoint rsspEP, IMacCalculator calc)
        {
            _rsspEndPoint = rsspEP;

            _macCalc = calc;
        }
        #endregion

        #region "public methods"
        
        #endregion

        #region "IAuMessageBuilder接口实现"

        public byte[] BuildAu1Packet()
        {
            var frame = new MaslAu1Frame(_rsspEndPoint.LocalEquipType, _rsspEndPoint.LocalID, 
                EncryptionAlgorithm.TripleDES, _macCalc.RandomB);

            return frame.GetBytes();
        }

        public byte[] BuildAu2Packet()
        {
            var frame = new MaslAu2Frame(_rsspEndPoint.LocalEquipType, _rsspEndPoint.LocalID, 
                EncryptionAlgorithm.TripleDES, _macCalc.RandomA);

            frame.MAC = this.CalcAu2MAC(frame, _rsspEndPoint.RemoteID);

            return frame.GetBytes();
        }

        public byte[] BuildAu3Packet()
        {
            var frame = new MaslAu3Frame();

            frame.MAC = this.CalcAu3MAC(frame, _rsspEndPoint.RemoteID);

            return frame.GetBytes();
        }

        public byte[] BuildArPacket()
        {
            var frame = new MaslArFrame();
            frame.MAC = this.CalcArMAC(frame, _rsspEndPoint.RemoteID);

            return frame.GetBytes();
        }

        public byte[] BuildDtPacket(byte[] userData)
        {
            var direction = _rsspEndPoint.IsInitiator ? MaslFrameDirection.Client2Server : MaslFrameDirection.Server2Client;
            
            var frame = new MaslDtFrame(direction, userData);

            frame.MAC = this.CalcDtMAC(frame, _rsspEndPoint.RemoteID);

            return frame.GetBytes();
        }

        public byte[] BuildDiPacket(byte majorReason, byte minorReason)
        {
            var direction = _rsspEndPoint.IsInitiator ? MaslFrameDirection.Client2Server : MaslFrameDirection.Server2Client;
            var frame = new MaslDiFrame(direction, majorReason, minorReason);

            return frame.GetBytes();
        }

        public void CheckAu1Packet(byte[] au1Bytes)
        {
            var au1Frame = MaslFrame.Parse(au1Bytes, 0, au1Bytes.Length - 1) as MaslAu1Frame;

            if (au1Frame == null) throw new Exception("无法将指定的字节流序列化为Au1Frame。");

            if (!ArrayHelper.Equals(au1Frame.RandomB, _macCalc.RandomB))
            {
                throw new Exception(string.Format("Au1消息中的RandomB检验失败，期望值={0}，实际值={1}",
                    HelperTools.ConvertToString(_macCalc.RandomB),
                    HelperTools.ConvertToString(au1Frame.RandomB)));
            }
        }

        #endregion

        #region "IAuMessageMacCaculator methods"

        public byte[] CalcAu2MAC(MaslAu2Frame frame, uint destAddress)
        {
            using (var memStream = new MemoryStream(30))
            {
                // L
                memStream.WriteByte(0x00);
                memStream.WriteByte(0x1B);

                // DA
                memStream.WriteByte((byte)((destAddress >> 16) & 0xFF));
                memStream.WriteByte((byte)((destAddress >> 8) & 0xFF));
                memStream.WriteByte((byte)((destAddress) & 0xFF));

                // ETY+MTI+DF
                memStream.WriteByte(frame.GetHeaderByte());

                // SA 
                memStream.WriteByte((byte)((frame.ServerID >> 16) & 0xFF));
                memStream.WriteByte((byte)((frame.ServerID >> 8) & 0xFF));
                memStream.WriteByte((byte)((frame.ServerID) & 0xFF));

                // SaF
                memStream.WriteByte((byte)frame.EncryAlgorithm);

                // RA + RB
                memStream.Write(_macCalc.RandomA, 0, _macCalc.RandomA.Length);
                memStream.Write(_macCalc.RandomB, 0, _macCalc.RandomB.Length);

                // DA
                memStream.WriteByte((byte)((destAddress >> 16) & 0xFF));
                memStream.WriteByte((byte)((destAddress >> 8) & 0xFF));
                memStream.WriteByte((byte)((destAddress) & 0xFF));

                var mac = _macCalc.CalcMac(memStream.ToArray());

                return mac;
            }
        }

        public byte[] CalcAu3MAC(MaslAu3Frame frame, uint destAddress)
        {
            using (var memStream = new MemoryStream(30))
            {
                // L
                memStream.WriteByte(0x00);
                memStream.WriteByte(0x14);

                // DA
                memStream.WriteByte((byte)((destAddress >> 16) & 0xFF));
                memStream.WriteByte((byte)((destAddress >> 8) & 0xFF));
                memStream.WriteByte((byte)((destAddress) & 0xFF));

                // ETY+MTI+DF
                memStream.WriteByte(frame.GetHeaderByte());

                // RB + RA
                memStream.Write(_macCalc.RandomB, 0, _macCalc.RandomB.Length);
                memStream.Write(_macCalc.RandomA, 0, _macCalc.RandomA.Length);

                var mac = _macCalc.CalcMac(memStream.ToArray());

                return mac;
            }
        }

        public byte[] CalcArMAC(MaslArFrame frame, uint destAddress)
        {
            using (var memStream = new MemoryStream(10))
            {
                // L
                memStream.WriteByte(0);
                memStream.WriteByte(4);

                // DA
                memStream.WriteByte((byte)((destAddress >> 16) & 0xFF));
                memStream.WriteByte((byte)((destAddress >> 8) & 0xFF));
                memStream.WriteByte((byte)((destAddress) & 0xFF));

                // ETY+MTI+DF
                memStream.WriteByte(frame.GetHeaderByte());

                // 
                var mac = _macCalc.CalcMac(memStream.ToArray());

                return mac;
            }
        }

        public byte[] CalcDtMAC(MaslDtFrame frame, uint destAddress)
        {
            var totalLen = frame.UserDataLen + 6;

            using (var memStream = new MemoryStream(totalLen))
            {
                // L
                var len = (ushort)(totalLen - 2);
                memStream.WriteByte((byte)((len >> 16) & 0xFF));
                memStream.WriteByte((byte)(len & 0xFF));

                // DA
                memStream.WriteByte((byte)((destAddress >> 16) & 0xFF));
                memStream.WriteByte((byte)((destAddress >> 8) & 0xFF));
                memStream.WriteByte((byte)((destAddress) & 0xFF));

                // ETY+MTI+DF
                memStream.WriteByte(frame.GetHeaderByte());

                // UserData
                memStream.Write(frame.UserData, 0, frame.UserDataLen);

                // 
                var mac = _macCalc.CalcMac(memStream.ToArray());

                return mac;
            }
        }
        #endregion

    }
}
