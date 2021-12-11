﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Namek.Library.Helpers
{
    public static class ExpressionHelpers
    {
        public static Expression<Func<T, bool>> CombineOr<T>(params Expression<Func<T, bool>>[] filters)
        {
            return filters.CombineOr();
        }

        public static Expression<Func<T, bool>> CombineOr<T>(this IEnumerable<Expression<Func<T, bool>>> filters)
        {
            if (!filters.Any())
            {
                Expression<Func<T, bool>> alwaysTrue = x => true;
                return alwaysTrue;
            }
            var firstFilter = filters.First();

            var lastFilter = firstFilter;
            Expression<Func<T, bool>> result = null;
            foreach (var nextFilter in filters.Skip(1))
            {
                var nextExpression =
                    new ReplaceVisitor(lastFilter.Parameters[0], nextFilter.Parameters[0]).Visit(lastFilter.Body);
                result = Expression.Lambda<Func<T, bool>>(Expression.OrElse(nextExpression, nextFilter.Body),
                    nextFilter.Parameters);
                lastFilter = nextFilter;
            }
            return result;
        }

        public static Expression<Func<T, bool>> CombineAnd<T>(params Expression<Func<T, bool>>[] filters)
        {
            return filters.CombineAnd();
        }

        public static Expression<Func<T, bool>> CombineAnd<T>(this IEnumerable<Expression<Func<T, bool>>> filters)
        {
            if (!filters.Any())
            {
                Expression<Func<T, bool>> alwaysTrue = x => true;
                return alwaysTrue;
            }
            var firstFilter = filters.First();

            var lastFilter = firstFilter;
            Expression<Func<T, bool>> result = null;
            foreach (var nextFilter in filters.Skip(1))
            {
                var nextExpression =
                    new ReplaceVisitor(lastFilter.Parameters[0], nextFilter.Parameters[0]).Visit(lastFilter.Body);
                result = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(nextExpression, nextFilter.Body),
                    nextFilter.Parameters);
                lastFilter = nextFilter;
            }
            return result;
        }

        private class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression from, to;

            public ReplaceVisitor(Expression from, Expression to)
            {
                this.from = from;
                this.to = to;
            }

            public override Expression Visit(Expression node)
            {
                return node == from ? to : base.Visit(node);
            }
        }
    }
}