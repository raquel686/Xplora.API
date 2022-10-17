using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XploraAPI.Shared.Extensions;
using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Domain.Services;
using XploraAPI.PuntosDeVenta.Resources;

namespace XploraAPI.PuntosDeVenta.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PDVsController:ControllerBase
{
    private readonly IPDVService _pdvService;
    private readonly IMapper _mapper;

    public PDVsController(IPDVService pdvService, IMapper mapper)
    {
        _pdvService = pdvService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PDVResource>> GetAllAsync()
    {
        var pdvs = await _pdvService.ListAsync();
        var resources = _mapper.Map<IEnumerable<PDV>, IEnumerable<PDVResource>>(pdvs);

        return resources;
    }

    [HttpGet("{id}")]
    public async Task<PDVResource> GetByIdAsync(int id)
    {
        var pdv = await _pdvService.GetByIdAsync(id);
        var pdvResource = _mapper.Map<PDV, PDVResource>(pdv.Resource);
        return pdvResource;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePDVResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var pdv = _mapper.Map<SavePDVResource, PDV>(resource);

        var result = await _pdvService.SaveAsync(pdv);

        if (!result.Success)
            return BadRequest(result.Message);

        var pdvResource = _mapper.Map<PDV, PDVResource>(result.Resource);

        return Ok(pdvResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SavePDVResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var pdv = _mapper.Map<SavePDVResource, PDV>(resource);
        var result = await _pdvService.UpdateAsync(id, pdv);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var filmResource = _mapper.Map<PDV, PDVResource>(result.Resource);

        return Ok(filmResource);
    }
}