using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Domain.Repositories;
using XploraAPI.PuntosDeVenta.Domain.Services;
using XploraAPI.PuntosDeVenta.Domain.Services.Communication;
using XploraAPI.Shared.Domain.Repositories;

namespace XploraAPI.PuntosDeVenta.Services;

public class PDVService:IPDVService
{
    private readonly IPDVRepository _pdvRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PDVService(IPDVRepository pdvRepository, IUnitOfWork unitOfWork)
    {
        _pdvRepository = pdvRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PDV>> ListAsync()
    {
        return await _pdvRepository.ListAsync();
    }

    public async Task<PDVResponse> GetByIdAsync(int pdvId)
    {
        var existingPdv = await _pdvRepository.FindByIdAsync(pdvId);
        if (existingPdv == null)
            return new PDVResponse("PDV not found");
        try
        {
            await _unitOfWork.CompleteAsync();
            return new PDVResponse(existingPdv);

        }
        catch (Exception e)
        {
            return new PDVResponse($"Error: {e.Message}");
        }
    }

    public async Task<PDVResponse> SaveAsync(PDV pdv)
    {
        try
        {
            await _pdvRepository.AddAsync(pdv);
            await _unitOfWork.CompleteAsync();
            return new PDVResponse(pdv);

        }
        catch (Exception e)
        {
            return new PDVResponse($"Error: {e.Message}");
        }
    }

    public async Task<PDVResponse> UpdateAsync(int pdvId, PDV pdv)
    {
        var existingPdv = await _pdvRepository.FindByIdAsync(pdvId);
        existingPdv.image = pdv.image;
        existingPdv.Code = pdv.Code;
        existingPdv.Direction = pdv.Direction;
        existingPdv.Name = pdv.Name;
        existingPdv.Latitude = pdv.Latitude;
        existingPdv.Longitude = pdv.Longitude;

        try
        {
            _pdvRepository.Update(existingPdv);
            await _unitOfWork.CompleteAsync();
            return new PDVResponse(existingPdv);

        }
        catch (Exception e)
        {
            return new PDVResponse($"Error: {e.Message}");
        }

    }
}