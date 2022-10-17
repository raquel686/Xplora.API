using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Domain.Services.Communication;

namespace XploraAPI.PuntosDeVenta.Domain.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> ListAsync();

    Task<ProductResponse> SaveAsync(Product product,int pdvId);
    Task<ProductResponse> UpdateAsync(int productId, int pdvId, Product pdv);
    Task<IEnumerable<Product>> ListBypdvIdAsync(int pdvId);
}