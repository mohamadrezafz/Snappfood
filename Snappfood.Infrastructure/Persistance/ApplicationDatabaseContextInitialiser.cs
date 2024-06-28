

using Microsoft.EntityFrameworkCore;

namespace Snappfood.Infrastructure.Persistance;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDatabaseContext _context;


    public ApplicationDbContextInitialiser(ApplicationDatabaseContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Asynchronously initializes the application database context by applying migrations if using SQL Server.
    /// </summary>
    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                // Apply pending migrations for SQL Server
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
