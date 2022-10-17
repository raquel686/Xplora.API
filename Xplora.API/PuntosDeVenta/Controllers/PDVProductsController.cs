using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XploraAPI.Shared.Extensions;
using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Domain.Services;
using XploraAPI.PuntosDeVenta.Resources;

namespace XploraAPI.PuntosDeVenta.Controllers;
[ApiController]
[Route("api/v1/pdvs/{pdvId}/products")]

public class PDVProductsController:ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public PDVProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductResource>> GetAllProduct(int pdvId)
    {
        var products = await _productService.ListBypdvIdAsync(pdvId);
        var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct(int pdvId, [FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var comment = _mapper.Map<SaveProductResource, Product>(resource);

        var result = await _productService.SaveAsync(comment,pdvId);

        if (!result.Success)
            return BadRequest(result.Message);

        var commentResource = _mapper.Map<Product, ProductResource>(result.Resource);
        return Ok(commentResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, int pdvId, [FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var pdv = _mapper.Map<SaveProductResource, Product>(resource);
        var result = await _productService.UpdateAsync(id,pdvId, pdv);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var filmResource = _mapper.Map<Product, ProductResource>(result.Resource);

        return Ok(filmResource);
    }

}