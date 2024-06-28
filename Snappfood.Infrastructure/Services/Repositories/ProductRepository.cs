
using Microsoft.EntityFrameworkCore;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Domain.Entities;
using Snappfood.Infrastructure.Persistance;

namespace Snappfood.Infrastructure.Services.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDatabaseContext _context;

    public ProductRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(int id)=>
         await _context.Products.FindAsync(id);

    public async Task<Product> GetByTitleAsync(string title)=>
         await _context.Products.FirstOrDefaultAsync(p => p.Title == title);
    

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}