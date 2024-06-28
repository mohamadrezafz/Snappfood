

using Snappfood.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Snappfood.Application.Interfaces;

public interface IApplicationDatabaseContext
{
    public DbSet<Product> Products { get; }
    public DbSet<User> Users { get; }
    public DbSet<Order> Orders { get; }

}
