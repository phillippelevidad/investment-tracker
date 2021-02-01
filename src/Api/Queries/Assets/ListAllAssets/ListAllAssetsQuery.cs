using MediatR;
using Shared.Dtos;
using System.Collections.Generic;

namespace Api.Queries.Assets.ListAllAssets
{
    public class ListAllAssetsQuery : IRequest<IReadOnlyCollection<AssetDto>>
    {
    }
}
