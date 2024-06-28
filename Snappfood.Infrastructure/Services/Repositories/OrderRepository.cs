using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Domain.Entities;
using Snappfood.Infrastructure.Persistance;

namespace Snappfood.Infrastructure.Services.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDatabaseContext _context;

    public OrderRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}