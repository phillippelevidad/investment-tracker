using System.Threading;
using System.Threading.Tasks;
using WebApp.Services.Assets.Models;

namespace WebApp.Services.Assets
{
    public interface IAssetsService
    {
        Task<Result<Asset[]>> ListAllAssync(CancellationToken cancellationToken = default);
    }
}
