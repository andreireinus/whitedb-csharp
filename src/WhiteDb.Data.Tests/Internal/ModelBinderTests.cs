namespace WhiteDb.Data.Tests.Internal
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using WhiteDb.Data.Internal;
    using WhiteDb.Data.Tests.Models;
    using WhiteDb.Data.Tests.Utils;

    [TestFixture]
    internal class ModelBinderTests
    {
        [Test]
        public void Bind_WhenBindingFromDatarecord_SimplePropertiesAreMapped()
        {
            using (var db = new TestDataContext())
            {
                var binder = new ModelBinder<Person>(db.Pointer);

                var record = Substitute.For<DataRecord>(db.Pointer, IntPtr.Zero, 3);
                record.GetFieldValueInteger(Arg.Is(0)).Returns(1);
                record.GetFieldValueString(Arg.Is(1)).Returns("Kati");
                record.GetFieldValueString(Arg.Is(2)).Returns("Karu");

                var person = binder.FromRecord(record);
                Assert.That(person.Age, Is.EqualTo(1));
                Assert.That(person.FirstName, Is.EqualTo("Kati"));
                Assert.That(person.LastName, Is.EqualTo("Karu"));
            }
        }

        [Test]
        public void Bind_WhenBinding_ThenIRecordFieldsMapped()
        {
            using (var db = new TestDataContext())
            {
                var pointerRecord = IntPtr.Add(IntPtr.Zero, 2);

                var binder = new ModelBinder<Person>(db.Pointer);

                var record = Substitute.For<DataRecord>(db.Pointer, pointerRecord, 3);
                record.GetFieldValueInteger(Arg.Is(0)).Returns(1);
                record.GetFieldValueString(Arg.Is(1)).Returns("Kati");
                record.GetFieldValueString(Arg.Is(2)).Returns("Karu");

                var person = binder.FromRecord(record) as IRecord;
                Assert.That(person.Database, Is.EqualTo(db.Pointer));
                Assert.That(person.Record, Is.EqualTo(pointerRecord));
            }
        }

        [Test]
        public void ToRecord_RecordFields_Mapped()
        {
            var person = new Person { Age = 33, FirstName = "Aadu", LastName = "Mesi" };
            using (var db = new TestDataContext())
            {
                var modelBinder = new ModelBinder<Person>(db.Pointer);
                var record = modelBinder.ToRecord(person);

                Assert.That(record.GetFieldValueInteger(0), Is.EqualTo(person.Age));
                Assert.That(record.GetFieldValueString(1), Is.EqualTo(person.FirstName));
                Assert.That(record.GetFieldValueString(2), Is.EqualTo(person.LastName));
            }
        }
    }
}