using CSharpFunctionalExtensions;
using System;

namespace Api.Core.Domain.Assets
{
    public class Asset : Entity<Guid>
    {
        private Asset(Guid id, string name, string broker, string category, string currency)
        {
            Id = id;
            Name = name;
            Broker = broker;
            Category = category;
            Currency = currency;
            AddedDateTime = DateTime.UtcNow;
        }

        public static Result<Asset> Create(Guid id, string name, string broker, string category, string currency)
        {
            if (id == Guid.Empty)
                return Result.Failure<Asset>("Id cannot be an empty guid.");

            return new Asset(
                id, name, broker, category, currency);
        }

        public string Name { get; private set; }

        public string Broker { get; private set; }

        public string Category { get; private set; }

        public string Currency { get; private set; }

        public DateTime AddedDateTime { get; private set; }
    }
}
