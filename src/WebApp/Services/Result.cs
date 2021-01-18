namespace WebApp.Services
{
    public class Result<TValue>
    {
        internal Result(TValue value, string error, bool isSuccess)
        {
            Value = value;
            Error = error;
            IsSuccess = isSuccess;
        }

        public TValue Value { get; }
        public string Error { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
    }

    public class Result
    {
        public static Result<string> Success(string value) => new Result<string>(value, null, true);
        public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, null, true);

        public static Result<string> Failure(string error) => new Result<string>(default, error, false);
        public static Result<TValue> Failure<TValue>(string error) => new Result<TValue>(default, error, false);
    }
}
