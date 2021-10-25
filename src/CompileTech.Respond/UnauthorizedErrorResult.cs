using System.Diagnostics.Contracts;

namespace CompileTech.Respond
{
    public class UnauthorizedErrorResult : ErrorResult
    {
        public UnauthorizedErrorResult()
        {
            Type = ErrorResultType.Unauthorized;
        }

        [Pure]
        public static UnauthorizedErrorResult Make()
        {
            return new UnauthorizedErrorResult();
        }
    }
}