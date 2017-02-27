using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.UnitTest.Utilities
{
    [TestFixture]
    class CrcToolTest
    {
        [Test]
        public void Test1()
        {
            var buffer = new byte[] { 0x00, 0x1E, 0x01, 0x1A, 0x00, 0x00, 0x01, 0x01};

            var actual = CrcTool.CaculateCCITT16(buffer, 0, buffer.Length);

            Assert.AreEqual(0x1089, actual);
        }
    }
}
