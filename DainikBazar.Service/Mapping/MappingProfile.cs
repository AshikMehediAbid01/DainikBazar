using AutoMapper;
using DainikBazar.Service.Models;
using Domains = DainikBazar.Domain.Models;

namespace DainikBazar.Service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<ReviewAndRating, Domains.ReviewAndRating>().ReverseMap();
        CreateMap<Product, Domains.Product>().ReverseMap();

        CreateMap<ReviewAndRating, Domains.ReviewAndRating>().ReverseMap();
        CreateMap<Cart, Domains.Cart>()
            .ReverseMap();

        CreateMap<CartItem, Domains.CartItem>()
            .ReverseMap();

        CreateMap<Order, Domains.Order>().ReverseMap();

    }
}
