using Microsoft.EntityFrameworkCore;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Domain.Entities;
using Snappfood.Infrastructure.Persistance;

namespace Snappfood.Infrastructure.Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDatabaseContext _context;

    public UserRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(int id)=>
         await _context.Users.FindAsync(id);



    public async Task<List<User>> GetAllAsync()=>
         await _context.Users .ToListAsync();


}
