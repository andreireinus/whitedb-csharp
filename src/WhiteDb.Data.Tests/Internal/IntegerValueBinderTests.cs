namespace WhiteDb.Data.Tests.Internal
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using WhiteDb.Data.Tests.Utils;
    using WhiteDb.Data.ValueBinders;

    [TestFixture]
    public class IntegerValueBinderTests
    {
        [Test]
        public void GetValue_WHEN_EXPECTED_RESULT()
        {
            var db = new TestDataContext();
            var binder = new IntegerValueBinder();

            var record = Substitute.For<DataRecord>(db, IntPtr.Zero, 1);
            record.GetFieldValueInteger(Arg.Is(0)).Returns(23);

            var value = binder.GetValue(record, 0);
            Assert.That(value, Is.EqualTo(23));
        }

        [Test]
        public void SetValue_WHEN_EXPECTED_RESULT()
        {
            using (var db = new TestDataContext())
            {
                var binder = new IntegerValueBinder();

                var record = db.CreateRecord(1);
                binder.SetValue(record, 0, 878);
                Assert.That(record.GetFieldValueInteger(0), Is.EqualTo(878));
            }
        }
    }
}