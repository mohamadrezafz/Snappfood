using Microsoft.EntityFrameworkCore;
using Snappfood.Application.Interfaces;
using Snappfood.Domain.Entities;
using System.Reflection;


namespace Snappfood.Infrastructure.Persistance;

public class ApplicationDatabaseContext : DbContext , IApplicationDatabaseContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Apply entity configurations from the current assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
