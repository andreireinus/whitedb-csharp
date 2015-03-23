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
            var db = new DataContext("test");
            var binder = new ModelBinder<Person>(db);

            var record = Substitute.For<DataRecord>(db, IntPtr.Zero, 2);
            record.GetFieldValueInteger(Arg.Is(0)).Returns(1);
            record.GetFieldValueString(Arg.Is(1)).Returns("Kati");

            var person = binder.FromRecord(record);
            Assert.That(person.Age, Is.EqualTo(1));
            Assert.That(person.Name, Is.EqualTo("Kati"));
        }

        [Test]
        public void Bind_WhenBinding_ThenIRecordFieldsMapped()
        {
            var db = new DataContext("test");
            var pointerRecord = IntPtr.Add(IntPtr.Zero, 2);

            var binder = new ModelBinder<Person>(db);

            var record = Substitute.For<DataRecord>(db, pointerRecord, 2);
            record.GetFieldValueInteger(Arg.Is(0)).Returns(1);
            record.GetFieldValueString(Arg.Is(1)).Returns("Kati");

            var person = binder.FromRecord(record) as IRecord;
            Assert.That(person.Database, Is.EqualTo(db.Pointer));
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

        [Test]
        public void ToRecord_ArrayOfItegers()
        {
            var model = new ArrayOfIntegers { Numbers = new[] { 1, 2, 3, 4, 5 } };
            using (var db = new TestDataContext())
            {
                var binder = new ModelBinder<ArrayOfIntegers>(db);
                var record = binder.ToRecord(model);
                var subRecord = record.GetFieldValueRecord(0);

                for (var index = 0; index < model.Numbers.Length; index++)
                {
                    Assert.That(subRecord.GetFieldValueInteger(index), Is.EqualTo(model.Numbers[index]), "Incorrect at index: {0}", index);
                }
            }
        }
    }
}