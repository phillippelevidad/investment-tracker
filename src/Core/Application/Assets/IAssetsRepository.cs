using Core.Domain.Assets;
using Core.Functional;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Assets
{
    public interface IAssetsRepository
    {
        Task<Result> AddAsync(Asset asset, CancellationToken cancellationToken);

        Task<Asset?> FindAsync(Guid id, CancellationToken cancellationToken);

        Task<Result> UpdateAsync(Asset asset, CancellationToken cancellationToken);
    }
}
