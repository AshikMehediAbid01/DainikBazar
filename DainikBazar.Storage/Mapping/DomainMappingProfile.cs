using AutoMapper;
using Domains = DainikBazar.Domain.Models;
using Storages = DainikBazar.Storage.Models;

namespace DainikBazar.Domain.Mapping;

public class DomainMappingProfile : Profile
{
    public DomainMappingProfile()
    {
        CreateMap<Storages.Cart, Domains.Cart>()
            .ReverseMap();

        CreateMap<Storages.CartItem, Domains.CartItem>()
            .ForMember( dest => dest.ProductName, opt => opt.MapFrom( src => src.Product.Name ) )
            .ReverseMap();

        CreateMap<Storages.Order, Domains.Order>()
        .ForMember( dest => dest.CartItems, opt => opt.MapFrom( src => src.Cart.CartItems ) )
        .ReverseMap();
        
    }
}
