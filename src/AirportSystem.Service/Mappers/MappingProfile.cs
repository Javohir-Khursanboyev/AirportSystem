using AutoMapper;
using AirportSystem.Service.DTOs.Assets;
using AirportSystem.Domain.Entities.Assets;

namespace AirportSystem.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Asset, AssetCreateModel>().ReverseMap();
        CreateMap<Asset,AssetViewModel>().ReverseMap();
    }
}
