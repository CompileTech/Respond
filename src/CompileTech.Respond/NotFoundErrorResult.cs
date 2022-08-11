using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using static CompileTech.Respond.HelperMethods;

namespace CompileTech.Respond
{
    public class NotFoundErrorResult : ErrorResult
    {
        public NotFoundErrorResult()
        {
            Type = ErrorResultType.NotFound;
        }

        [Pure]
        public static NotFoundErrorResult Make(string message, string subject = null, object translationData = null)
        {
            return new NotFoundErrorResult
            {
                Subject = subject,
                Message = message,
                TranslationData = translationData
            };
        }
        
        [Pure]
        public static NotFoundErrorResult Make(Expression<Func<string>> messageKeyExpression, string subject = null, object translationData = null)
        {
            return Make(GetExpressionMemberName(messageKeyExpression), subject, translationData);
        }
    }
}