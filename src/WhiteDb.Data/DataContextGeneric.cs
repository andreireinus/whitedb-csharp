namespace WhiteDb.Data
{
    using System;
    using System.Linq;

    using WhiteDb.Data.Internal;

    public class DataContext<T> : IDisposable
        where T : class
    {
        private bool isDisposed;

        private readonly DataContext context;

        private readonly ModelBinder<T> modelBinder;

        public DataContext(string name, int size = 100000000)
        {
            this.context = new DataContext(name, size);

            this.modelBinder = new ModelBinder<T>(this.context.Pointer);
        }

        public IQueryable<T> Query()
        {
            return new Queryable<T>(new QueryProvider(new QueryContext<T>(this.context.Pointer)));
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