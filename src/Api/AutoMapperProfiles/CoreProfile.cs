using AutoMapper;
using Core.Domain;
using Core.Domain.Transactions;
using Shared.Dtos;

namespace Api.AutoMapperProfiles
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateMap<Money, MoneyDto>()
                .ForMember(x => x.Currency, options => options.MapFrom(source => source.Currency.Value));

            CreateMap<Operation, string>()
                .ConvertUsing(operation => operation.Id);
        }
    }
}
