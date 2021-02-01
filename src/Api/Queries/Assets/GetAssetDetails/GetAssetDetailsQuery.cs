using MediatR;
using Shared.Dtos;
using System;

namespace Api.Queries.Assets.GetAssetDetails
{
    public class GetAssetDetailsQuery : IRequest<AssetDto?>
    {
        public GetAssetDetailsQuery(Guid assetId)
        {
            AssetId = assetId;
        }

        public Guid AssetId { get; }
    }
}
