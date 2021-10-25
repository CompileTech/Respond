using System;
using System.Diagnostics.Contracts;

namespace CompileTech.Respond
{
    public enum ErrorResultType
    {
        Unhandled = 0,
        Unauthorized = 1,
        Validation = 2
    }

    public class ErrorResult
    {
        public ErrorResultType Type { get; protected set; } = ErrorResultType.Unhandled;
        public string Subject { get; protected set; }
        public string Message { get; protected set; }
        public object TranslationData { get; protected set; }
    }

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

    public class Response
    {
        private readonly ErrorResult _error;
        private readonly bool _isSuccess;

        public Response()
        {
            _isSuccess = true;
        }

        public Response(ErrorResult error)
        {
            _error = error;
            _isSuccess = false;
        }

        [Pure]
        public static Response Void()
        {
            return new Response();
        }

        [Pure]
        public T Resolve<T>(Func<T> onSuccess, Func<ErrorResult, T> onError)
        {
            if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));
            if (onError == null) throw new ArgumentNullException(nameof(onError));
            return _isSuccess ? onSuccess() : onError(_error);
        }

        public static implicit operator Response(ErrorResult error)
        {
            return new Response(error);
        }
    }

    public class Response<TResult>
    {
        private readonly ErrorResult _error;
        private readonly bool _isSuccess;
        private readonly TResult _result;

        public Response(TResult result)
        {
            _result = result;
            _isSuccess = true;
        }

        public Response(ErrorResult error)
        {
            _error = error;
            _isSuccess = false;
        }

        [Pure]
        public T Resolve<T>(Func<TResult, T> onSuccess, Func<ErrorResult, T> onError)
        {
            if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));
            if (onError == null) throw new ArgumentNullException(nameof(onError));
            return _isSuccess ? onSuccess(_result) : onError(_error);
        }

        public static implicit operator Response<TResult>(TResult result)
        {
            return new Response<TResult>(result);
        }

        public static implicit operator Response<TResult>(ErrorResult error)
        {
            return new Response<TResult>(error);
        }
    }
}