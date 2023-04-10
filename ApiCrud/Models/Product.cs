namespace ApiCrud.Models;

public class Product:BaseEntity
{
  
    public string? Name { get; set; }
    public double Price { get; set; }
    public double DiscountPrice { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }


}