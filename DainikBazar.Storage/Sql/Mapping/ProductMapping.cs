using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DainikBazar.Storage.Sql.Mapping;

public class ProductMapping
{
    public static void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.ProductId);

        builder.Property(p=>p.ProductGuid)
            .IsRequired()
            .HasMaxLength(36);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);


        builder.Property(p => p.Description);

        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.Quantity)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.HasMany(p => p.ReviewAndRatings)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);
    }
}
