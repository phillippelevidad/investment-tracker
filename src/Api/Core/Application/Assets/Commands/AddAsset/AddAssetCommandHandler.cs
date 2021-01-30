using Api.Core.Domain;
using Api.Core.Domain.Assets;
using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Core.Application.Assets.Commands.AddAsset
{
    public class AddAssetCommandHandler : IRequestHandler<AddAssetCommand, Result>
    {
        private readonly IAssetsRepository repository;

        public AddAssetCommandHandler(IAssetsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> Handle(AddAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = new Asset(
                request.Id, request.Name, request.Broker, request.Category, new Currency(request.Currency));

            return await repository.AddAsync(asset, cancellationToken);
        }
    }
}
