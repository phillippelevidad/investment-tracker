using Core.Domain.Assets;
using Core.Functional;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Assets
{
    public interface IAssetsRepository
    {
        Task<Result> AddAsync(Asset asset, CancellationToken cancellationToken = default);

        Task<Asset?> FindAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result> RemoveAsync(Asset asset, CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(Asset asset, CancellationToken cancellationToken = default);
    }
}
