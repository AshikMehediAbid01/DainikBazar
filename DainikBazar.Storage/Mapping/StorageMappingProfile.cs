using AutoMapper; 
using DomainModels = DainikBazar.Domain.Models;
using DainikBazar.Storage.Models;

namespace DainikBazar.Storage.Mapping;

public class StorageMappingProfile : Profile
{
    public StorageMappingProfile()
    {
        CreateMap<Product, DomainModels.Product>().ReverseMap();
        CreateMap<ReviewAndRating, DomainModels.ReviewAndRating>().ReverseMap();
    }
}
