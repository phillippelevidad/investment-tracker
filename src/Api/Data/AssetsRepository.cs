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

        public async Task<Result> AddAsync(Asset asset, CancellationToken cancellationToken = default)
        {
            await db.Assets.AddAsync(asset, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Asset?> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await db.Assets.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public async Task<Result> RemoveAsync(Asset asset, CancellationToken cancellationToken = default)
        {
            db.Assets.Remove(asset);
            await db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(Asset asset, CancellationToken cancellationToken = default)
        {
            db.Assets.Update(asset);
            await db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
