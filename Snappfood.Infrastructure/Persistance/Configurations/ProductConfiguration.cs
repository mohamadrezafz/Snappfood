

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snappfood.Domain.Entities;
using System.Reflection;
using System.Reflection.Emit;

namespace Snappfood.Infrastructure.Persistance.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(b => b.Title)
            .HasMaxLength(40);

        builder.HasIndex(c => c.Title).IsUnique();
        builder.Property(e => e.Price)
            .HasColumnType("decimal(18,2)");
        builder.Property(e => e.Discount)
            .HasColumnType("decimal(18,2)");

    }
}
