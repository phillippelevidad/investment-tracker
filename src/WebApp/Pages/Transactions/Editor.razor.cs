using Core.Application.Transactions.Commands.AddTransaction;
using Core.Domain.Transactions;
using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp.Pages.Transactions
{
    public partial class Editor
    {
        protected AddTransactionCommand Transaction { get; set; }
        protected List<AssetDto> Assets { get; set; }
        protected decimal Volume
        {
            get => (decimal)(Transaction.Quantity ?? 0d) * (Transaction.UnitPrice ?? 0m);
            set
            {
                if (Transaction.Quantity == 0) Transaction.Quantity = 1;
                Transaction.UnitPrice = value / (decimal)Transaction.Quantity;
            }
        }

        protected bool IsVisible { get; set; }
        protected bool IsNew { get; set; }

        [Parameter] public EventCallback SavedCallback { get; set; }
        [Parameter] public EventCallback RemovedCallback { get; set; }
        [Inject] public HttpClient Http { get; set; }

        public async Task LoadTransactionAsync(Guid id)
        {
            var dto = await Http.GetFromJsonAsync<TransactionDto>($"api/transactions/{id}");
            Transaction = new AddTransactionCommand
            {
                Id = id,
                AssetId = dto.Asset.Id,
                Operation = dto.Operation,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice.Value,
                Currency = dto.UnitPrice.Currency,
                DateTime = dto.DateTime
            };

            IsNew = false;
            StateHasChanged();
        }

        public async Task ResetAsync()
        {
            Assets = await Http.GetFromJsonAsync<List<AssetDto>>("api/assets");
            Transaction = new AddTransactionCommand
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                AssetId = Assets.FirstOrDefault()?.Id,
                Operation = Operation.ListAllIds().First()
            };

            IsNew = true;
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
            await Http.PostAsJsonAsync("api/transactions", Transaction);

            await SavedCallback.InvokeAsync();

            IsVisible = false;
            StateHasChanged();
        }

        public async Task RemoveAssetAsync(EventArgs e, Guid id)
        {
            await Http.DeleteAsync($"api/transactions/{id}");
            await RemovedCallback.InvokeAsync();

            IsVisible = false;
            StateHasChanged();
        }
    }
}
