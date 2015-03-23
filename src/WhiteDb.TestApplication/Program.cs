namespace WhiteDb.TestApplication
{
    using System;

    using WhiteDb.Data;
    using WhiteDb.Data.Utils;

    public static class Program
    {
        private static void Main(string[] args)
        {
            DatabaseUtilites.EmptyDatabase("name");
            using (var db = new DataContext("name"))
            {
                var record = db.CreateRecord(2);

                var record2 = db.CreateRecord(2);
                record2.SetFieldValue(0, "Andrei");
                record2.SetFieldValue(1, 33);
                int val = NativeApiWrapper.wg_encode_record(record2.DatabasePointer, record2.RecordPointer);
                NativeApiWrapper.wg_set_field(record.DatabasePointer, record.RecordPointer, 0, val);

                var count = 50;
                var record3 = db.CreateRecord(count);
                for (var i = 0; i < count; i++)
                {
                    record3.SetFieldValue(i, i * 3);
                }

                NativeApiWrapper.wg_set_field(record.DatabasePointer, record.RecordPointer, 1, NativeApiWrapper.wg_encode_record(record3.DatabasePointer, record3.RecordPointer));

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