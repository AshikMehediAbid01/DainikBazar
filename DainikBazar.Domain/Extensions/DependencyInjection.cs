
using DainikBazar.Domain.Managers.Implementations;
using DainikBazar.Domain.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DainikBazar.Domain.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection ManagerRegistration(this IServiceCollection services)
    {
        services.AddScoped<IProductManager, ProductManager>();  
    //    services.AddScoped<ICartManager, CartManager>();
      //  services.AddScoped<IOrderManager, OrderManager>();
        services.AddScoped<IReviewManager, ReviewManager>();
        
        
        return services;
    }
}
