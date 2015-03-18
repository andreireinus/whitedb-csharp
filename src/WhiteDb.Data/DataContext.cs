namespace WhiteDb.Data
{
    using System;

    public class DataContext : IDisposable
    {
        private bool isDisposed = false;
        private IntPtr pointer;

        public DataContext(string name, int size = 100000)
        {
            this.pointer = NativeApiWrapper.wg_attach_database(name, size);

            if (this.pointer == null)
            {
                throw new WhiteDbException(-1, "Unable to attach database");
            }
        }

        ~DataContext()
        {
            this.Dispose(false);
        }

        public DataRecord CreateRecord(int length)
        {
            var recordPointer = NativeApiWrapper.wg_create_record(this.pointer, length);

            return new DataRecord(this.pointer, recordPointer, length);
        }

        public void Delete(DataRecord record)
        {
#warning How to check if record exists in database?
            NativeApiWrapper.wg_delete_record(this.pointer, record.Pointer);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DataRecord GetFirstRecord()
        {
            var recordPointer = NativeApiWrapper.wg_get_first_record(this.pointer);

            if (recordPointer == IntPtr.Zero)
            {
                return null;
            }

            var length = NativeApiWrapper.wg_get_record_len(this.pointer, recordPointer);

            return new DataRecord(this.pointer, recordPointer, length);
        }

        public void PrintDatabase()
        {
            NativeApiWrapper.wg_print_db(this.pointer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("DataContext");
                }
            }

            if (this.pointer != IntPtr.Zero)
            {
                NativeApiWrapper.wg_detach_database(this.pointer);
                this.pointer = IntPtr.Zero;
            }

            this.isDisposed = true;
        }
    }
}