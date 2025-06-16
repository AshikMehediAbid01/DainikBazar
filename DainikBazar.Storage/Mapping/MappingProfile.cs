using AutoMapper; 
using DomainModels = DainikBazar.Domain.Models;
using DainikBazar.Storage.Models;

namespace DainikBazar.Storage.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, DomainModels.Product>().ReverseMap();
        CreateMap<ReviewAndRating, DomainModels.ReviewAndRating>().ReverseMap();
    }
}
