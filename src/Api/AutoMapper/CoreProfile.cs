using Api.Core.Domain;
using AutoMapper;
using Shared.Dtos;

namespace Api.AutoMapper
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateMap<Money, MoneyDto>()
                .ForMember(x => x.Currency, options => options.MapFrom(source => source.Currency.Value));
        }
    }
}
