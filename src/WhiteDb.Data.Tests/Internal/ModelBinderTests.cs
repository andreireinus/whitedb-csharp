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
            var binder = new ModelBinder<Person>();

            var record = Substitute.For<DataRecord>(IntPtr.Zero, IntPtr.Zero, 2);
            record.GetFieldValueInteger(Arg.Is(0)).Returns(1);
            record.GetFieldValueString(Arg.Is(1)).Returns("Kati");

            var person = binder.FromRecord(record);
            Assert.That(person.Age, Is.EqualTo(1));
            Assert.That(person.Name, Is.EqualTo("Kati"));
        }
    }
}