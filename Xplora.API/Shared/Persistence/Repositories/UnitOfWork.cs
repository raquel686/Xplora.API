using XploraAPI.Shared.Domain.Repositories;
using XploraAPI.Shared.Persistence.Contexts;

namespace XploraAPI.Shared.Persistence.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}