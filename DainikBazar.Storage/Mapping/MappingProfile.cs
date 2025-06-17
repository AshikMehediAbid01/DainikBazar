using AutoMapper; 
using DomainModels = DainikBazar.Domain.Models;
using DainikBazar.Storage.Models;

namespace DainikBazar.Storage.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, DomainModels.Product>().ReverseMap();
        CreateMap<Cart, DomainModels.Cart>()
            .ReverseMap();
        CreateMap<CartItem, DomainModels.CartItem>()
            .ReverseMap()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Cart, opt => opt.Ignore());
        CreateMap<Order, DomainModels.Order>()
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Cart != null ? src.Cart.CartItems : null))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ?  src.Product.Name : null))
            .ReverseMap()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Cart, opt => opt.Ignore());
        CreateMap<ReviewAndRating, DomainModels.ReviewAndRating>().ReverseMap();
    }
}
