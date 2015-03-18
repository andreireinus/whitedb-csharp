namespace WhiteDb.Data
{
    using System;

    public class DataContext<T> : IDisposable where T : class
    {
        private bool isDisposed = false;

        private readonly DataContext context;

        public DataContext(string name, int size = 100000)
        {
            this.context = new DataContext(name, size);
        }

        ~DataContext()
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
            if (disposing)
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("DataContext");
                }

                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }

            this.isDisposed = true;
        }

        public void Create(T record)
        {
        }
    }
}