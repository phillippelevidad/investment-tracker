using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Api.Core.Domain
{
    public abstract class Enumeration<TKey> : IComparable
        where TKey : IComparable
    {
        [NotNull]
        public string Name { get; private set; }

        [NotNull]
        public TKey Id { get; private set; }

        protected Enumeration(TKey id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<TEnumeration> GetAll<TEnumeration>()
            where TEnumeration : Enumeration<TKey>
        {
            var fields = typeof(TEnumeration).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<TEnumeration>();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration<TKey> otherEnum)
                return false;

            return Id.Equals(otherEnum.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static TEnumeration FromId<TEnumeration>(TKey id)
            where TEnumeration : Enumeration<TKey>
        {
            var matchingItem = Parse<TEnumeration, TKey>(id, "id", item => item.Id.Equals(id));
            return matchingItem;
        }

        public static TEnumeration FromDisplayName<TEnumeration>(string displayName)
            where TEnumeration : Enumeration<TKey>
        {
            var matchingItem = Parse<TEnumeration, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate)
            where T : Enumeration<TKey>
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public static bool CanParseFromDisplayName<T>(string displayName) where T : Enumeration<TKey>
        {
            var parsedItem = GetAll<T>()
                .FirstOrDefault(item => item.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase));

            return parsedItem != null;
        }

        public int CompareTo(object? other)
        {
            if (other == null) return 1;
            if (other is Enumeration<TKey> otherEnum) return Id.CompareTo(otherEnum.Id);
            throw new ArgumentException("Object is not an Enumeration with the same Id type", nameof(other));
        }

        public static explicit operator Enumeration<TKey>(string displayName)
            => FromDisplayName<Enumeration<TKey>>(displayName);

        public static explicit operator Enumeration<TKey>(TKey id)
            => FromId<Enumeration<TKey>>(id);
    }
}
