using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.UnitTest.MASL
{
    [TestFixture]
    class Au1FrameTest
    {
        [Test]
        public void Test1()
        {
            var randomB = new byte[] { 0x0F, 0x95, 0xEF, 0x4A, 0x66, 0x25, 0xA9, 0x0D };
            var frame1 = new MaslAu1Frame(5, 0x8AC002, EncryptionAlgorithm.TripleDES, randomB);

            var bytes = frame1.GetBytes();

            var frame2 = new MaslAu1Frame();
            frame2.ParseBytes(bytes, 0, bytes.Length);

            Assert.AreEqual(frame1.ClientID, frame2.ClientID);
            Assert.AreEqual(frame1.DeviceType, frame2.DeviceType);
            Assert.AreEqual(frame1.Direction, frame2.Direction);
            Assert.AreEqual(frame1.EncryAlgorithm, frame2.EncryAlgorithm);
            Assert.AreEqual(frame1.FrameType, frame2.FrameType);
            CollectionAssert.AreEqual(frame1.RandomB, frame2.RandomB);
        }
    }
}
