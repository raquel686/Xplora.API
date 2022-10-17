using AutoMapper;
using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Resources;

namespace XploraAPI.PuntosDeVenta.Mapping;

public class ResourceToModelProfile:Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveProductResource, Product>();
        CreateMap<SavePDVResource, PDV>();
    }
    
}