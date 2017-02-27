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
    class SaiFrameTest
    {
        [Test]
        [ExpectedException]
        public void Test1()
        {
            var bytes = new byte[20];

            SaiFrame.Parse(bytes);
        }
    }
}
