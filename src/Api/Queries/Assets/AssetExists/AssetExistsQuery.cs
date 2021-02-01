using MediatR;
using System;

namespace Api.Queries.Assets.AssetExists
{
    public class AssetExistsQuery : IRequest<bool>
    {
        public AssetExistsQuery(Guid assetId)
        {
            AssetId = assetId;
        }

        public Guid AssetId { get; }
    }
}
