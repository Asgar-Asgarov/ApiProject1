using AutoMapper;
using ApiCrud.Models;
using ApiCrud.Dtos;

namespace ApiCrud.Data.Configurations;

public class MapperConfig : Profile
{
    protected MapperConfig()
    {
        CreateMap<Product,ProductReturnDto>();
    }
}