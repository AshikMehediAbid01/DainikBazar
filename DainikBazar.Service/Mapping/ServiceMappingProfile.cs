using AutoMapper;
using Services = DainikBazar.Service.Models;
using Domains = DainikBazar.Domain.Models;

namespace DainikBazar.Service.Mapping;

public class ServiceMappingProfile : Profile
{
    public ServiceMappingProfile()
    {
        CreateMap<Services.ReviewAndRating,Domains.ReviewAndRating>().ReverseMap();
        CreateMap<Services.Cart, Domains.Cart>()
            .ReverseMap();

        CreateMap<Services.CartItem, Domains.CartItem>()
            .ReverseMap();

        CreateMap<Services.Order, Domains.Order>()
            .ReverseMap();
    }
}
