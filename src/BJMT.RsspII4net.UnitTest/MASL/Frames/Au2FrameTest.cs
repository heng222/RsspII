using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.UnitTest.MASL
{
    [TestFixture]
    class Au2FrameTest
    {
        [Test]
        public void Test1()
        {
            var randomA = new byte[] { 0x1f, 0xd3, 0xa1, 0xce, 0x7b, 0x87, 0xe9, 0xb0 };
            var mac = new byte[] { 0xA6, 0x86, 0x33, 0x93, 0xb9, 0x3B, 0x51, 0x0D };
            var frame1 = new MaslAu2Frame(5, 0x8AC001, EncryptionAlgorithm.TripleDES, randomA);
            frame1.MAC = mac;

            var bytes = frame1.GetBytes();

            var frame2 = new MaslAu2Frame();
            frame2.ParseBytes(bytes, 0, bytes.Length);

            Assert.AreEqual(frame1.ServerID, frame2.ServerID);
            Assert.AreEqual(frame1.DeviceType, frame2.DeviceType);
            Assert.AreEqual(frame1.Direction, frame2.Direction);
            Assert.AreEqual(frame1.EncryAlgorithm, frame2.EncryAlgorithm);
            Assert.AreEqual(frame1.FrameType, frame2.FrameType);
            CollectionAssert.AreEqual(frame1.RandomA, frame2.RandomA);
            CollectionAssert.AreEqual(frame1.MAC, frame2.MAC);
        }
    }
}
