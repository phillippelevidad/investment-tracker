using AutoMapper;
using Core.Domain.Assets;
using Shared.Dtos;

namespace Api.AutoMapperProfiles
{
    public class AssetsProfile : Profile
    {
        public AssetsProfile()
        {
            CreateMap<Asset, AssetDto>()
                .ForMember(x => x.Currency, options => options.MapFrom(source => source.Currency.Value));
        }
    }
}
