using DainikBazar.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DainikBazar.Storage.Sql.Mapping;

public class OrderMapping
{
    public static void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.UserId)
            .IsRequired();
        builder.Property(o => o.SubtotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.Property(o => o.DeliveryCharge)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.Property(o => o.OrderStatus)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(o => o.OrderDate)
            .IsRequired();
        builder.Property(o => o.PaymentMethod)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(o => o.OrderHistory)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(o => o.ReceiverAddress)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(o => o.ReceiverPhone)
            .IsRequired()
            .HasMaxLength(15);
        builder.Property(o => o.ReceiverName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(o => o.Cart).WithOne(c => c.Order).HasForeignKey<Order>(o => o.CartId);
        builder.HasOne(o => o.Product).WithOne(p => p.Order).HasForeignKey<Order>(o => o.ProductId);
    }
}
