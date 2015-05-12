namespace WhiteDb.Data
{
    using System;

    public class Query : IDisposable
    {
        private readonly IntPtr databasePointer;

        private readonly IntPtr queryPointer;

        private bool isDisposed;

        public Query(IntPtr databasePointer, IntPtr queryPointer)
        {
            this.databasePointer = databasePointer;
            this.queryPointer = queryPointer;
        }

        public IntPtr QueryPointer
        {
            get
            {
                return this.queryPointer;
            }
        }

        public DataRecord Fetch()
        {
            var record = NativeApi.wg_fetch(this.databasePointer, this.queryPointer);
            if (record == IntPtr.Zero)
            {
                return null;
            }

            return new DataRecord(this.databasePointer, record);
        }

        ~Query()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                NativeApi.wg_free_query(this.databasePointer, this.queryPointer);
            }

            this.isDisposed = true;
        }
    }
}