using Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Queries.Assets.AssetExists
{
    public class AssetExistsQueryHandler : IRequestHandler<AssetExistsQuery, bool>
    {
        private readonly InvestmentDbContext db;

        public AssetExistsQueryHandler(InvestmentDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(AssetExistsQuery request, CancellationToken cancellationToken)
        {
            return await db.Assets.AnyAsync(a => a.Id == request.AssetId, cancellationToken);
        }
    }
}
