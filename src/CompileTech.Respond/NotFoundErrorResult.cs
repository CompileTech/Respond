using System.Diagnostics.Contracts;

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
    }
}