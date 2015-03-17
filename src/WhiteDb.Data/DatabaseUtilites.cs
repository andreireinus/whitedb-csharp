namespace WhiteDb.Data
{
    public static class DatabaseUtilites
    {
        public static void DeleteDatabase(string name)
        {
            var result = NativeApiWrapper.wg_delete_database(name);

            if (result != 0)
            {
                throw new WhiteDbException(result, "Delete database failed");
            }
        }

        public static void EmptyDatabase(string name)
        {
            using (var db = new DataContext(name))
            {
                DataRecord record;
                do
                {
                    record = db.GetFirstRecord();
                    if (record != null)
                    {
                        db.Delete(record);
                    }
                }
                while (record != null);
            }
        }
    }
}