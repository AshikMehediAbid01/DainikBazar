using DainikBazar.Domain.Settings;
using DainikBazar.Domain.Managers.Implementations;
using DainikBazar.Domain.Managers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DainikBazar.Domain.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection ManagerRegistration(
        this IServiceCollection services,
        IConfiguration config
        )
    {
        services.AddScoped<IProductManager, ProductManager>();  
        services.AddScoped<ICartManager, CartManager>();
        services.AddScoped<IOrderManager, OrderManager>();
        services.AddScoped<IReviewManager, ReviewManager>();

        services.Configure<Auth0Settings>(config.GetSection("Auth0"));

        services.AddHttpClient<IAuth0Manager, Auth0Manager>();

        return services;
    }
}
