namespace XploraAPI.PuntosDeVenta.Domain.Models;

public class Product
{
    public int Id { get; set; }

    public float PCosto { get; set; }
    
    public string Name { get; set; }

    public float PRvtaMayor { get; set; }

    public int Stock { get; set; }
    
    public int PDVId { get; set; }
    public PDV PDV { get; set; }

}