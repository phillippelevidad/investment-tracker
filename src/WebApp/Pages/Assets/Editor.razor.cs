using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp.Pages.Assets
{
    public partial class Editor
    {
        private Asset asset;
        private bool isVisible;

        [Parameter]
        public EventCallback SavedCallback { get; set; }

        [Parameter]
        public EventCallback RemovedCallback { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        public async Task LoadAssetAsync(Guid id)
        {
            var dto = await Http.GetFromJsonAsync<AssetDto>($"api/assets/{id}");
            asset = new Asset
            {
                Id = id,
                Name = dto.Name,
                Broker = dto.Broker,
                Category = dto.Category,
                Currency = dto.Currency
            };

            StateHasChanged();
        }

        public void Reset()
        {
            asset = new Asset();
        }

        public void Show()
        {
            isVisible = true;
            StateHasChanged();
        }

        public void Hide()
        {
            isVisible = false;
            StateHasChanged();
        }

        public async Task HandleValidSubmitAsync()
        {
            if (asset.Id == Guid.Empty)
            {
                asset.Id = Guid.NewGuid();
                await Http.PostAsJsonAsync("api/assets", asset);
            }
            else
            {
                await Http.PatchAsync($"api/assets/{asset.Id}", JsonContent.Create(asset));
            }

            await SavedCallback.InvokeAsync();

            isVisible = false;
            StateHasChanged();
        }

        public async Task RemoveAssetAsync(EventArgs e, Guid id)
        {
            await Http.DeleteAsync($"api/assets/{id}");
            await RemovedCallback.InvokeAsync();

            isVisible = false;
            StateHasChanged();
        }

        public class Asset
        {
            public Guid Id { get; set; }

            [Required]
            [StringLength(30, MinimumLength = 3)]
            public string Name { get; set; }

            [Required]
            [StringLength(30, MinimumLength = 3)]
            public string Broker { get; set; }

            [Required]
            [StringLength(30, MinimumLength = 3)]
            public string Category { get; set; }

            [Required]
            [StringLength(3, MinimumLength = 3)]
            public string Currency { get; set; }
        }
    }
}
