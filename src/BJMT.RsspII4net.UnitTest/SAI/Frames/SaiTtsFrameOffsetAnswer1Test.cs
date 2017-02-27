using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.SAI.EC.Frames;
using NUnit.Framework;
using BJMT.RsspII4net.SAI.TTS.Frames;
using BJMT.RsspII4net.SAI;

namespace BJMT.RsspII4net.UnitTest.SAI.Frames
{
    [TestFixture]
    class SaiTtsFrameOffsetAnswer1Test
    {
        [Test]
        public void Test1()
        {
            var frameInital = new SaiTtsFrameOffsetAnswer1();

            frameInital.SequenceNo = 100;
            frameInital.SenderTimestamp = 1000;
            frameInital.SenderLastRecvTimestamp = 900;
            frameInital.ReceiverLastSendTimestamp = 800;
            frameInital.ResponseCycle = 3000;

            var bytes = frameInital.GetBytes();

            var actual = SaiFrame.Parse(bytes) as SaiTtsFrameOffsetAnswer1;

            Assert.AreEqual(frameInital.FrameType, actual.FrameType);
            Assert.AreEqual(frameInital.SequenceNo, actual.SequenceNo);
            Assert.AreEqual(frameInital.SenderTimestamp, actual.SenderTimestamp);
            Assert.AreEqual(frameInital.SenderLastRecvTimestamp, actual.SenderLastRecvTimestamp);
            Assert.AreEqual(frameInital.ReceiverLastSendTimestamp, actual.ReceiverLastSendTimestamp);
            Assert.AreEqual(frameInital.ResponseCycle, actual.ResponseCycle);
        }
    }
}
