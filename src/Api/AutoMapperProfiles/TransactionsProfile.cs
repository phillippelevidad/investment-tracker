using AutoMapper;
using Core.Domain.Transactions;
using Shared.Dtos;

namespace Api.AutoMapperProfiles
{
    public class TransactionsProfile : Profile
    {
        public TransactionsProfile()
        {
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
