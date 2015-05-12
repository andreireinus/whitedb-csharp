namespace WhiteDb.Data.Utils
{
    public static class DatabaseUtilites
    {
        public static void DeleteDatabase(string name)
        {
            var result = NativeApi.wg_delete_database(name);

            if (result != 0)
            {
                throw new WhiteDbException(result, "Delete database failed");
            }
        }
    }
}