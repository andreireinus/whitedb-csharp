namespace WhiteDb.Data.Internal
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal class QueryProvider : IQueryProvider
    {
        private readonly IQueryContext queryContext;

        public QueryProvider(IQueryContext queryContext)
        {
            this.queryContext = queryContext;
        }

        public virtual IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new Queryable<T>(this, expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return this.queryContext.Execute(expression, false);
        }

        T IQueryProvider.Execute<T>(Expression expression)
        {
            return (T)this.queryContext.Execute(expression,
                       (typeof(T).Name == "IEnumerable`1"));
        }
    }
}