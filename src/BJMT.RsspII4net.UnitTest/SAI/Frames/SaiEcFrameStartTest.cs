using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.SAI.EC.Frames;
using BJMT.RsspII4net.SAI;

namespace BJMT.RsspII4net.UnitTest.SAI.Frames
{
    [TestFixture]
    class SaiEcFrameStartTest
    {
        [Test]
        public void Test1()
        {
            var frame1 = new SaiEcFrameStart();
            frame1.SequenceNo = 200;
            frame1.Version = 1;
            frame1.InitialValue = 100;
            frame1.Interval = 1000;

            var bytes = frame1.GetBytes();

            var frame2 = SaiFrame.Parse(bytes) as SaiEcFrameStart;

            Assert.AreEqual(SaiFrameType.EC_Start, frame2.FrameType);
            Assert.AreEqual(frame2.Version, frame1.Version);
            Assert.AreEqual(frame2.SequenceNo, frame1.SequenceNo);
            Assert.AreEqual(frame2.InitialValue, frame1.InitialValue);
            Assert.AreEqual(frame2.Interval, frame1.Interval);
        }
    }
}


