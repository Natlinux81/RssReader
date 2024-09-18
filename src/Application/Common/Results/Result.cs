using System.Net.Http.Headers;

namespace Application.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error? Error { get; }

        protected Result(bool isSuccess, Error error )
        {
            if ( isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new InvalidOperationException("Invalid operation");
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
        public static Result Failure(Error error) => new(false, error);
    }
}