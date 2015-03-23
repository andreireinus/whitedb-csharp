namespace WhiteDb.Data
{
    using System;

    using WhiteDb.Data.Internal;

    public class DataContext<T> : IDisposable where T : class
    {
        private bool isDisposed = false;

        private readonly DataContext context;
        private readonly ModelBuilder<T> modelBuilder = new ModelBuilder<T>();
        private readonly ModelBinder<T> modelBinder;

        public DataContext(string name, int size = 100000)
        {
            this.context = new DataContext(name, size);

            this.modelBinder = new ModelBinder<T>(this.context);
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

        public T Create(T entity)
        {
            var record = this.modelBinder.ToRecord(entity);

            return this.modelBinder.FromRecord(record);
        }
    }
}