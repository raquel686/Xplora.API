using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.Shared.Domain.Services.Communication;

namespace XploraAPI.PuntosDeVenta.Domain.Services.Communication;

public class PDVResponse:BaseResponse<PDV>
{
    public PDVResponse(string message) : base(message)
    {
    }

    public PDVResponse(PDV resource) : base(resource)
    {
    }
}