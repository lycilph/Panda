using System;
using System.Linq.Expressions;

namespace Panda.ApplicationCore.Utilities.Validation
{
    public static class ValidationExtensions
    {
        public static ValidationRule<TSender, TRet> Validate<TSender, TRet>(this TSender obj, Expression<Func<TSender, TRet>> property, Func<TRet, bool> func, string message) where TSender : ISupportValidation
        {
            var rule = new ValidationRule<TSender, TRet>
            {
                Source = obj,
                PropertyName = GetPropertyName(property),
                Property = property.Compile(),
                Condition = func,
                Message = message
            };

            obj.AddValidationRule(rule);

            return rule;
        }

        private static string GetPropertyName(this Expression exp)
        {
            var lambda = exp as LambdaExpression;
            if (lambda == null)
                throw new ArgumentException(string.Format("Expression '{0}' is not a lambda expression.", exp));

            var member = lambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format("Expression '{0}' is not a member expression.", exp));

            if (member.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException(string.Format("Expression '{0}' is not a member access expression.", exp));

            return member.Member.Name;
        }
    }
}
