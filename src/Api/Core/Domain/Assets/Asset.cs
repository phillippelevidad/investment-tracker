using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;

namespace Api.Core.Domain.Assets
{
    public class Asset : Entity<Guid>
    {
        private string name;
        private string broker;
        private string category;
        private string currency;
        private DateTime addedDateTime;

        private Asset()
        {
        }

        public Asset(Guid id, string name, string broker, string category, string currency)
        {
            Id = Guard.Against.Default(id, nameof(id));
            Name = name;
            Broker = broker;
            Category = category;
            Currency = currency;
            AddedDateTime = DateTime.UtcNow;
        }

        public string Name
        {
            get => name;
            set => name = Guard.Against.OutOfLength(value, 3, 30, nameof(name));
        }

        public string Broker
        {
            get => broker;
            set => broker = Guard.Against.OutOfLength(value, 3, 30, nameof(broker));
        }

        public string Category
        {
            get => category;
            set => category = Guard.Against.OutOfLength(value, 3, 30, nameof(category));
        }

        public string Currency
        {
            get => currency;
            set => currency = Guard.Against.OutOfLength(value, 3, 3, nameof(currency));
        }

        public DateTime AddedDateTime
        {
            get => addedDateTime;
            private set => addedDateTime = value;
        }
    }
}
