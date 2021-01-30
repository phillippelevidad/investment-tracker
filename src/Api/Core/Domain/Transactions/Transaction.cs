using Api.Core.Domain.Assets;
using CSharpFunctionalExtensions;
using System;

namespace Api.Core.Domain.Transactions
{
    public class Transaction : Entity<Guid>
    {
        private Transaction() { }

        public Transaction(Guid id, Asset asset, Operation operation, float quantity, Money unitPrice, DateTime dateTime)
        {
            Id = id;
            Asset = asset;
            Operation = operation;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DateTime = dateTime;
        }

        public Asset Asset { get; }
        public Operation Operation { get; }
        public float Quantity { get; }
        public Money UnitPrice { get; }
        public DateTime DateTime { get; }

        public Money Volume => new Money((decimal)Quantity * UnitPrice.Value, UnitPrice.Currency);
        public Money LiquidVolume => new Money(Volume.Value * Operation.Multiplier, UnitPrice.Currency);
    }
}
