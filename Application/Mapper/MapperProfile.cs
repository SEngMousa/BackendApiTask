
using Application.DTOs;
using  AutoMapper;
using Domain.Entities;

namespace Application.Mapper;



public class MapperProfile : Profile
{

    public MapperProfile()
    {
        CreateMap<DriverListDTO, Driver>().ReverseMap();
        CreateMap<CreateDriverDTO, Driver>().ReverseMap();
        CreateMap<UpdateDriverDTO, Driver>().ReverseMap();

    }
}