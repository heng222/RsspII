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
    class SaiTtsFrameOffsetEndTest
    {
        [Test]
        public void Test1()
        {
            var frameInital = new SaiTtsFrameOffsetEnd();

            frameInital.SequenceNo = 100;
            frameInital.SenderTimestamp = 1000;
            frameInital.SenderLastRecvTimestamp = 900;
            frameInital.ReceiverLastSendTimestamp = 800;
            frameInital.Valid = true;

            var bytes = frameInital.GetBytes();

            var actual = SaiFrame.Parse(bytes) as SaiTtsFrameOffsetEnd;

            Assert.AreEqual(frameInital.FrameType, actual.FrameType);
            Assert.AreEqual(frameInital.SequenceNo, actual.SequenceNo);
            Assert.AreEqual(frameInital.SenderTimestamp, actual.SenderTimestamp);
            Assert.AreEqual(frameInital.SenderLastRecvTimestamp, actual.SenderLastRecvTimestamp);
            Assert.AreEqual(frameInital.ReceiverLastSendTimestamp, actual.ReceiverLastSendTimestamp);
            Assert.AreEqual(frameInital.Valid, actual.Valid);
        }
    }
}
