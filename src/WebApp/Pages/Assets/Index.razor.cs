using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp.Pages.Assets
{
    public partial class Index
    {
        private AssetDto[] assets;

        public Editor Editor { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAssetsAsync();
        }

        private async Task LoadAssetsAsync()
        {
            assets = await Http.GetFromJsonAsync<AssetDto[]>("api/assets");
        }

        private async void OnEditorSavedOrRemovedAsync()
        {
            await LoadAssetsAsync();
            StateHasChanged();
        }

        private void AddAsset()
        {
            Editor.Reset();
            Editor.Show();
        }

        private async Task EditAssetAsync(EventArgs e, Guid id)
        {
            await Editor.LoadAssetAsync(id);
            Editor.Show();
        }
    }
}
