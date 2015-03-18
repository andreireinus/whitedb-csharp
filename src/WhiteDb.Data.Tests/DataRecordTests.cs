namespace WhiteDb.Data.Tests
{
    using System;

    using NUnit.Framework;

    using WhiteDb.Data.Utils;

    [TestFixture]
    internal class DataRecordTests
    {
        private DataContext db;

        private DataRecord record;

        [SetUp]
        public void Setup()
        {
            DatabaseUtilites.DeleteDatabase("testdb");
            this.db = new DataContext("testdb");

            this.record = this.db.CreateRecord(1);
        }

        [TearDown]
        public void TearDown()
        {
            if (this.db != null)
            {
                this.db.Dispose();
                this.db = null;
            }
        }

        [Test]
        public void SetField_WhenPassingInteger_ThenValueIsReadCorrectly()
        {
            this.record.SetFieldValue(0, 1);

            Assert.That(this.record.GetFieldValueInteger(0), Is.EqualTo(1));
        }

        [Test]
        public void SetField_WhenPassingChar_ThenValueIsReadCorrectly()
        {
            this.record.SetFieldValue(0, 'A');

            Assert.That(this.record.GetFieldValueChar(0), Is.EqualTo('A'));
        }

        [TestCase(1.1)]
        [TestCase(1001)]
        public void SetField_WhenPassingDouble_ThenValueIsReadCorrectly(double value)
        {
            this.record.SetFieldValue(0, value);
            Assert.AreEqual(this.record.GetFieldValueDouble(0), value, 0.000001);
        }

        [Test]
        public void SetField_WhenPassingDate_ThenValueIsReadCorrectly()
        {
            var value = new DateTime(2042, 12, 31);
            this.record.SetFieldValue(0, value, DateSaveMode.DateOnly);

            var returned = this.record.GetFieldValueDate(0);
            Assert.That(returned, Is.EqualTo(value));
            Assert.That(returned.Year, Is.EqualTo(2042), "Year");
            Assert.That(returned.Month, Is.EqualTo(12), "Month");
            Assert.That(returned.Day, Is.EqualTo(31), "Day");
        }

        [Test]
        public void SetField_WhenPassingTime_ThenValueIsReadCorrectly()
        {
            var value = new DateTime(1, 1, 1, 12, 34, 56, 780);
            this.record.SetFieldValue(0, value, DateSaveMode.TimeOnly);

            var returned = this.record.GetFieldValueTime(0);
            Assert.That(returned, Is.EqualTo(value), "Time");
            Assert.That(returned.Hour, Is.EqualTo(12), "Hour");
            Assert.That(returned.Minute, Is.EqualTo(34), "Minutes");
            Assert.That(returned.Second, Is.EqualTo(56), "Seconds");
            Assert.That(returned.Millisecond, Is.EqualTo(value.Millisecond), "Milliseconds");
        }

        [Test]
        public void SetField_WhenPassingString_ThenValueIsReadCorrectly()
        {
            const string Value = "Kuidas moos kommi sisse sai?";
            this.record.SetFieldValue(0, Value);
            Assert.That(this.record.GetFieldValueString(0), Is.EqualTo(Value));
        }

        [Test]
        public void SetField_WithNegativeIndex_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                { this.record.SetFieldValue(-1, 0); });
        }

        [Test]
        public void SetField_WithIndexLargerThanFieldCount_ThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                { this.record.SetFieldValue(1, 0); });
        }
    }
}