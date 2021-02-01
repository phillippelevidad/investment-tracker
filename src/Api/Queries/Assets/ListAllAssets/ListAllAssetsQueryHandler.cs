using Api.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Queries.Assets.ListAllAssets
{
    public class ListAllAssetsQueryHandler : IRequestHandler<ListAllAssetsQuery, IReadOnlyCollection<AssetDto>>
    {
        private readonly InvestmentDbContext db;
        private readonly IMapper mapper;

        public ListAllAssetsQueryHandler(InvestmentDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<AssetDto>> Handle(ListAllAssetsQuery request, CancellationToken cancellationToken)
        {
            var assets = await db.Assets.ToListAsync(cancellationToken);
            return assets.Select(asset => mapper.Map<AssetDto>(asset)).ToList().AsReadOnly();
        }
    }
}
