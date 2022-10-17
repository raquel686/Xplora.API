using XploraAPI.PuntosDeVenta.Domain.Models;

namespace XploraAPI.PuntosDeVenta.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ListAsync();
    Task AddAsync(Product product);
    Task<Product> FindByIdAsync(int productId);
    Task<IEnumerable<Product>> FindBypdvId(int pdvId);
    void Update(Product product);
}