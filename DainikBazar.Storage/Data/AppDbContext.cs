using DainikBazar.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Storage.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
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


        modelBuilder.Entity<Cart>()
            .HasOne( c => c.User )
            .WithMany( u => u.Cart )
            .HasForeignKey( c => c.UserId );

        modelBuilder.Entity<Order>()
            .HasOne( o => o.User )
            .WithMany(u => u.Order )
            .HasForeignKey( o => o.UserId );

        modelBuilder.Entity<CartItem>()
            .HasOne( ci => ci.Cart )
            .WithMany( c=> c.CartItems )
            .HasForeignKey(ci => ci.CartId );

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Order)
            .WithOne(o => o.Product)
            .HasForeignKey<Product>(p => p.OrderId);

    }
}