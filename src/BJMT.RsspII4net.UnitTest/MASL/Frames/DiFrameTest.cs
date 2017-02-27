using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.UnitTest.MASL
{
    [TestFixture]
    class DiFrameTest
    {
        [Test]
        public void Test1()
        {
            var frame1 = new MaslDiFrame(MaslFrameDirection.Client2Server, 3, 2);

            var bytes = frame1.GetBytes();

            var frame2 = new MaslDiFrame();
            frame2.ParseBytes(bytes, 0, bytes.Length -1);

            Assert.AreEqual(frame1.DeviceType, frame2.DeviceType);
            Assert.AreEqual(frame1.Direction, frame2.Direction);
            Assert.AreEqual(frame1.FrameType, frame2.FrameType);
            Assert.AreEqual(frame1.MajorReason, frame2.MajorReason);
            Assert.AreEqual(frame1.MinorReason, frame2.MinorReason);
        }
    }
}
