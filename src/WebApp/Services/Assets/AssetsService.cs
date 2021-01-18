using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Services.Assets.Models;

namespace WebApp.Services.Assets
{
    public class AssetsService : IAssetsService
    {
        private readonly HttpClient http;

        public AssetsService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<Result<Asset[]>> ListAllAssync(CancellationToken cancellationToken = default)
        {
            var response = await http.GetAsync("api/assets", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var assets = await response.Content.ReadFromJsonAsync<Asset[]>(null, cancellationToken);
                return Result.Success(assets);
            }

            return Result.Failure<Asset[]>(response.ReasonPhrase);
        }
    }
}
