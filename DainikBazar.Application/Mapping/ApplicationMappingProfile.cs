using AutoMapper;
using DainikBazar.Application.Common.DTOs;
using DainikBazar.Domain.Entities;


namespace DainikBazar.Application.Mapping;

public class ApplicationMappingProfile: Profile
{
    public ApplicationMappingProfile() 
    {
        CreateMap<Cart,CartDto>();
        CreateMap<CartDto, Cart>();
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        CreateMap<CartItemDto, CartItem>();
        CreateMap<Order,OrderDto>();
        CreateMap<OrderDto, Order>();


        //===================
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<ReviewAndRating, ReviewDto>().ReverseMap();
    }
}
