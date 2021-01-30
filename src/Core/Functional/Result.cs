using System;

namespace Core.Functional
{
    public class Result
    {
        protected Result()
        {
        }

        protected Result(string error) => Errors.Add(error);
        protected Result(Exception exception) => Errors.Add(exception);
        protected Result(Errors errors) => Errors = errors;

        public static Result Success() => new Result();
        public static Result Failure(string error) => new Result(error);
        public static Result Failure(Exception exception) => new Result(exception);
        public static Result Failure(Errors errors) => new Result(errors);

        public Errors Errors { get; } = new Errors();
        public bool IsSuccess => !Errors.HasErrors;
        public bool IsFailure => Errors.HasErrors;
    }

    public class Result<T> : Result
    {
        private readonly T? value;

        protected Result(T value)
        {
            this.value = value;
        }

        protected Result(string error) : base(error) { }
        protected Result(Exception exception) : base(exception) { }
        protected Result(Errors errors) : base(errors) { }

        public static Result<T> Success(T value) => new Result<T>(value);
        public new static Result<T> Failure(string error) => new Result<T>(error);
        public new static Result<T> Failure(Exception exception) => new Result<T>(exception);
        public new static Result<T> Failure(Errors errors) => new Result<T>(errors);

        public T? Value
        {
            get
            {
                if (IsFailure) throw new InvalidOperationException("Cannot access the value of a failed result.");
                return value;
            }
        }
    }
}
