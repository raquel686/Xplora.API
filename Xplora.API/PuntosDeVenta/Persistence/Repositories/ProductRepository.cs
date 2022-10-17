using Microsoft.EntityFrameworkCore;
using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Domain.Repositories;
using XploraAPI.Shared.Persistence.Contexts;
using XploraAPI.Shared.Persistence.Repositories;

namespace XploraAPI.PuntosDeVenta.Persistence.Repositories;

public class ProductRepository:BaseRepository,IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _context.Products
            .Include(p => p.PDV)
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<Product> FindByIdAsync(int productId)
    {
        return await _context.Products
            .Include(p => p.PDV)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<IEnumerable<Product>> FindBypdvId(int pdvId)
    {
        return await _context.Products
            .Where(p => p.PDVId == pdvId)
            .Include(p => p.PDV)
            .ToListAsync();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
}