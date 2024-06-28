
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snappfood.Domain.Entities;

namespace Snappfood.Infrastructure.Persistance.Configurations;

public class OrderConfiguration
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(p => p.Buyer)
            .WithMany(c => c.Orders)
            .HasForeignKey(p => p.BuyerId)
            .IsRequired();


        builder.HasOne(p => p.Product)
            .WithMany(c => c.Orders)
            .HasForeignKey(p => p.ProductId)
            .IsRequired();

    }
}
