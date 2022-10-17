using Microsoft.EntityFrameworkCore;
using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Domain.Repositories;
using XploraAPI.Shared.Persistence.Contexts;
using XploraAPI.Shared.Persistence.Repositories;

namespace XploraAPI.PuntosDeVenta.Persistence.Repositories;

public class PDVRepository:BaseRepository,IPDVRepository
{
    public PDVRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PDV>> ListAsync()
    {
        return await _context.Pdvs
            .ToListAsync();
    }

    public async Task AddAsync(PDV pdv)
    {
        await _context.Pdvs.AddAsync(pdv);
    }

    public async Task<PDV> FindByIdAsync(int pdvId)
    {
        return await _context.Pdvs
            .FirstOrDefaultAsync(p => p.Id == pdvId);
    }

    public void Update(PDV pdv)
    {
        _context.Pdvs.Update(pdv);
    }
}