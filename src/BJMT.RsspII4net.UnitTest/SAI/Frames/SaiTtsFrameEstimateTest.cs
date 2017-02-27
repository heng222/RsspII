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
    class SaiTtsFrameEstimateTest
    {
        [Test]
        public void Test1()
        {
            var frameInital = new SaiTtsFrameEstimate();

            frameInital.SequenceNo = 100;
            frameInital.SenderTimestamp = 1000;
            frameInital.SenderLastRecvTimestamp = 900;
            frameInital.ReceiverLastSendTimestamp = 800;
            frameInital.OffsetMax = -3000;
            frameInital.OffsetMin = -3100;

            var bytes = frameInital.GetBytes();

            var actual = SaiFrame.Parse(bytes) as SaiTtsFrameEstimate;

            Assert.AreEqual(frameInital.FrameType, actual.FrameType);
            Assert.AreEqual(frameInital.SequenceNo, actual.SequenceNo);
            Assert.AreEqual(frameInital.SenderTimestamp, actual.SenderTimestamp);
            Assert.AreEqual(frameInital.SenderLastRecvTimestamp, actual.SenderLastRecvTimestamp);
            Assert.AreEqual(frameInital.ReceiverLastSendTimestamp, actual.ReceiverLastSendTimestamp);
            Assert.AreEqual(frameInital.OffsetMax, actual.OffsetMax);
            Assert.AreEqual(frameInital.OffsetMin, actual.OffsetMin);
        }
    }
}
