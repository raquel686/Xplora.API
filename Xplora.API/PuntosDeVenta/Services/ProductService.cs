using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Domain.Repositories;
using XploraAPI.PuntosDeVenta.Domain.Services;
using XploraAPI.PuntosDeVenta.Domain.Services.Communication;
using XploraAPI.Shared.Domain.Repositories;

namespace XploraAPI.PuntosDeVenta.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPDVRepository _pdvRepository;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IPDVRepository pdvRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _pdvRepository = pdvRepository;
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _productRepository.ListAsync();
    }

    public async Task<ProductResponse> SaveAsync(Product product,int pdvId)
    {
        var existingFilm = await _pdvRepository.FindByIdAsync(pdvId);

        if (existingFilm == null)
            return new ProductResponse("Invalid Film :'v");

        try
        {
            product.PDVId = pdvId;
            await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return new ProductResponse(product);
        }
        catch (Exception e)
        {
            return new ProductResponse($"Error: {e.Message}");
        }
    }

    public async Task<ProductResponse> UpdateAsync(int productId, int pdvId, Product pdv)
    {
        var existingProduct = await _productRepository.FindByIdAsync(productId);

        existingProduct.Stock = pdv.Stock;
        existingProduct.PCosto = pdv.PCosto;
        existingProduct.PRvtaMayor = pdv.PRvtaMayor;
        existingProduct.PDVId = pdvId;

        try
        {
            _productRepository.Update(existingProduct);
            await _unitOfWork.CompleteAsync();
            
            return new ProductResponse(existingProduct);

        }
        catch (Exception e)
        {
            return new ProductResponse($"Error: {e.Message}");
        }
    }

    public async Task<IEnumerable<Product>> ListBypdvIdAsync(int pdvId)
    {
        return await _productRepository.FindBypdvId(pdvId);
    }
}