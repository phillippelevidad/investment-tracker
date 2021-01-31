using Core.Domain.Assets;
using Core.Application.Assets;
using Core.Functional;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Data
{
    public class AssetsRepository : IAssetsRepository
    {
        private readonly InvestmentDbContext db;

        public AssetsRepository(InvestmentDbContext db)
        {
            this.db = db;
        }

        public async Task<Result> AddAsync(Asset asset, CancellationToken cancellationToken)
        {
            await db.Assets.AddAsync(asset, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Asset?> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Assets.FindAsync(id);
        }

        public async Task<Result> UpdateAsync(Asset asset, CancellationToken cancellationToken)
        {
            db.Assets.Update(asset);
            await db.SaveChangesAsync();
            return Result.Success();
        }
    }
}
