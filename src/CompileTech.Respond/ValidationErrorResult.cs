using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using static CompileTech.Respond.HelperMethods;

namespace CompileTech.Respond
{
    public class ValidationErrorResult : ErrorResult
    {
        public ValidationErrorResult()
        {
            Type = ErrorResultType.Validation;
        }

        [Pure]
        public static ValidationErrorResult None => null;

        [Pure]
        public static ValidationErrorResult Make(string message, string subject = null, object translationData = null)
        {
            return new ValidationErrorResult
            {
                Subject = subject,
                Message = message,
                TranslationData = translationData
            };
        }  
        
        [Pure]
        public static ValidationErrorResult Make(Expression<Func<string>> messageKeyExpression, string subject = null, object translationData = null)
        {
            return Make(GetExpressionMemberName(messageKeyExpression), subject, translationData);
        }
    }
}