using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.ALE.Frames;

namespace BJMT.RsspII4net.UnitTest.ALE.Frames
{
    [TestFixture]
    class AleDataConnectionRequestTest
    {
        [Test]
        public void Test1() 
        {
            var data1 = new AleConnectionRequest();
            data1.InitiatorID = 11;
            data1.ResponderID = 12;
            data1.UserData = new byte[] { 100, 200};

            var bytes = data1.GetBytes();

            var data2 = new AleConnectionRequest();
            data2.ParseBytes(bytes, 0, bytes.Length - 1);

            Assert.AreEqual(AleFrameType.ConnectionRequest, data2.FrameType);
            Assert.AreEqual(11, data2.InitiatorID);
            Assert.AreEqual(12, data2.ResponderID);
            Assert.AreEqual(100, data2.UserData[0]);
            Assert.AreEqual(200, data2.UserData[1]);
        }
    }
}

