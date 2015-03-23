namespace WhiteDb.Data.Tests
{
    using NUnit.Framework;

    using WhiteDb.Data.Internal;
    using WhiteDb.Data.Tests.Models;
    using WhiteDb.Data.Utils;

    [TestFixture]
    internal class GenericDataContextTests
    {
        private const string DatabaseName = "dbname-9876";

        [SetUp]
        public void SetUp()
        {
            DatabaseUtilites.EmptyDatabase(DatabaseName);
        }

        [Test]
        public void Create_WithPocoModel_SavedTo()
        {
            var person = new Person { Age = 12, Name = "Juku" };
            using (var db = new DataContext<Person>(DatabaseName))
            {
                var actual = db.Create(person);

                Assert.That(actual.Age, Is.EqualTo(person.Age));
                Assert.That(actual.Name, Is.EqualTo(person.Name));
                Assert.That(actual is IRecord, Is.True);
            }
        }
    }
}