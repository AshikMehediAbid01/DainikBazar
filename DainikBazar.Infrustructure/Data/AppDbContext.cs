using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cart>()
            .HasOne( c => c.User )
            .WithOne( u => u.Cart )
            .HasForeignKey<Cart>( c => c.UserId );

        modelBuilder.Entity<Order>()
            .HasOne( o => o.User )
            .WithMany(u => u.Order )
            .HasForeignKey( o => o.UserId );

        modelBuilder.Entity<CartItem>()
            .HasOne( ci => ci.Cart )
            .WithMany( c=> c.CartItems )
            .HasForeignKey(ci => ci.CartId );

        modelBuilder.Entity<OrderItem>()
            .HasOne( oi => oi.Order )
            .WithMany( o => o.OrderItems )
            .HasForeignKey( oi => oi.OrderId );

    }
}
