namespace WhiteDb.Data.Tests.Internal
{
    using System;
    using System.Linq;
    using System.Reflection;

    using NUnit.Framework;

    using WhiteDb.Data.Internal;
    using WhiteDb.Data.Tests.Models;

    [TestFixture]
    internal class ModelBuilderTests
    {
        [Test]
        public void Build_CreateModel_PropertiesAreSame()
        {
            var builder = new ModelBuilder<Person>();

            var actual = GetPropertyNames(builder.Build().GetType());
            var expected = GetPropertyNames(typeof(Person));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Build_CreatedModel_ResultTypeShouldNotBeSameAsSourceButExtended()
        {
            var builder = new ModelBuilder<Person>();
            var model = builder.Build();

            Assert.That(model.GetType(), Is.Not.EqualTo(typeof(Person)));
        }

        [Test]
        public void Build_AddingIRecord_HasInterfaceAdded()
        {
            var builder = new ModelBuilder<Person>();
            var model = builder.Build();
            Assert.That(model is IRecord, Is.True);
        }

        [Test]
        public void Build_Person_ValuesAreSame()
        {
            var builder = new ModelBuilder<Person>();
            var expected = new Person { Age = 20, Name = "Kati" };
            var actual = builder.Build(expected);

            Assert.That(ReferenceEquals(expected, actual), Is.False);
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Age, Is.EqualTo(expected.Age));
        }

        private static string[] GetPropertyNames(IReflect type)
        {
            const BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.Public;

            return type.GetProperties(BindingFlags).Select(a => a.Name).ToArray();
        }
    }
}