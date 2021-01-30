using AutoMapper;
using Shared.Dtos;
using System.Transactions;

namespace Api.AutoMapper
{
    public class TransactionsProfile : Profile
    {
        public TransactionsProfile()
        {
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
