using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp.Pages.Transactions
{
    public partial class Index
    {
        private TransactionDto[] transactions;

        public Editor Editor { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadTransactionsAsync();
        }

        private async Task LoadTransactionsAsync()
        {
            transactions = await Http.GetFromJsonAsync<TransactionDto[]>("api/transactions");
        }

        private async void OnEditorSavedOrRemovedAsync()
        {
            await LoadTransactionsAsync();
            StateHasChanged();
        }

        private async Task AddTransactionAsync()
        {
            await Editor.ResetAsync();
            Editor.Show();
        }

        private async Task EditTransactionAsync(EventArgs e, Guid id)
        {
            await Editor.LoadTransactionAsync(id);
            Editor.Show();
        }
    }
}
