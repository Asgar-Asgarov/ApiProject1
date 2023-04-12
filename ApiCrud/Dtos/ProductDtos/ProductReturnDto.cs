namespace ApiCrud.Dtos;

public class ProductReturnDto
{
    public string? Name { get; set; }
    public string? ImageUrl    { get; set; }
    public double Price { get; set; }
    public double DiscountPrice { get; set; }
      public DateTime CreatedTime { get; set; }
      public DateTime UpdatedTime { get; set; }
}