using Api.Core.Application.Assets;
using Api.Core.Domain.Assets;
using CSharpFunctionalExtensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Data
{
    public class AssetsRepository : IAssetsRepository
    {
        public Task<Result> AddAsync(Asset asset, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
