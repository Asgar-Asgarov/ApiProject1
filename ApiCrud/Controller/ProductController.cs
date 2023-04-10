using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiCrud.Models;
using ApiCrud.Data.DAL;
using ApiCrud.Dtos;

namespace ApiCrud.Controllers;

public class ProductController : BaseController
{
    private readonly AppDbContext _appDbContext;

    public ProductController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _appDbContext.Products.ToList();
        return StatusCode(200, products);
    }


    [HttpGet("{id}")]
    public IActionResult GetOne(int id)
    {
        var product = _appDbContext.Products
        .Where(p => !p.IsDeleted)
        .FirstOrDefault(p => p.Id == id);
        if (product == null) return StatusCode(StatusCodes.Status404NotFound);
        ProductReturnDto productReturnDto = new()
        {
            Name = product.Name,
            Price = product.Price,
            DiscountPrice = product.DiscountPrice,
            CreatedTime=product.CreatedTime,
            UpdatedTime=product.UpdatedTime
        };

        return StatusCode(200, productReturnDto);
    }

    [HttpPost]
    public IActionResult AddProduct(ProductCreateDto productCreateDto)
    {
        Product newProduct = new Product()
        {
            Name = productCreateDto.Name,
            Price = productCreateDto.Price,
            DiscountPrice = productCreateDto.DiscountPrice,
            IsActive = productCreateDto.IsActive
        };

        _appDbContext.Products.Add(newProduct);
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created, newProduct);
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return StatusCode(StatusCodes.Status404NotFound);
        _appDbContext.Products.Remove(product);
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductUpdateDto productUpdateDto)
    {
        var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
        if (existProduct == null) return NotFound();
        existProduct.Name = productUpdateDto.Name;
        existProduct.Price = productUpdateDto.Price;
        existProduct.DiscountPrice = productUpdateDto.DiscountPrice;
        existProduct.IsActive = productUpdateDto.IsActive;
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);

    }

    [HttpPatch]
    public IActionResult ChangeStatus(int id, bool IsActive)
    {
        var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
        if (existProduct == null) return StatusCode(StatusCodes.Status404NotFound);
        existProduct.IsActive = IsActive;
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);
    }



}