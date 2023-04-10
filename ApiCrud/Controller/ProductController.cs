using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiCrud.Models;
using ApiCrud.Data.DAL;
using ApiCrud.Dtos;
using AutoMapper;

namespace ApiCrud.Controllers;

public class ProductController : BaseController
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public ProductController(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ProductController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public IActionResult GetAll(int page=1)
    {
        var query = _appDbContext.Products
        .Where(p=>!p.IsDeleted)
        .ToList();
        ProductListDto productListDto = new ()
        {
            // if (!string.IsNullOrWhiteSpace(search))
            // {
            //     query = query.Where(p=>p.Name.Contains(search));
            // }
         CurrentPage=page,
         TotalCount=query.Count(),
         items = query.Skip((page-1)*2)
         .Take(2)
         .Select(p=>new ProductListItemDto 
         {
          Name=p.Name,
          Price=p.Price,
          DiscountPrice=p.DiscountPrice,
          CreatedTime=p.CreatedTime,
          UpdatedTime=p.UpdatedTime  
         }).ToList()
        };
        return StatusCode(200,productListDto);
    }


    [HttpGet("{id}")]
    public IActionResult GetOne(int id)
    {
        var product = _appDbContext.Products
        .Where(p => !p.IsDeleted)
        .FirstOrDefault(p => p.Id == id);
        if (product == null) return StatusCode(StatusCodes.Status404NotFound);
        ProductReturnDto productReturnDto = _mapper.Map<ProductReturnDto>(product);
            // Name = product.Name,
            // Price = product.Price,
            // DiscountPrice = product.DiscountPrice,
            // CreatedTime=product.CreatedTime,
            // UpdatedTime=product.UpdatedTime
       

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