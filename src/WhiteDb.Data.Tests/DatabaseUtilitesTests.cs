namespace WhiteDb.Data.Tests
{
    using NUnit.Framework;

    using WhiteDb.Data.Utils;

    [TestFixture]
    internal class DatabaseUtilitesTests
    {
        [Test]
        public void DeleteDatabase_WhenDatabaseExists_DatabaseIsDeleted()
        {
            Assert.DoesNotThrow(() => DatabaseUtilites.DeleteDatabase("1234"));
        }
    }
}