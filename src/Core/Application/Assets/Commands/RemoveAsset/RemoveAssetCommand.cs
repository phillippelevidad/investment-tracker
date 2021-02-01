using Core.Functional;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Assets.Commands.RemoveAsset
{
    public class RemoveAssetCommand : IRequest<Result>
    {
        public RemoveAssetCommand(Guid assetId)
        {
            AssetId = assetId;
        }

        public Guid AssetId { get; }
    }

    public class RemoveAssetCommandHandler : IRequestHandler<RemoveAssetCommand, Result>
    {
        private readonly IAssetsRepository repository;

        public RemoveAssetCommandHandler(IAssetsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> Handle(RemoveAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await repository.FindAsync(request.AssetId, cancellationToken);
            if (asset == null) return Result.Failure($"Asset '{request.AssetId}' does not exist.");

            return await repository.RemoveAsync(asset, cancellationToken);
        }
    }
}
