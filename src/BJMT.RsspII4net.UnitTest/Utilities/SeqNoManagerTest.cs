using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BJMT.RsspII4net.Utilities;
using System.Threading.Tasks;

namespace BJMT.RsspII4net.UnitTest.Utilities
{
    [TestFixture]
    class SeqNoManagerTest
    {
        [Test]
        public void Test_UpdateSendSeq()
        {
            uint number = 3;

            uint initialValue = 0;
            uint maxValue = 255;

            var mgr = new SeqNoManager(initialValue, maxValue, 3);

            for (uint i = initialValue; i < number * maxValue; i++)
            {
                var seqNo = mgr.GetAndUpdateSendSeq();
                var expectedValue = i % (maxValue - initialValue + 1) + initialValue;

                Assert.AreEqual(expectedValue, seqNo);
            }
        }

        [Test]
        public void Test_UpdateSendSeq_MultiThread()
        {
            uint minValue = 0;
            uint maxValue = 65535;
            var result = new List<uint>();
            uint singleCount = 100;

            var mgr = new SeqNoManager(minValue, maxValue, 1);

            var job = new Action(() =>
            {
                for (var i = mgr.MinSendSeq; i <= singleCount; i++)
                {
                    var value = mgr.GetAndUpdateSendSeq();
                    result.Add(value);
                }
            });

            var jobCount = 5;
            var jobs = new List<Task>();
            for (int i = 0; i < jobCount; i++)
            {
                var task1 = Task.Factory.StartNew(job);
                jobs.Add(task1);
            }

            jobs.ForEach(p => p.Wait());

            Assert.AreEqual(jobCount * singleCount + jobCount - 1, result.Last());
        }

        [Test]
        public void Test_IsExpected()
        {
            uint initialValue = 0;
            uint maxValue = 255;

            var mgr = new SeqNoManager(initialValue, maxValue, 3);
            mgr.UpdateAckSeq(0);

            var actual = mgr.IsExpected(1);
            Assert.AreEqual(true, actual);
            actual = mgr.IsExpected(2);
            Assert.AreEqual(true, actual);
            actual = mgr.IsExpected(3);
            Assert.AreEqual(true, actual);
            actual = mgr.IsExpected(4);
            Assert.AreEqual(false, actual);
            actual = mgr.IsExpected(5);
            Assert.AreEqual(false, actual);


            mgr.UpdateAckSeq(254);
            actual = mgr.IsExpected(255);
            Assert.AreEqual(true, actual);
            actual = mgr.IsExpected(0);
            Assert.AreEqual(true, actual);
            actual = mgr.IsExpected(1);
            Assert.AreEqual(true, actual);
            actual = mgr.IsExpected(2);
            Assert.AreEqual(false, actual);
        }

        [Test]
        public void Test_IsBeyondRange()
        {
            uint initialValue = 0;
            uint maxValue = 255;

            var mgr = new SeqNoManager(initialValue, maxValue, 6);

            mgr.UpdateAckSeq(252);
            var actual = mgr.IsBeyondRange(250);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(251);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(252);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(253);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(254);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(255);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(0);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(1);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(2);
            Assert.AreEqual(false, actual);

            actual = mgr.IsBeyondRange(3);
            Assert.AreEqual(true, actual);

            actual = mgr.IsBeyondRange(4);
            Assert.AreEqual(true, actual);

            actual = mgr.IsBeyondRange(5);
            Assert.AreEqual(true, actual);

            actual = mgr.IsBeyondRange(10);
            Assert.AreEqual(true, actual);
        }
    }
}
