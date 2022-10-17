using AutoMapper;
using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.PuntosDeVenta.Resources;

namespace XploraAPI.PuntosDeVenta.Mapping;

public class ModelToResourceProfile:Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<PDV, PDVResource>();
        CreateMap<Product, ProductResource>();
    }
}