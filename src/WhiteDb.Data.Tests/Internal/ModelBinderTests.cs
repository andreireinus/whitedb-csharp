namespace WhiteDb.Data.Tests.Internal
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using WhiteDb.Data.Internal;
    using WhiteDb.Data.Tests.Models;

    [TestFixture]
    internal class ModelBinderTests
    {
        [Test]
        public void Bind_WhenBindingFromDatarecord_SimplePropertiesAreMapped()
        {
            var binder = new ModelBinder<Person>(new DataContext("test"));

            var record = Substitute.For<DataRecord>(IntPtr.Zero, IntPtr.Zero, 2);
            record.GetFieldValueInteger(Arg.Is(0)).Returns(1);
            record.GetFieldValueString(Arg.Is(1)).Returns("Kati");

            var person = binder.FromRecord(record);
            Assert.That(person.Age, Is.EqualTo(1));
            Assert.That(person.Name, Is.EqualTo("Kati"));
        }

        [Test]
        public void Bind_WhenBinding_ThenIRecordFieldsMapped()
        {
            var pointerDatabase = IntPtr.Add(IntPtr.Zero, 1);
            var pointerRecord = IntPtr.Add(IntPtr.Zero, 2);

            var binder = new ModelBinder<Person>(new DataContext("test"));

            var record = Substitute.For<DataRecord>(pointerDatabase, pointerRecord, 2);
            record.GetFieldValueInteger(Arg.Is(0)).Returns(1);
            record.GetFieldValueString(Arg.Is(1)).Returns("Kati");

            var person = binder.FromRecord(record) as IRecord;
            Assert.That(person.Database, Is.EqualTo(pointerDatabase));
            Assert.That(person.Record, Is.EqualTo(pointerRecord));
        }

        [Test]
        public void ToRecord_RecordFields_Mapped()
        {
            var person = new Person { Age = 33, Name = "Aadu" };
            using (var db = new DataContext("test"))
            {
                var modelBinder = new ModelBinder<Person>(db);
                var record = modelBinder.ToRecord(person);

                Assert.That(record.GetFieldValueInteger(0), Is.EqualTo(person.Age));
                Assert.That(record.GetFieldValueString(1), Is.EqualTo(person.Name));
            }
        }
    }
}