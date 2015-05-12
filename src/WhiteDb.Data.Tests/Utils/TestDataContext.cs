namespace WhiteDb.Data.Tests.Utils
{
    public class TestDataContext : DataContext
    {
        private const string DatabaseName = "2";

        public TestDataContext(int size = 1000000)
            : base(DatabaseName, size)
        {
            this.EmptyDatabase();
        }

        private void EmptyDatabase()
        {
            DataRecord record;
            do
            {
                record = this.GetFirstRecord();
                if (record != null)
                {
                    this.Delete(record);
                }
            }
            while (record != null);
        }
    }
}