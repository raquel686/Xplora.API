using XploraAPI.PuntosDeVenta.Domain.Models;

namespace XploraAPI.PuntosDeVenta.Domain.Repositories;

public interface IPDVRepository
{
    Task<IEnumerable<PDV>> ListAsync();
    Task AddAsync(PDV pdv);
    Task<PDV> FindByIdAsync(int pdvId);
    void Update(PDV pdv);
}