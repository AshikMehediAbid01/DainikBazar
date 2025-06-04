using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DainikBazar.Storage.Sql.Mapping;

public class OrderMapping
{
    public static void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.UserId).IsRequired();

        builder.Property(o => o.SubtotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.DeliveryCharge).HasColumnType("decimal(18,2)").IsRequired();

        builder.Property(o => o.OrderStatus)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.OrderDate).IsRequired();

        builder.Property(o => o.PaymentMethod)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.OrderHistory).IsRequired().HasMaxLength(500);

        builder.Property(o => o.ReceiverAddress)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.ReceiverPhone).IsRequired().HasMaxLength(15);

        builder.Property(o => o.ReceiverName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(o => o.Cart)
            .WithOne(c => c.Order)
            .HasForeignKey<Order>(c => c.CartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p=>p.Product)
            .WithOne(o=>o.Order)
            .HasForeignKey<Order>(p => p.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
