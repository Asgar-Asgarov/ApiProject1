namespace ApiCrud.Dtos;

public class ProductCreateDto
{
    public string? Name { get; set; }
    public double Price { get; set; }
    public double DiscountPrice { get; set; }
    public bool IsActive { get; set; }
}