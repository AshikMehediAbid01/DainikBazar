using DainikBazar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext( DbContextOptions options ) : base( options )
    { 
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

}
