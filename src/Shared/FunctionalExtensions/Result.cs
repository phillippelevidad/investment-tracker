using System;
using System.Linq;

namespace Shared.FunctionalExtensions
{
    public class Result
    {
        private readonly Errors errors;

        public Result(bool isSuccess, Errors? errors)
        {
            if (isSuccess && (errors?.Any() ?? false))
                throw new ArgumentException("There can't be any errors in a successful result.");

            IsSuccess = isSuccess;
            this.errors = errors ?? new Errors();
        }

        public static Result Success() => new Result(true, null);
        public static Result Failure(Errors errors) => new Result(false, errors);

        public static Result<T> Success<T>(T value) => new Result<T>(true, null, value);
        public static Result<T> Failure<T>(Errors errors) => new Result<T>(false, errors, default);

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Errors Errors
        {
            get
            {
                if (IsFailure) return errors!;
                throw new InvalidOperationException("Cannot access the errors of a successful result.");
            }
        }
    }

    public class Result<T> : Result
    {
        private readonly T? value;

        public Result(bool isSuccess, Errors? errors, T? value)
            : base(isSuccess, errors)
        {
            if (isSuccess && value == null)
                throw new ArgumentNullException(nameof(value), "The value cannot be null in a successful result.");

            this.value = value;
        }

        public T Value
        {
            get
            {
                if (IsSuccess) return value!;
                throw new InvalidOperationException("Cannot access the value of a failed result.");
            }
        }
    }
}
