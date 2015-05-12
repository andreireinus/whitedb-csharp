namespace WhiteDb.Data.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Queryable<T> : IQueryable<T>
    {
        public Queryable(IQueryProvider provider)
        {
            this.Initialize(provider, null);
        }

        internal Queryable(IQueryProvider provider, Expression expression)
        {
            this.Initialize(provider, expression);
        }

        private void Initialize(IQueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (expression != null && !typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentException(String.Format("Not assignable from {0}", expression.Type), "expression");
            }

            this.Provider = provider;
            this.Expression = expression ?? Expression.Constant(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (this.Provider.Execute<IEnumerable<T>>(this.Expression)).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (this.Provider.Execute<System.Collections.IEnumerable>(this.Expression)).GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public Expression Expression { get; private set; }

        public IQueryProvider Provider { get; private set; }
    }
}