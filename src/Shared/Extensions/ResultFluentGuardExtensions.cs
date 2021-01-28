using FluentGuard;
using Shared.FunctionalExtensions;

namespace Shared.Extensions
{
    public static class ResultFluentGuardExtensions
    {
        public static Result<T> ToResult<T>(this IGuardResult<T> guardResult)
        {
            if (guardResult.IsFailure)
                return Result.Failure<T>(
                    new FunctionalExtensions.Errors(guardResult.Errors.GetExceptions()));

            return Result.Success(guardResult.Value!);
        }
    }
}
