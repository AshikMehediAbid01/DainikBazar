using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DainikBazar.Storage.Sql.Mapping;

public class CartMapping
{
    public static void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasOne(c => c.User)
            .WithMany(u => u.Cart)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Order)
            .WithOne(o => o.Cart)
            .HasForeignKey<Order>(o => o.CartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.CartItems)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);



    }
}
