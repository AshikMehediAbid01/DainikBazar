using DainikBazar.Storage.Models;
using DainikBazar.Storage.Sql.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Storage.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ReviewAndRating> ReviewAndRatings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ReviewAndRating>()
            .HasOne(b => b.Product)
            .WithMany(a => a.ReviewAndRatings)
            .HasForeignKey(b => b.ProductId);

        modelBuilder.Entity<Cart>();

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId);

        modelBuilder.Entity<Order>(OrderMapping.Configure);

        modelBuilder.Entity<User>(UserMapping.Configure);


    }
}