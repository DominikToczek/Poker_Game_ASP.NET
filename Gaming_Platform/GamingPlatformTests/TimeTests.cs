using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GamePlatform.Models.Time;

namespace GamingPlatformTests
{
    [TestClass]
    public class ConstructorTimeTests
    {
        [TestMethod]
        [DataRow(01, 13, 25)]
        [DataRow(34, 99, 72)]
        public void Constructor_ThreeParameters(int hh, int mm, int ss)
        {
            Time time = new Time(hh, mm, ss);
            Assert.AreEqual(hh % 24, time.Hours);
            Assert.AreEqual(mm % 60, time.Minutes);
            Assert.AreEqual(ss % 60, time.Seconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(-01, 13, 25)]
        [DataRow(01, -13, 25)]
        [DataRow(01, 13, -25)]
        [DataRow(-01, -13, 25)]
        [DataRow(-01, 13, -25)]
        [DataRow(01, -13, -25)]
        [DataRow(-01, -13, -25)]
        public void Constructor_ThreeParameters_NegativeArgument(int hh, int mm, int ss)
        {
            Time time = new Time(hh, mm, ss);
        }

        [TestMethod]
        [DataRow("13:02:39", 13, 02, 39)]
        public void Constructor_StringParameter(string data, int hh, int mm, int ss)
        {
            Time time = new Time(data);
            Assert.AreEqual(hh, time.Hours);
            Assert.AreEqual(mm, time.Minutes);
            Assert.AreEqual(ss, time.Seconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("-13:02:39")]
        [DataRow("12:13:02:39")]
        [DataRow("aaa")]
        [DataRow("13:2")]
        public void Constructor_StringParameter_IncorrectArgument(string data)
        {
            Time time = new Time(data);
        }
    }

    [TestClass]
    public class ConstructorTimePeriodTests
    {
        [TestMethod]
        [DataRow(34, 99, 72)]
        public void Constructor_ThreeParameters(int hh, int mm, int ss)
        {
            TimePeriod timePeriod = new TimePeriod(hh, mm, ss);
            Assert.AreEqual(35, timePeriod.Hours);
            Assert.AreEqual(40, timePeriod.Minutes);
            Assert.AreEqual(12, timePeriod.Seconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(-01, 13, 25)]
        [DataRow(01, -13, 25)]
        [DataRow(01, 13, -25)]
        [DataRow(-01, -13, 25)]
        [DataRow(-01, 13, -25)]
        [DataRow(01, -13, -25)]
        [DataRow(-01, -13, -25)]
        public void Constructor_ThreeParameters_NegativeArgument(int hh, int mm, int ss)
        {
            Time time = new Time(hh, mm, ss);
        }

        [TestMethod]
        [DataRow("13:02:39", 13, 02, 39)]
        public void Constructor_StringParameter(string data, int hh, int mm, int ss)
        {
            Time time = new Time(data);
            Assert.AreEqual(hh, time.Hours);
            Assert.AreEqual(mm, time.Minutes);
            Assert.AreEqual(ss, time.Seconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("-13:02:39")]
        [DataRow("12:13:02:39")]
        [DataRow("aaa")]
        [DataRow("13:2")]
        public void Constructor_StringParameter_IncorrectArgument(string data)
        {
            Time time = new Time(data);
        }
    }

    [TestClass]
    public class RelationalOperatorsTests
    {
        [TestMethod]
        [DataRow("13:41:52", "13:41:52")]
        public void Time_EqualityOperator(string t1, string t2)
        {
            Time time1 = new Time(t1);
            Time time2 = new Time(t2);
            Assert.IsTrue(time1 == time2);
            Assert.IsFalse(time1 != time2);
        }

        [TestMethod]
        [DataRow("13:41:52", "13:41:52")]
        public void TimePeriod_EqualityOperator(string t1, string t2)
        {
            TimePeriod time1 = new TimePeriod(t1);
            TimePeriod time2 = new TimePeriod(t2);
            Assert.IsTrue(time1 == time2);
            Assert.IsFalse(time1 != time2);
        }

        [TestMethod]
        [DataRow("13:41:52", "12:41:52")]
        [DataRow("13:41:52", "13:10:52")]
        [DataRow("13:41:52", "13:41:02")]
        [DataRow("13:41:52", "09:55:59")]
        [DataRow("13:41:52", "13:20:59")]
        public void Time_GreaterThanOperator(string t1, string t2)
        {
            Time time1 = new Time(t1);
            Time time2 = new Time(t2);
            Assert.IsTrue(time1 > time2);
            Assert.IsFalse(time1 <= time2);
        }

        [TestMethod]
        [DataRow("13:41:52", "12:41:52")]
        [DataRow("13:41:52", "13:10:52")]
        [DataRow("13:41:52", "13:41:02")]
        [DataRow("13:41:52", "09:55:59")]
        [DataRow("13:41:52", "13:20:59")]
        public void Time_LessThanOperator(string t1, string t2)
        {
            Time time1 = new Time(t1);
            Time time2 = new Time(t2);
            Assert.IsTrue(time2 < time1);
            Assert.IsFalse(time2 >= time1);
        }

        [TestMethod]
        [DataRow("13:41:52", "12:41:52")]
        [DataRow("13:41:52", "13:10:52")]
        [DataRow("13:41:52", "13:41:02")]
        [DataRow("13:41:52", "09:55:59")]
        [DataRow("13:41:52", "13:20:59")]
        public void TimePeriod_GreaterThanOperator(string t1, string t2)
        {
            TimePeriod time1 = new TimePeriod(t1);
            TimePeriod time2 = new TimePeriod(t2);
            Assert.IsTrue(time1 > time2);
            Assert.IsFalse(time1 <= time2);
        }

        [TestMethod]
        [DataRow("13:41:52", "12:41:52")]
        [DataRow("13:41:52", "13:10:52")]
        [DataRow("13:41:52", "13:41:02")]
        [DataRow("13:41:52", "09:55:59")]
        [DataRow("13:41:52", "13:20:59")]
        public void TimePeriod_LessThanOperator(string t1, string t2)
        {
            TimePeriod time1 = new TimePeriod(t1);
            TimePeriod time2 = new TimePeriod(t2);
            Assert.IsTrue(time2 < time1);
            Assert.IsFalse(time2 >= time1);
        }
    }
}
