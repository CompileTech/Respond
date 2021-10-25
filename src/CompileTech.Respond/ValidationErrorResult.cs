using System.Diagnostics.Contracts;

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
    }
}