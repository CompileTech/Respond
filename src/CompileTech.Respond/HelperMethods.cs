using System;
using System.Linq.Expressions;

namespace CompileTech.Respond
{
    public static class HelperMethods
    {
        public static string GetExpressionMemberName(Expression<Func<string>> expression)
        {
            var expressionBody = (MemberExpression)expression.Body;
            var memberName = expressionBody.Member.Name;
            return $"{memberName[0].ToString().ToLower()}{memberName.Substring(1)}";
        }
    }
}