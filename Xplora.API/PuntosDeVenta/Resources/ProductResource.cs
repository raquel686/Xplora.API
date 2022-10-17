using XploraAPI.PuntosDeVenta.Domain.Models;

namespace XploraAPI.PuntosDeVenta.Resources;

public class ProductResource
{
    public int Id { get; set; }

    public float PCosto { get; set; }
    
    public string Name { get; set; }

    public float PRvtaMayor { get; set; }

    public int Stock { get; set; }

    public PDVResource PDV { get; set; }
}