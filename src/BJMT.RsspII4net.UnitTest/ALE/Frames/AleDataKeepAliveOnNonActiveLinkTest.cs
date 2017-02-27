
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.ALE.Frames;

namespace BJMT.RsspII4net.UnitTest.ALE.Frames
{
    [TestFixture]
    class AleDataKeepAliveOnNonActiveLinkTest
    {
        [Test]
        public void Test1() 
        {
            var data1 = new AleKeepAliveOnNonActiveLink();
            Assert.NotNull(data1);

            var bytes = data1.GetBytes();
        }
    }
}
