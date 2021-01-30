using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Ddd
{
    public abstract class Enumeration<TEnum, TKey> : IComparable
        where TEnum : Enumeration<TEnum, TKey>
        where TKey : IComparable, IEquatable<TKey>
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

        public static IEnumerable<TEnum> ListAll()
        {
            var fields = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<TEnum>();
        }

        public static IEnumerable<TKey> ListAllIds()
        {
            return ListAll().Select(entry => entry.Id);
        }

        public static IEnumerable<string> ListAllNames()
        {
            return ListAll().Select(entry => entry.Name);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not TEnum otherEnum)
                return false;

            return Id.Equals(otherEnum.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static TEnum FromId(TKey id)
        {
            var matchingItem = Parse(id, "id", item => item.Id.Equals(id));
            return matchingItem;
        }

        public static TEnum FromName(string name)
        {
            var matchingItem = Parse(name, "name", item => item.Name == name);
            return matchingItem;
        }

        private static TEnum Parse(object value, string description, Func<TEnum, bool> predicate)
        {
            var matchingItem = ListAll().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(TEnum)}");

            return matchingItem;
        }

        public static bool CanParseFromId(TKey id)
        {
            var parsedItem = ListAll()
                .FirstOrDefault(item => item.Id.Equals(id));

            return parsedItem != null;
        }

        public static bool CanParseFromName(string name)
        {
            var parsedItem = ListAll()
                .FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return parsedItem != null;
        }

        public int CompareTo(object? other)
        {
            if (other == null) return 1;
            if (other is TEnum otherEnum) return Id.CompareTo(otherEnum.Id);
            throw new ArgumentException("Object is not an Enumeration with the same Id type", nameof(other));
        }

        public static explicit operator Enumeration<TEnum, TKey>(string name)
            => FromName(name);

        public static explicit operator Enumeration<TEnum, TKey>(TKey id)
            => FromId(id);
    }
}
