using AutoMapper; 
using DainikBazar.Domain.Models;
using StorageModels = DainikBazar.Storage.Models;

namespace DainikBazar.Storage.Mapping;

public class StorageMappingProfile : Profile
{
    public StorageMappingProfile()
    {
        CreateMap<Product, StorageModels.Product>().ReverseMap();
    }
}
