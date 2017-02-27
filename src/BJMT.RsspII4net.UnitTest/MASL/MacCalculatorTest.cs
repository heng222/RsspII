using System;
using System.Diagnostics;
using System.Linq;
using BJMT.RsspII4net.MASL;
using NUnit.Framework;

namespace BJMT.RsspII4net.UnitTest.Utilities
{

    [TestFixture]
    class MacCalculatorTest
    {
        [Test(Description = "计算AU2的Mac")]
        public void Test2()
        {
            var keys = new byte[] 
            { 
                0xA3, 0x45, 0x34, 0x68, 0x98, 0x01, 0x2A, 0xBF,
                0xCD, 0xBE, 0x34, 0x56, 0x78, 0xBF, 0xEA, 0x32,
                0x12, 0xAE, 0x34, 0x21, 0x45, 0x78, 0x98, 0x50
            };

            var macCalc = new TrippleDesMacCalculator(keys);
            macCalc.RandomA = new byte[] { 0x1F, 0xD3, 0xA1, 0xCE, 0x7B, 0x87, 0xE9, 0xB0 };
            macCalc.RandomB = new byte[] { 0x0F, 0x95, 0xEF, 0x4A, 0x66, 0x25, 0xA9, 0x0D };
            macCalc.UpdateSessionKeys();
            var destAddr = new byte[] { 0x8a, 0xc0, 0x02 };

            var text2 = new byte[] { 0x00, 0x1B/*L*/, 0x8a, 0xc0, 0x02/*DA*/, 0xc5/*ETY+MTI+DF*/, 0x8a, 0xc0, 0x01/*SA*/, 01/*SaF*/ };
            var data = text2.Concat(macCalc.RandomA).Concat(macCalc.RandomB).Concat(destAddr).ToArray(); // Text2|RandA|RandB

            var macActual = macCalc.CalcMac(data);

            var macExpected = new byte[] { 0xa6, 0x86, 0x33, 0x93, 0xb9, 0x3b, 0x51, 0x0d};
            CollectionAssert.AreEqual(macExpected, macActual);
        }

        [Test(Description="计算AU3的Mac")]
        public void Test4()
        {
            var keys = new byte[] 
            { 
                0xA3, 0x45, 0x34, 0x68, 0x98, 0x01, 0x2A, 0xBF,
                0xCD, 0xBE, 0x34, 0x56, 0x78, 0xBF, 0xEA, 0x32,
                0x12, 0xAE, 0x34, 0x21, 0x45, 0x78, 0x98, 0x50
            };

            var macCalc = new TrippleDesMacCalculator(keys);
            macCalc.RandomA = new byte[] { 0x1F, 0xD3, 0xA1, 0xCE, 0x7B, 0x87, 0xE9, 0xB0 };
            macCalc.RandomB = new byte[] { 0x0F, 0x95, 0xEF, 0x4A, 0x66, 0x25, 0xA9, 0x0D };
            macCalc.UpdateSessionKeys();
            
            var text5 = new byte[] { 0x00, 0x14/*L*/, 0x8a, 0xc0, 0x01/*DA*/, 0x06/*ETY+MTI+DF*/ };
            var data = text5.Concat(macCalc.RandomB).Concat(macCalc.RandomA).ToArray(); // Text5|RandB|RandA

            var macActual = macCalc.CalcMac(data);

            var macExpected = new byte[] { 0x2e, 0x93, 0x89, 0x47, 0x97, 0xec, 0x28, 0x74 };
            CollectionAssert.AreEqual(macExpected, macActual);
        }

        [Test(Description="Mac计算性能测试")]
        public void Test_Performance()
        {
            int dataLen = 4 * 1024;
            var data = new byte[dataLen];
            var random = new Random();
            random.NextBytes(data);

            // 
            var keys = new byte[] 
            { 
                0xA3, 0x45, 0x34, 0x68, 0x98, 0x01, 0x2A, 0xBF,
                0xCD, 0xBE, 0x34, 0x56, 0x78, 0xBF, 0xEA, 0x32,
                0x12, 0xAE, 0x34, 0x21, 0x45, 0x78, 0x98, 0x50
            };
            var macCalc = new TrippleDesMacCalculator(keys);

            // 
            var sw = new Stopwatch();
            sw.Start();

            int count = 100;
            for (int i = 0; i < count; i++)
            {
                macCalc.CalcMac(data);
            }
            sw.Stop();

            //
            Console.WriteLine(string.Format("计算{0}次MAC（数据长度{1}）使用时间={2}毫秒。", count, dataLen, sw.ElapsedMilliseconds));
        }
    }
}
