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
        CreateMap<Order, DomainModels.Order>()
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Cart != null? src.Cart.CartItems: null))
            .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Cart != null ? src.Cart.Id: (int?)null))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product != null? src.Product.ProductId: (int?)null))
            .ReverseMap()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Cart, opt => opt.Ignore())
            .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
        CreateMap<ReviewAndRating, DomainModels.ReviewAndRating>().ReverseMap();
    }
}
