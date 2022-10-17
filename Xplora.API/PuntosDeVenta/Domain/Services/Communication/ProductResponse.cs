using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.Shared.Domain.Services.Communication;

namespace XploraAPI.PuntosDeVenta.Domain.Services.Communication;

public class ProductResponse:BaseResponse<Product>
{
    public ProductResponse(string message) : base(message)
    {
    }

    public ProductResponse(Product resource) : base(resource)
    {
    }
}