
using Snappfood.Domain.Entities;

namespace Snappfood.Application.Interfaces.Repositories;
public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<List<User>> GetAllAsync();
}