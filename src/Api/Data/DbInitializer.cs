using Api.Core.Domain;
using Api.Core.Domain.Assets;
using System;
using System.Linq;

namespace Api.Data
{
    internal static class DbInitializer
    {
        public static void Initialize(InvestmentDbContext context)
        {
            context.Database.EnsureCreated();

            SeedAssets(context);
        }

        private static void SeedAssets(InvestmentDbContext context)
        {
            if (context.Assets.Any())
                return;

            context.Assets.AddRange(new Asset[]
            {
                new Asset(Guid.NewGuid(), "NuConta", "NuBank", "Savings", (Currency)"BRL"),
                new Asset(Guid.NewGuid(), "Bova 11", "Clear", "Stocks", (Currency)"BRL"),
                new Asset(Guid.NewGuid(), "Occam FI Cambial", "Clear", "Hedge Funds", (Currency)"USD")
            });

            context.SaveChanges();
        }
    }

}
