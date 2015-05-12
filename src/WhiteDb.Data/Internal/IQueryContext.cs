namespace WhiteDb.Data.Internal
{
    using System.Linq.Expressions;

    internal interface IQueryContext
    {
        object Execute(Expression expression, bool isEnumerable);
    }
}