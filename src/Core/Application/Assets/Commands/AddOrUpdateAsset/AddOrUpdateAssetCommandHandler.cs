using Core.Domain;
using Core.Domain.Assets;
using Core.Functional;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Assets.Commands.AddOrUpdateAsset
{
    public class AddOrUpdateAssetCommandHandler : IRequestHandler<AddOrUpdateAssetCommand, Result>
    {
        private readonly IAssetsRepository repository;

        public AddOrUpdateAssetCommandHandler(IAssetsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> Handle(AddOrUpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await repository.FindAsync(request.Id.Value, cancellationToken);

            return asset == null
                ? await AddAsync(request, cancellationToken)
                : await UpdateAsync(asset, request, cancellationToken);
        }

        private async Task<Result> AddAsync(AddOrUpdateAssetCommand request, CancellationToken cancellationToken)
        {
            Asset asset;

            try
            {
                asset = new Asset(
                    request.Id.Value, request.Name, request.Broker,
                    request.Category, new Currency(request.Currency));
            }
            catch (Exception ex)
            {
                return Result.Failure(ex);
            }

            return await repository.AddAsync(asset, cancellationToken);
        }

        private async Task<Result> UpdateAsync(Asset current, AddOrUpdateAssetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                current.Name = request.Name;
                current.Broker = request.Broker;
                current.Category = request.Category;
                current.Currency = new Currency(request.Currency);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex);
            }

            return await repository.UpdateAsync(current, cancellationToken);
        }
    }
}
