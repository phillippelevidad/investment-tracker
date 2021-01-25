using Api.Core.Domain.Assets;
using AutoMapper;
using Shared.Dtos;

namespace Api.AutoMapper
{
    public class AssetsProfile : Profile
    {
        public AssetsProfile()
        {
            CreateMap<Asset, AssetDto>();
        }
    }
}
