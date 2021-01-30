using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Api.Core.Domain
{
    public class Currency : ValueObject
    {
        public Currency(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            value = value.Trim().ToUpper();
            var pattern = @"^[A-Z]{3}$";
            if (!Regex.IsMatch(value, pattern)) throw new ArgumentException("The currency must be in the format 'AAA'.");

            Value = value;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;

        public static explicit operator Currency(string value) => new Currency(value);
        public static implicit operator string(Currency currency) => currency.ToString();
    }
}
