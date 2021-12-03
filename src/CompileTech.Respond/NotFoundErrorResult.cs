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
        public static NotFoundErrorResult Make()
        {
            return new NotFoundErrorResult();
        }
    }
}