namespace WhiteDb.Data.Internal
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class QueryBuilder<T> : QueryBuilder
        where T : class
    {
        private readonly Expression expression;
        private readonly ModelBinder<T> modelBinder;

        private ConditionOperator currentConditionOperator = ConditionOperator.Equal;

        private Func<UIntPtr> currentEncoder;

        private uint currentFieldIndex;

        public QueryBuilder(IntPtr database, Expression expression)
            : base(database)
        {
            this.expression = expression;
            this.modelBinder = new ModelBinder<T>(database);
        }

        public override Query Execute()
        {
            this.Visit(this.expression);
            var i = 0;
            //this.AddCondition()
            return base.Execute();
        }

        protected virtual Expression Visit(Expression exp)
        {
            if (exp == null)
            {
                return null;
            }

            switch (exp.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    return this.VisitBinary((BinaryExpression)exp);

                case ExpressionType.MemberAccess:
                    return this.VisitMemberAccess((MemberExpression)exp);

                case ExpressionType.Call:
                    return this.VisitMethodCall((MethodCallExpression)exp);

                case ExpressionType.TypeIs:
                //return this.VisitTypeIs((TypeBinaryExpression)exp);

                case ExpressionType.Conditional:
                //return this.VisitConditional((ConditionalExpression)exp);

                case ExpressionType.Constant:
                    return this.VisitConstant((ConstantExpression)exp);

                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                //return this.VisitUnary((UnaryExpression)exp);

                case ExpressionType.Parameter:
                //return this.VisitParameter((ParameterExpression)exp);

                case ExpressionType.Lambda:
                //return this.VisitLambda((LambdaExpression)exp);

                case ExpressionType.New:
                //return this.VisitNew((NewExpression)exp);

                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                //return this.VisitNewArray((NewArrayExpression)exp);

                case ExpressionType.Invoke:
                //return this.VisitInvocation((InvocationExpression)exp);

                case ExpressionType.MemberInit:
                //return this.VisitMemberInit((MemberInitExpression)exp);

                case ExpressionType.ListInit:
                //return this.VisitListInit((ListInitExpression)exp);

                default:
                    throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
            }
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        private Expression VisitBinary(BinaryExpression b)
        {
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.Equal:
                    this.currentConditionOperator = ConditionOperator.Equal;
                    break;

                case ExpressionType.NotEqual:
                    this.currentConditionOperator = ConditionOperator.NotEqual;
                    break;

                case ExpressionType.LessThan:
                    this.currentConditionOperator = ConditionOperator.LessThan;
                    break;

                case ExpressionType.LessThanOrEqual:
                    this.currentConditionOperator = ConditionOperator.LessThanEqual;
                    break;

                case ExpressionType.GreaterThan:
                    this.currentConditionOperator = ConditionOperator.Greater;
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    this.currentConditionOperator = ConditionOperator.GreaterThanEqual;
                    break;

                case ExpressionType.AndAlso:
                    return b;

                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
            }
            this.Visit(b.Right);

            this.AddCondition(this.currentFieldIndex, this.currentConditionOperator, this.currentEncoder);
            return b;
        }

        private Expression VisitConstant(ConstantExpression c)
        {
            if (c.Value == null)
            {
                this.currentEncoder = () => UIntPtr.Zero;
                return c;
            }

            if (c.Value is IQueryable)
            {
                return c;
            }

            switch (Type.GetTypeCode(c.Value.GetType()))
            {
                case TypeCode.String:
                    this.currentEncoder = () => NativeApi.wg_encode_query_param_str(this.databasePointer, c.Value as string, null);
                    break;

                case TypeCode.Int32:
                    this.currentEncoder = () => NativeApi.wg_encode_query_param_int(this.databasePointer, (int)c.Value);
                    break;

                default:
                    throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", c.Value));
            }

            return c;
        }

        private Expression VisitMemberAccess(MemberExpression m)
        {
            this.currentFieldIndex = this.modelBinder.GetFieldIndex(m.Member);

            return m;
        }

        private Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) && (m.Method.Name == "Where" || m.Method.Name == "First"))
            {
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                this.Visit(lambda.Body);
            }
            return m;
        }

        //private Expression VisitUnary(UnaryExpression u)
        //{
        //    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
        //}
        //protected virtual MemberBinding VisitBinding(MemberBinding binding)
        //{
        //    switch (binding.BindingType)
        //    {
        //        case MemberBindingType.Assignment:
        //            return this.VisitMemberAssignment((MemberAssignment)binding);

        //        case MemberBindingType.MemberBinding:
        //            return this.VisitMemberMemberBinding((MemberMemberBinding)binding);

        //        case MemberBindingType.ListBinding:
        //            return this.VisitMemberListBinding((MemberListBinding)binding);

        //        default:
        //            throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
        //    }
        //}

        //protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
        //{
        //    ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments);
        //    if (arguments != initializer.Arguments)
        //    {
        //        return Expression.ElementInit(initializer.AddMethod, arguments);
        //    }
        //    return initializer;
        //}

        //protected virtual Expression VisitTypeIs(TypeBinaryExpression b)
        //{
        //    Expression expr = this.Visit(b.Expression);
        //    if (expr != b.Expression)
        //    {
        //        return Expression.TypeIs(expr, b.TypeOperand);
        //    }
        //    return b;
        //}

        //protected virtual Expression VisitConditional(ConditionalExpression c)
        //{
        //    Expression test = this.Visit(c.Test);
        //    Expression ifTrue = this.Visit(c.IfTrue);
        //    Expression ifFalse = this.Visit(c.IfFalse);
        //    if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
        //    {
        //        return Expression.Condition(test, ifTrue, ifFalse);
        //    }
        //    return c;
        //}

        //protected virtual Expression VisitParameter(ParameterExpression p)
        //{
        //    return p;
        //}

        //protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
        //{
        //    List<Expression> list = null;
        //    for (int i = 0, n = original.Count; i < n; i++)
        //    {
        //        Expression p = this.Visit(original[i]);
        //        if (list != null)
        //        {
        //            list.Add(p);
        //        }
        //        else if (p != original[i])
        //        {
        //            list = new List<Expression>(n);
        //            for (int j = 0; j < i; j++)
        //            {
        //                list.Add(original[j]);
        //            }
        //            list.Add(p);
        //        }
        //    }
        //    if (list != null)
        //    {
        //        return list.AsReadOnly();
        //    }
        //    return original;
        //}

        //protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        //{
        //    Expression e = this.Visit(assignment.Expression);
        //    if (e != assignment.Expression)
        //    {
        //        return Expression.Bind(assignment.Member, e);
        //    }
        //    return assignment;
        //}

        //protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
        //{
        //    IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings);
        //    if (bindings != binding.Bindings)
        //    {
        //        return Expression.MemberBind(binding.Member, bindings);
        //    }
        //    return binding;
        //}

        //protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
        //{
        //    IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers);
        //    if (initializers != binding.Initializers)
        //    {
        //        return Expression.ListBind(binding.Member, initializers);
        //    }
        //    return binding;
        //}

        //protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
        //{
        //    List<MemberBinding> list = null;
        //    for (int i = 0, n = original.Count; i < n; i++)
        //    {
        //        MemberBinding b = this.VisitBinding(original[i]);
        //        if (list != null)
        //        {
        //            list.Add(b);
        //        }
        //        else if (b != original[i])
        //        {
        //            list = new List<MemberBinding>(n);
        //            for (int j = 0; j < i; j++)
        //            {
        //                list.Add(original[j]);
        //            }
        //            list.Add(b);
        //        }
        //    }
        //    if (list != null)
        //        return list;
        //    return original;
        //}

        //protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
        //{
        //    List<ElementInit> list = null;
        //    for (int i = 0, n = original.Count; i < n; i++)
        //    {
        //        ElementInit init = this.VisitElementInitializer(original[i]);
        //        if (list != null)
        //        {
        //            list.Add(init);
        //        }
        //        else if (init != original[i])
        //        {
        //            list = new List<ElementInit>(n);
        //            for (int j = 0; j < i; j++)
        //            {
        //                list.Add(original[j]);
        //            }
        //            list.Add(init);
        //        }
        //    }
        //    if (list != null)
        //        return list;
        //    return original;
        //}

        //protected virtual Expression VisitLambda(LambdaExpression lambda)
        //{
        //    Expression body = this.Visit(lambda.Body);
        //    if (body != lambda.Body)
        //    {
        //        return Expression.Lambda(lambda.Type, body, lambda.Parameters);
        //    }
        //    return lambda;
        //}

        //protected virtual NewExpression VisitNew(NewExpression nex)
        //{
        //    IEnumerable<Expression> args = this.VisitExpressionList(nex.Arguments);
        //    if (args != nex.Arguments)
        //    {
        //        if (nex.Members != null)
        //            return Expression.New(nex.Constructor, args, nex.Members);
        //        else
        //            return Expression.New(nex.Constructor, args);
        //    }
        //    return nex;
        //}

        //protected virtual Expression VisitMemberInit(MemberInitExpression init)
        //{
        //    NewExpression n = this.VisitNew(init.NewExpression);
        //    IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings);
        //    if (n != init.NewExpression || bindings != init.Bindings)
        //    {
        //        return Expression.MemberInit(n, bindings);
        //    }
        //    return init;
        //}

        //protected virtual Expression VisitListInit(ListInitExpression init)
        //{
        //    NewExpression n = this.VisitNew(init.NewExpression);
        //    IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers);
        //    if (n != init.NewExpression || initializers != init.Initializers)
        //    {
        //        return Expression.ListInit(n, initializers);
        //    }
        //    return init;
        //}

        //protected virtual Expression VisitNewArray(NewArrayExpression na)
        //{
        //    IEnumerable<Expression> exprs = this.VisitExpressionList(na.Expressions);
        //    if (exprs != na.Expressions)
        //    {
        //        if (na.NodeType == ExpressionType.NewArrayInit)
        //        {
        //            return Expression.NewArrayInit(na.Type.GetElementType(), exprs);
        //        }
        //        else
        //        {
        //            return Expression.NewArrayBounds(na.Type.GetElementType(), exprs);
        //        }
        //    }
        //    return na;
        //}

        //protected virtual Expression VisitInvocation(InvocationExpression iv)
        //{
        //    IEnumerable<Expression> args = this.VisitExpressionList(iv.Arguments);
        //    Expression expr = this.Visit(iv.Expression);
        //    if (args != iv.Arguments || expr != iv.Expression)
        //    {
        //        return Expression.Invoke(expr, args);
        //    }
        //    return iv;
        //}
    }
}