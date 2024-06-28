
using Snappfood.Domain.Entities;

namespace Snappfood.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<Product> GetByTitleAsync(string title);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
}
