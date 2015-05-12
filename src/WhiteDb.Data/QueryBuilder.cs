namespace WhiteDb.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;

    using WhiteDb.Data.Internal;

    public class QueryBuilder
    {
        protected readonly IntPtr databasePointer;

        private readonly List<QueryCondition> conditions = new List<QueryCondition>();

        public QueryBuilder(IntPtr databasePointer)
        {
            this.databasePointer = databasePointer;
        }

        public virtual Query Execute()
        {
            QueryCondition[] args = null;
            var argsLength = 0;

            if (this.conditions.Any())
            {
                args = this.conditions.ToArray();
                argsLength = args.Length;
            }

            var pointer = NativeApi.wg_make_query(this.databasePointer, IntPtr.Zero, 0, args, argsLength);

            return new Query(this.databasePointer, pointer);
        }

        public QueryBuilder AddCondition(uint column, ConditionOperator condition, int value)
        {
            return this.AddCondition(column, condition, () => NativeApi.wg_encode_query_param_int(this.databasePointer, value));
        }

        public QueryBuilder AddCondition(uint column, ConditionOperator condition, string value)
        {
            return this.AddCondition(column, condition, () => NativeApi.wg_encode_query_param_str(this.databasePointer, value, null));
        }

        public QueryBuilder AddCondition(uint column, ConditionOperator condition, Func<UIntPtr> encoder)
        {
            this.conditions.Add(new QueryCondition
            {
                Column = new UIntPtr(column),
                Condition = new UIntPtr((uint)condition.GetHashCode()),
                Value = encoder()
            });

            return this;
        }
    }
}