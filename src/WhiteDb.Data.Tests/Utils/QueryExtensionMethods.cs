namespace WhiteDb.Data.Tests.Utils
{
    public static class QueryExtensionMethods
    {
        public static int GetRowCount(this Data.Query query)
        {
            var count = 0;
            while (query.Fetch() != null)
            {
                count++;
            }

            return count;
        }
    }
}