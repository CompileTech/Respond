using System;
using System.Diagnostics.Contracts;

namespace CompileTech.Respond
{
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