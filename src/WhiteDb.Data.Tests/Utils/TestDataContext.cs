namespace WhiteDb.Data.Tests.Utils
{
    using WhiteDb.Data.Utils;

    public class TestDataContext : DataContext
    {
        public TestDataContext(int size = 100000)
            : base("testdb", size)
        {
            DatabaseUtilites.EmptyDatabase("testdb");
        }
    }
}