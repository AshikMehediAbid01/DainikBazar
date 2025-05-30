using DainikBazar.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Storage.Data;

public class ProductAndUserSeeder
{
    public async Task SeedProductsAsync(AppDbContext dbContext)
    {
        if (await dbContext.Products.AnyAsync())
        {
            return;
        }
        var products = new List<Product>();
        products.Add( new Product {
            Name = "HP Monitor 17 Inch",
            Price = 10000,
            CreatedAt = DateTime.Now,
            ImageUrl = "/",
            Description = "HP Monitor 17 Inch Descriptions",
            Quantity = 19,
        } );
        products.Add( new Product
        {
            Name = "HP Probook Laptop i5 32GB",
            Price = 160000,
            CreatedAt = DateTime.Now,
            ImageUrl = "/",
            Description = "HP Probook Laptop i5 32GB Descriptions",
            Quantity = 43,
        } );
        products.Add( new Product
        {
            Name = "Samsung S70 Mobile",
            Price = 70000,
            CreatedAt = DateTime.Now,
            ImageUrl = "/",
            Description = "Samsung S70 Mobile Descriptions",
            Quantity = 32,
        } );
        await dbContext.Products.AddRangeAsync( products );
        await dbContext.SaveChangesAsync();
    }

    public async Task SeedUsersAsync(AppDbContext dbContext)
    {
        if (await dbContext.Users.AnyAsync())
        {
            return;
        }
        var users = new List<User>();
        users.Add( new User { Id = Guid.NewGuid().ToString(), Name = "Abid" } );
        users.Add( new User { Id = Guid.NewGuid().ToString(), Name = "Gates" } );
        users.Add( new User { Id = Guid.NewGuid().ToString(), Name = "Eusha" } );
        await dbContext.Users.AddRangeAsync( users );
        await dbContext.SaveChangesAsync();
    }
}
