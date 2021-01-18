using Api.Core.Domain.Assets;
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Core.Application.Assets
{
    public interface IAssetsRepository
    {
        Task<Result> AddAsync(Asset asset, CancellationToken token);
    }
}
