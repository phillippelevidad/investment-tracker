using Api.Data;
using AutoMapper;
using MediatR;
using Shared.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Queries.Assets.GetAssetDetails
{
    public class GetAssetDetailsQueryHandler : IRequestHandler<GetAssetDetailsQuery, AssetDto?>
    {
        private readonly InvestmentDbContext db;
        private readonly IMapper mapper;

        public GetAssetDetailsQueryHandler(InvestmentDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<AssetDto?> Handle(GetAssetDetailsQuery request, CancellationToken cancellationToken)
        {
            var asset = await db.Assets.FindAsync(request.AssetId);
            if (asset == null) return null;
            return mapper.Map<AssetDto>(asset);
        }
    }
}
