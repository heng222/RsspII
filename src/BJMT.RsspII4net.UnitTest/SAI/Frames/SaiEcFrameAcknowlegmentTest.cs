using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.SAI;
using BJMT.RsspII4net.SAI.EC.Frames;

namespace BJMT.RsspII4net.UnitTest.SAI.Frames
{
    [TestFixture]
    class SaiEcFrameAcknowlegmentTest
    {
        [Test]
        public void Test1()
        {
            var frame1 = new SaiEcFrameAcknowlegment();
            frame1.SequenceNo = 200;
            frame1.EcValue = 100;
            frame1.UserData = new byte[] { 1 };

            var bytes = frame1.GetBytes();

            var frame2 = SaiFrame.Parse(bytes) as SaiEcFrameAcknowlegment;

            Assert.AreEqual(SaiFrameType.EC_AppDataAcknowlegment, frame2.FrameType);
            Assert.AreEqual(frame2.SequenceNo, frame1.SequenceNo);
            Assert.AreEqual(frame2.EcValue, frame1.EcValue);
            Assert.AreEqual(frame2.UserData[0], frame1.UserData[0]);
        }
    }
}
