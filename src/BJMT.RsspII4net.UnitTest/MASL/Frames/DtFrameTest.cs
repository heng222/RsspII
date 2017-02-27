using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.UnitTest.MASL
{
    [TestFixture]
    class DtFrameTest
    {
        [Test]
        public void Test1()
        {
            var data = new byte[] { 0x11, 0xF1, 0x1C};
            var mac = new byte[] { 0x4f, 0x2c, 0xe, 0xa1, 0x3f, 0x3e, 0xb4, 0x4a };
            var frame1 = new MaslDtFrame(MaslFrameDirection.Client2Server, data);
            frame1.MAC = mac;

            var bytes = frame1.GetBytes();

            var frame2 = new MaslDtFrame();
            frame2.ParseBytes(bytes, 0, bytes.Length -1);

            Assert.AreEqual(frame1.DeviceType, frame2.DeviceType);
            Assert.AreEqual(frame1.Direction, frame2.Direction);
            Assert.AreEqual(frame1.FrameType, frame2.FrameType);
            CollectionAssert.AreEqual(frame1.UserData, frame2.UserData);
            CollectionAssert.AreEqual(frame1.MAC, frame2.MAC);
        }
    }
}
