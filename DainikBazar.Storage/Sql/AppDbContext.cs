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

        modelBuilder.Entity<Product>(ProductMapping.Configure);

        modelBuilder.Entity<Order>(OrderMapping.Configure);

        modelBuilder.Entity<User>(UserMapping.Configure);

        modelBuilder.Entity<Cart>(CartMapping.Configure);


  /*      modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(u => u.Cart)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
*/


        /*        modelBuilder.Entity<Order>()
                    .HasOne(o => o.User)
                    .WithMany(u => u.Order)
                    .HasForeignKey(o => o.UserId).HasForeignKey(c => c.CartId);
        */

        modelBuilder.Entity<Cart>();

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Restrict);

        /*        modelBuilder.Entity<Order>()
                    .HasOne(o => o.Product)
                    .WithOne(p => p.Order)
                    .HasForeignKey<Order>(o => o.ProductId);
        */


    }
}