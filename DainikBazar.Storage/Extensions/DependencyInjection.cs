using DainikBazar.Domain.Interfaces;
using DainikBazar.Storage.Data;
using DainikBazar.Storage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DainikBazar.Storage.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RepositoryRegistration(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddDbContext<AppDbContext>( options =>
            options.UseSqlServer(configuration.GetConnectionString( "DefaultConnection" ))
        );
        //services.AddScoped<IGenericRepository, GenericRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        return services;
    }

    public static async Task SeedStorageDataAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var seeder = new ProductAndUserSeeder();
        await seeder.SeedUsersAsync( dbContext );
        await seeder.SeedProductsAsync( dbContext );
    }
}
