using Snappfood.Domain.Entities;

namespace Snappfood.Application.Interfaces.Repositories;
public interface IOrderRepository
{
    Task AddAsync(Order order);
}
