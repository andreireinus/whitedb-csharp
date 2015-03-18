namespace WhiteDb.Data.Tests
{
    using NUnit.Framework;

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
    }
}