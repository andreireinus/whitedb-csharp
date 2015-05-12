namespace WhiteDb.Data.Tests
{
    using NUnit.Framework;

    using WhiteDb.Data.Internal;
    using WhiteDb.Data.Tests.Models;

    [TestFixture]
    internal class GenericDataContextTests
    {
        private const string DatabaseName = "1234";

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Create_WithPocoModel_SavedTo()
        {
            var person = new Person { Age = 12, FirstName = "Juku", LastName = "Juss" };
            using (var db = new DataContext<Person>(DatabaseName))
            {
                var actual = db.Create(person);

                Assert.That(actual.Age, Is.EqualTo(person.Age));
                Assert.That(actual.FirstName, Is.EqualTo(person.FirstName));
                Assert.That(actual.LastName, Is.EqualTo(person.LastName));
                Assert.That(actual is IRecord, Is.True);
            }
        }
    }
}