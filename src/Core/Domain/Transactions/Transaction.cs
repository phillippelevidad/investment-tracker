using Core.Ddd;
using Core.Domain.Assets;
using System;

namespace Core.Domain.Transactions
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

        public Asset Asset { get; private set; }
        public Operation Operation { get; private set; }
        public float Quantity { get; private set; }
        public Money UnitPrice { get; private set; }
        public DateTime DateTime { get; private set; }

        public Money Volume => new Money((decimal)Quantity * UnitPrice.Value, UnitPrice.Currency);
        public Money LiquidVolume => new Money(Volume.Value * Operation.Multiplier, UnitPrice.Currency);
    }
}
