namespace WhiteDb.TestApplication
{
    using System;

    using WhiteDb.Data;

    public static class Program
    {
        private static void Main(string[] args)
        {
            DatabaseUtilites.EmptyDatabase("name");
            using (var db = new DataContext("name"))
            {
                var record = db.CreateRecord(2);
                record.SetFieldValue(0, DateTime.Now, DateSaveMode.TimeOnly);
                db.PrintDatabase();
            }
            /*
             * wg_int wg_delete_record(void* db, void *rec);
void* wg_get_first_record(void* db);
void* wg_get_next_record(void* db, void* record);
void *wg_get_first_parent(void* db, void *record);
void *wg_get_next_parent(void* db, void* record, void *parent);
             */
            Console.ReadKey();
        }
    }
}