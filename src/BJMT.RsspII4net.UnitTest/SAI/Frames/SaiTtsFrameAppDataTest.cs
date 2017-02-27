using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.SAI.EC.Frames;
using BJMT.RsspII4net.SAI;
using BJMT.RsspII4net.SAI.TTS.Frames;

namespace BJMT.RsspII4net.UnitTest.SAI.Frames
{
    [TestFixture]
    class SaiTtsFrameAppDataTest
    {
        [Test]
        public void Test1()
        {
            var frameInital = new SaiTtsFrameAppData();
            frameInital.SequenceNo = 100;
            frameInital.SenderTimestamp = 1000;
            frameInital.SenderLastRecvTimestamp = 900;
            frameInital.ReceiverLastSendTimestamp = 800;
            frameInital.UserData = new byte[] { 1, 2, 3 };

            var bytes = frameInital.GetBytes();

            var actual = SaiFrame.Parse(bytes) as SaiTtsFrameAppData;

            Assert.AreEqual(frameInital.FrameType, actual.FrameType);
            Assert.AreEqual(frameInital.SequenceNo, actual.SequenceNo);
            Assert.AreEqual(frameInital.SenderTimestamp, actual.SenderTimestamp);
            Assert.AreEqual(frameInital.SenderLastRecvTimestamp, actual.SenderLastRecvTimestamp);
            Assert.AreEqual(frameInital.ReceiverLastSendTimestamp, actual.ReceiverLastSendTimestamp);
            Assert.AreEqual(frameInital.UserData[0], actual.UserData[0]);
            Assert.AreEqual(frameInital.UserData[1], actual.UserData[1]);
            Assert.AreEqual(frameInital.UserData[2], actual.UserData[2]);
        }

        [Test]
        public void Test2()
        {
            var frameInital = new SaiTtsFrameAppData();
            
            Assert.AreEqual(frameInital.UserDataLength, 0);
        }
    }
}
