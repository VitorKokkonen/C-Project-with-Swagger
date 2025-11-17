// ProductApplication/Common/MappingProfile.cs
using AutoMapper;
using ProductApplication.DTOs;
using ProductDomain.Entities;
namespace ProductApplication.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}