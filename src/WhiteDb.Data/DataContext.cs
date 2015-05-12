namespace WhiteDb.Data
{
    using System;

    public class DataContext : IDisposable
    {
        private bool isDisposed;

        public DataContext(string name, int size = 100000000)
        {
            this.Pointer = NativeApi.wg_attach_existing_database(name);
            if (this.Pointer == IntPtr.Zero)
            {
#if __MonoCS__
                this.pointer = NativeApi.wg_attach_database(name, size);
#else
                this.Pointer = NativeApi.wg_attach_local_database(size);
#endif
            }

            if (this.Pointer == IntPtr.Zero)
            {
                throw new WhiteDbException(-1, "Unable to attach database");
            }
        }

        public IntPtr Pointer { get; private set; }

        public DataRecord CreateRecord(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("length", "Length must be greater than zero.");
            }
            var recordPointer = NativeApi.wg_create_record(this.Pointer, length);

            return new DataRecord(this.Pointer, recordPointer, length);
        }

        public void Delete(DataRecord record)
        {
#warning How to check if record exists in database?
            NativeApi.wg_delete_record(this.Pointer, record.RecordPointer);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public DataRecord GetFirstRecord()
        {
            var recordPointer = NativeApi.wg_get_first_record(this.Pointer);

            if (recordPointer == IntPtr.Zero)
            {
                return null;
            }

            return new DataRecord(this.Pointer, recordPointer);
        }

        public DataRecord GetNextRecord(DataRecord record)
        {
            var recordPointer = NativeApi.wg_get_next_record(this.Pointer, record.RecordPointer);
            if (recordPointer == IntPtr.Zero)
            {
                return null;
            }
            return new DataRecord(this.Pointer, recordPointer);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("DataContext");
                }
            }

            if (this.Pointer != IntPtr.Zero)
            {
                NativeApi.wg_detach_database(this.Pointer);
                this.Pointer = IntPtr.Zero;
            }

            this.isDisposed = true;
        }

        public QueryBuilder CreateQueryBuilder()
        {
            return new QueryBuilder(this.Pointer);
        }
    }
}