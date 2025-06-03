using AutoMapper;
using DainikBazar.Service.Models;
using Domains = DainikBazar.Domain.Models;

namespace DainikBazar.Service.Mapping;

public class ServiceMappingProfile : Profile
{
    public ServiceMappingProfile()
    {

        CreateMap<ReviewAndRating, Domains.ReviewAndRating>().ReverseMap();
        CreateMap<Product, Domains.Product>().ReverseMap();

        CreateMap<ReviewAndRating,Domains.ReviewAndRating>().ReverseMap();
        /*        CreateMap<Cart, Domains.Cart>()
                    .ReverseMap();

                CreateMap<CartItem, Domains.CartItem>()
                    .ReverseMap();

                CreateMap<Order, Domains.Order>().ReverseMap();
        */
    }
}
