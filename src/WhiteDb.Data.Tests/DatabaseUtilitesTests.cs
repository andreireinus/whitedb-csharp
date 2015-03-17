namespace WhiteDb.Data.Tests
{
    using NUnit.Framework;

    [TestFixture]
    internal class DatabaseUtilitesTests
    {
        [Test]
        public void DeleteDatabase_WhenDatabaseExists_DatabaseIsDeleted()
        {
            Assert.DoesNotThrow(() => DatabaseUtilites.DeleteDatabase("does-not-exist"));
        }

        [Test]
        public void DeleteDatabase_WhenDatabaseInUse_ShouldThrowException()
        {
            const string DatabaseName = "name";
            using (new DataContext(DatabaseName, 10000))
            {
                DatabaseUtilites.DeleteDatabase(DatabaseName);
            }
        }
    }
}