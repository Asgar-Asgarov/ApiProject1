using AutoMapper;
using ApiCrud.Models;
using ApiCrud.Dtos;

namespace ApiCrud.Data.Configurations;

public class MapperConfig : Profile
{
   public MapperConfig()
    {
        CreateMap<Product,ProductReturnDto>();
        CreateMap<Product,ProductCreateDto>().ReverseMap();
    }
}