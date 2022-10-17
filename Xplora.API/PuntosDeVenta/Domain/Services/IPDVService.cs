using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Domain.Services.Communication;

namespace XploraAPI.PuntosDeVenta.Domain.Services;

public interface IPDVService
{
    Task<IEnumerable<PDV>> ListAsync();
    Task<PDVResponse> GetByIdAsync(int pdvId);
    Task<PDVResponse> SaveAsync(PDV pdv);
    Task<PDVResponse> UpdateAsync(int pdvId, PDV pdv);
}