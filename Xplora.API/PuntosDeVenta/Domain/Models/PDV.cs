namespace XploraAPI.PuntosDeVenta.Domain.Models;

public class PDV
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Code { get; set; }
    
    public string Direction { get; set; }
    
    public float Latitude { get; set; }
    
    public float Longitude { get; set; }
    
    public string image { get; set; }
    
    public IList<Product> products { get; set; }
    
}