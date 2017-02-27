using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.UnitTest.MASL
{
    [TestFixture]
    class Au3FrameTest
    {
        [Test]
        public void Test1()
        {
            var mac = new byte[] { 0x2e, 0x93, 0x89, 0x47, 0x97, 0xec, 0x28, 0x74 };
            var frame1 = new MaslAu3Frame();
            frame1.MAC = mac;

            var bytes = frame1.GetBytes();

            var frame2 = new MaslAu3Frame();
            frame2.ParseBytes(bytes, 0, bytes.Length);

            Assert.AreEqual(frame1.DeviceType, frame2.DeviceType);
            Assert.AreEqual(frame1.Direction, frame2.Direction);
            Assert.AreEqual(frame1.FrameType, frame2.FrameType);
            CollectionAssert.AreEqual(frame1.MAC, frame2.MAC);
        }
    }
}
