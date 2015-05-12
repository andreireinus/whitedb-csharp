namespace WhiteDb.Data.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;

    using WhiteDb.Data.Utils;

    public class QueryContext<T> : IQueryContext
        where T : class
    {
        private readonly IntPtr database;

        private readonly ModelBinder<T> modelBinder;

        public QueryContext(IntPtr database)
        {
            this.database = database;
            this.modelBinder = new ModelBinder<T>(this.database);
        }

        public object Execute(Expression expression, bool isEnumerable)
        {
            IEnumerable<T> enumerable = this.Execute(expression);

            if (isEnumerable)
            {
                return enumerable;
            }

            return enumerable.First();
        }

        private IEnumerable<T> Execute(Expression expression)
        {
            var queryBuilder = new QueryBuilder<T>(this.database, expression);
            var query = queryBuilder.Execute();

            using (query)
            {
                DataRecord record;
                while ((record = query.Fetch()) != null)
                {
                    yield return this.modelBinder.FromRecord(record);
                }
            }
        }
    }
}