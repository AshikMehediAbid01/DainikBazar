using AutoMapper;
using DainikBazar.Service.Models;
using DomainModels = DainikBazar.Domain.Models;

namespace DainikBazar.Service.Mapping;

public class ServiceMappingProfile :Profile
{
    public ServiceMappingProfile()
    {
        CreateMap<ReviewAndRating,DomainModels.ReviewAndRating>().ReverseMap();
    }
}
