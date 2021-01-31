using Core.Ddd;
using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Money : ValueObject, IComparable<Money>
    {
        public Money(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public decimal Value { get; }
        public Currency Currency { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }

        public static Money operator +(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return new Money(a.Value + b.Value, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return new Money(a.Value + b.Value, a.Currency);
        }

        public static Money operator *(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return new Money(a.Value * b.Value, a.Currency);
        }

        public static Money operator /(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return new Money(a.Value / b.Value, a.Currency);
        }

        public static bool operator <(Money left, Money right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Money left, Money right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Money left, Money right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Money left, Money right)
        {
            return left.CompareTo(right) >= 0;
        }

        private static void EnsureSameCurrency(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot add money from different currencies.");
        }

        public int CompareTo(Money? other)
        {
            if (other is null) return Value.CompareTo(0M);
            EnsureSameCurrency(this, other);
            return Value.CompareTo(other.Value);
        }
    }
}
