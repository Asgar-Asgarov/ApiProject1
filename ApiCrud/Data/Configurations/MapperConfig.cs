using AutoMapper;
using ApiCrud.Models;
using ApiCrud.Dtos;

namespace ApiCrud.Data.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Product, ProductReturnDto>();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();


        CreateMap<Category, CategoryReturnDto>();
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap();

    }
}