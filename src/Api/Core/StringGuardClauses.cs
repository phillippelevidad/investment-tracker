using System;

namespace Ardalis.GuardClauses
{
    public static class StringGuardClauses
    {
        public static string MinLength(this IGuardClause guard, string input, int minLength, string parameterName)
        {
            return OutOfLength(guard, input, minLength, int.MaxValue, parameterName);
        }

        public static string MaxLength(this IGuardClause guard, string input, int maxLength, string parameterName)
        {
            return OutOfLength(guard, input, 0, maxLength, parameterName);
        }

        public static string OutOfLength(this IGuardClause guard, string input, int minLength, int maxLength, string parameterName)
        {
            if ((input?.Length ?? 0) < minLength)
                throw new ArgumentException($"'{parameterName}' must have at least {minLength} characters.");

            if (input != null && input.Length > maxLength)
                throw new ArgumentException($"'{parameterName}' must have at most {maxLength} characters.");

            return input;
        }
    }
}
