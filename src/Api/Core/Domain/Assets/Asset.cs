using CSharpFunctionalExtensions;
using FluentGuard;
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
            Guard
                .With(id, "Id").NotDefault()
                .With(name, "Name").NotNullOrEmpty().Length(3, 30)
                .With(broker, "Broker").NotNullOrEmpty().Length(3, 30)
                .With(category, "Category").NotNullOrEmpty().Length(3, 30)
                .With(currency, "Currency").NotNullOrEmpty().Length(3)
                .ThrowIfError();

            Id = id;
            Name = name;
            Broker = broker;
            Category = category;
            Currency = currency.ToUpper();
            AddedDateTime = DateTime.UtcNow;
        }

        public string Name
        {
            get => name;
            set => name = Guard.With(value, nameof(Name)).ThrowIfError().Input;
        }

        public string Broker
        {
            get => broker;
            set => broker = Guard.With(value, nameof(Broker)).ThrowIfError().Input;
        }

        public string Category
        {
            get => category;
            set => category = Guard.With(value, nameof(Category)).ThrowIfError().Input;
        }

        public string Currency
        {
            get => currency;
            set => currency = Guard.With(value, nameof(Category)).ThrowIfError().Input.ToUpper();
        }

        public DateTime AddedDateTime
        {
            get => addedDateTime;
            private set => addedDateTime = value;
        }
    }
}
