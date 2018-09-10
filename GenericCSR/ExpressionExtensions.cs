using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericCSR
{
    public static class ExpressionExtensions
    {
        public static string ExpressionBodyToString<T, TProperty>(this Expression<Func<T, TProperty>> exp)
        {
            if (!TryFindMemberExpression(exp.Body, out MemberExpression memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();
            do
            {
                memberNames.Push(memberExp.Member.Name);
            }
            while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }
        
        private static bool TryFindMemberExpression
        (Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;
            if (memberExp != null)
            {
                return true;
            }

            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression)exp).Operand as MemberExpression;
                if (memberExp != null)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsConversion(Expression exp)
        {
            return (
                exp.NodeType == ExpressionType.Convert ||
                exp.NodeType == ExpressionType.ConvertChecked
            );
        }
    }
}