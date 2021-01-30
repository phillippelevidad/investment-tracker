using Core.Application.Assets.Commands.AddOrUpdateAsset;
using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp.Pages.Assets
{
    public partial class Editor
    {
        protected AddOrUpdateAssetCommand Asset { get; set; }
        private bool IsVisible { get; set; }
        protected bool IsUpdate { get; set; }

        [Parameter] public EventCallback SavedCallback { get; set; }
        [Parameter] public EventCallback RemovedCallback { get; set; }
        [Inject] public HttpClient Http { get; set; }

        public async Task LoadAssetAsync(Guid id)
        {
            var dto = await Http.GetFromJsonAsync<AssetDto>($"api/assets/{id}");
            Asset = new AddOrUpdateAssetCommand
            {
                Id = id,
                Name = dto.Name,
                Broker = dto.Broker,
                Category = dto.Category,
                Currency = dto.Currency
            };

            IsUpdate = true;
            StateHasChanged();
        }

        public void Reset()
        {
            IsUpdate = false;
            Asset = new AddOrUpdateAssetCommand
            {
                Id = Guid.NewGuid()
            };
        }

        public void Show()
        {
            IsVisible = true;
            StateHasChanged();
        }

        public void Hide()
        {
            IsVisible = false;
            StateHasChanged();
        }

        public async Task HandleValidSubmitAsync()
        {
            if (IsUpdate)
            {
                await Http.PatchAsync($"api/assets/{Asset.Id}", JsonContent.Create(Asset));
            }
            else
            {
                await Http.PostAsJsonAsync("api/assets", Asset);
            }

            await SavedCallback.InvokeAsync();

            IsVisible = false;
            StateHasChanged();
        }

        public async Task RemoveAssetAsync(EventArgs e, Guid id)
        {
            await Http.DeleteAsync($"api/assets/{id}");
            await RemovedCallback.InvokeAsync();

            IsVisible = false;
            StateHasChanged();
        }
    }
}
