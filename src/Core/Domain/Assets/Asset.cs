using Core.Ddd;
using Core.Domain.Transactions;
using FluentGuard;
using System;
using System.Collections.Generic;

namespace Core.Domain.Assets
{
    public class Asset : Entity<Guid>
    {
        private string name;
        private string broker;
        private string category;
        private Currency currency;
        private List<Transaction> transactions;

        private Asset()
        {
        }

        public Asset(Guid id, string name, string broker, string category, Currency currency)
        {
            Id = id;
            this.name = name;
            this.broker = broker;
            this.category = category;
            this.currency = currency;
            AddedDateTime = DateTime.UtcNow;

            Validate();
        }

        public string Name
        {
            get => name;
            set { name = value; Validate(); }
        }

        public string Broker
        {
            get => broker;
            set { broker = value; Validate(); }
        }

        public string Category
        {
            get => category;
            set { category = value; Validate(); }
        }

        public Currency Currency
        {
            get => currency;
            set { currency = value; Validate(); }
        }

        public DateTime AddedDateTime
        {
            get;
            private set;
        }

        public IReadOnlyList<Transaction> Transactions => transactions.AsReadOnly();

        private void Validate()
        {
            Guard
                .With(Id, "Id").NotDefault()
                .With(Name, "Name").NotNullOrEmpty().Length(3, 30)
                .With(Broker, "Broker").NotNullOrEmpty().Length(3, 30)
                .With(Category, "Category").NotNullOrEmpty().Length(3, 30)
                .With(Currency, "Currency").NotNull()
                .ThrowIfError();
        }
    }
}
