namespace ApiCrud.Dtos;

public class ProductListDto
{
public int TotalCount { get; set; }
public List<ProductListItemDto>? items { get; set; }
}