using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net;

namespace BJMT.RsspII4net.UnitTest.ALE.Frames
{
    [TestFixture]
    class AleFrameTest
    {
        [Test]
        public void Test1()
        {
            var aleData1 = new AleConnectionRequest()
            {
                InitiatorID = 10, ResponderID = 20, @ServiceType = ServiceType.D
            };

            var aleFrame1 = new AleFrame(1, 111, false, aleData1);
            aleFrame1.Version = 1;
            aleFrame1.ApplicationType = 22;
            aleFrame1.SequenceNo = 300;

            Assert.AreEqual(AleFrameType.ConnectionRequest, aleFrame1.FrameType);

            var bytes = aleFrame1.GetBytes();

            var aleFrame2 = new AleFrame();
            aleFrame2.ParseBytes(bytes);

            Assert.AreEqual(aleFrame2.Version, aleFrame1.Version);
            Assert.AreEqual(aleFrame2.ApplicationType, aleFrame1.ApplicationType);
            Assert.AreEqual(aleFrame2.SequenceNo, aleFrame1.SequenceNo);
            Assert.AreEqual(aleFrame2.IsNormal, aleFrame1.IsNormal);

            var aleData2 = aleFrame2.UserData as AleConnectionRequest;
            Assert.NotNull(aleData2);
            Assert.AreEqual(aleData1.InitiatorID, aleData2.InitiatorID);
            Assert.AreEqual(aleData1.ResponderID, aleData2.ResponderID);
            Assert.AreEqual(aleData1.ServiceType, aleData2.ServiceType);
        }


        [Test]
        public void Test2()
        {
            var aleData1 = new AleConnectionConfirm()
            {
                ServerID = 10,
            };

            var aleFrame1 = new AleFrame(1, 111, false, aleData1);
            aleFrame1.Version = 1;
            aleFrame1.ApplicationType = 22;
            aleFrame1.SequenceNo = 300;

            Assert.AreEqual(AleFrameType.ConnectionConfirm, aleFrame1.FrameType);

            var bytes = aleFrame1.GetBytes();

            var aleFrame2 = new AleFrame();
            aleFrame2.ParseBytes(bytes);

            Assert.AreEqual(aleFrame2.Version, aleFrame1.Version);
            Assert.AreEqual(aleFrame2.ApplicationType, aleFrame1.ApplicationType);
            Assert.AreEqual(aleFrame2.SequenceNo, aleFrame1.SequenceNo);
            Assert.AreEqual(aleFrame2.IsNormal, aleFrame1.IsNormal);

            var aleData2 = aleFrame2.UserData as AleConnectionConfirm;
            Assert.NotNull(aleData2);
            Assert.AreEqual(aleData1.ServerID, aleData2.ServerID);
        }

        [Test]
        public void Test3()
        {
            var aleData1 = new AleDataTransmission()
            {
                UserData = new byte[] { 100, 200}
            };

            var aleFrame1 = new AleFrame(1, 111, false, aleData1);
            aleFrame1.Version = 1;
            aleFrame1.ApplicationType = 22;
            aleFrame1.SequenceNo = 300;

            Assert.AreEqual(AleFrameType.DataTransmission, aleFrame1.FrameType);

            var bytes = aleFrame1.GetBytes();

            var aleFrame2 = new AleFrame();
            aleFrame2.ParseBytes(bytes);

            Assert.AreEqual(aleFrame2.Version, aleFrame1.Version);
            Assert.AreEqual(aleFrame2.ApplicationType, aleFrame1.ApplicationType);
            Assert.AreEqual(aleFrame2.SequenceNo, aleFrame1.SequenceNo);
            Assert.AreEqual(aleFrame2.IsNormal, aleFrame1.IsNormal);

            var aleData2 = aleFrame2.UserData as AleDataTransmission;
            Assert.NotNull(aleData2);
            Assert.AreEqual(aleData1.UserData[0], aleData2.UserData[0]);
            Assert.AreEqual(aleData1.UserData[1], aleData2.UserData[1]);
        }
    }
}
