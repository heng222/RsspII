using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.ALE.Frames;

namespace BJMT.RsspII4net.UnitTest.ALE.Frames
{
    [TestFixture]
    class AleDataSwitchR2NTest
    {
        [Test]
        public void Test1()
        {
            var data1 = new AleSwitchR2N();
            data1.UserData = new byte[] { 100, 200 };

            var bytes = data1.GetBytes();

            var data2 = new AleSwitchR2N();
            data2.ParseBytes(bytes, 0, bytes.Length - 1);

            Assert.AreEqual(AleFrameType.SwitchR2N, data2.FrameType);
            Assert.AreEqual(100, data2.UserData[0]);
            Assert.AreEqual(200, data2.UserData[1]);
        }
    }
}
