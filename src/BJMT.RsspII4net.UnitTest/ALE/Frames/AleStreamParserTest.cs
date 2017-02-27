using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.ALE.Frames;

namespace BJMT.RsspII4net.UnitTest.ALE.Frames
{
    [TestFixture]
    class AleStreamParserTest
    {
        [Test(Description="正常解析")]
        public void ParseBytesBuffer_Test1()
        {
            var parser = new AleStreamParser();
            var bytes = new byte[] { 0x00, 0x0A, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A };

            var frames = parser.ParseTcpStream(bytes, bytes.Length);

            Assert.AreEqual(frames.Count, 1);
        }

        [Test(Description = "正常解析，第一个字节无效。")]
        public void ParseBytesBuffer_Test11()
        {
            var parser = new AleStreamParser();
            var bytes = new byte[] { 0x00/*无效字节*/, 0x00, 0x08, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A };

            var frames = parser.ParseTcpStream(bytes, bytes.Length);

            Assert.AreEqual(frames.Count, 1);
        }


        [Test(Description = "一个完整的数据流分成两个能否解析。")]
        public void ParseBytesBuffer_Test2()
        {
            var parser = new AleStreamParser();
            var bytes1 = new byte[] { 0x00, 0x09, 0x01, 0x02, 0x03, 0x03/* 前半部分 */};
            var bytes2 = new byte[] { 0x01, 0x04, 0x05, 0x05, 0xAA /* 后半部分 */};

            var frames = parser.ParseTcpStream(bytes1, bytes1.Length);
            Assert.AreEqual(frames.Count, 0);

            frames = parser.ParseTcpStream(bytes2, bytes2.Length);
            Assert.AreEqual(frames.Count, 1);
        }


        [Test(Description = "一个字节流包含两个帧时能否解析。")]
        public void ParseBytesBuffer_Test3()
        {
            var parser = new AleStreamParser();
            var bytes = new byte[] { 0x00, 0x09, 0x01, 0x02, 0x03, 0x03, 0x01, 0x04, 0x05, 0x05, 0xA1, /*第一帧*/
                                     0x00, 0x09, 0x01, 0x02, 0x03, 0x03, 0x01, 0x04, 0x05, 0x05, 0xA2  /*第二帧*/
                                    };

            var frames = parser.ParseTcpStream(bytes, bytes.Length);
            Assert.AreEqual(frames.Count, 2);
        }

        [Test(Description = "字节流中含有无效字节")]
        public void ParseBytesBuffer_Test4()
        {
            var parser = new AleStreamParser();
            var bytes = new byte[] { 0x00/*无效字节*/, 0x00, 0x09, 0x01, 0x02, 0x03, 0x03, 0x01, 0x04, 0x05, 0x05, 0xAA };

            var frames = parser.ParseTcpStream(bytes, bytes.Length);

            Assert.AreEqual(frames.Count, 1);
        }

        [Test(Description = "长度字节超过范围")]
        [ExpectedException]
        public void ParseBytesBuffer_Test5()
        {
            var parser = new AleStreamParser();
            var bytes = new byte[] { 0xFF, 0xFF, 0x01, 0x02, 0x03, 0x03, 0x01, 0x04, 0x05, 0x05, 0xAA };

            var frames = parser.ParseTcpStream(bytes, bytes.Length);

            Assert.AreEqual(frames.Count, 1);
        }
    }
}
