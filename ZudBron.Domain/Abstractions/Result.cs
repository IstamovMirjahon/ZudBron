namespace Article.Domain.Abstractions
{
    public sealed class Result<T>
    {
        private Result(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        private Result(Error error)
        {
            Error = error;
            IsSuccess = false;
        }

        public T? Value { get; }
        public Error? Error { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public static Result<T> Success(T value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value), "Success result must have a value.");

            return new Result<T>(value);
        }

        public static Result<T> Failure(Error error)
        {
            if (error is null)
                throw new ArgumentNullException(nameof(error), "Failure result must have an error.");

            return new Result<T>(error);
        }
    }
}
