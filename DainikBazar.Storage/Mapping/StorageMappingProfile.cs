using AutoMapper; 
using DomainModels = DainikBazar.Domain.Models;
using DainikBazar.Storage.Models;

namespace DainikBazar.Storage.Mapping;

public class StorageMappingProfile : Profile
{
    public StorageMappingProfile()
    {
        CreateMap<Product, DomainModels.Product>().ReverseMap();
        CreateMap<Cart, DomainModels.Cart>()
            //.ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems))
            .ReverseMap();
            //.ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));
        CreateMap<CartItem, DomainModels.CartItem>()
            .ReverseMap()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Cart, opt => opt.Ignore());
    }
}
