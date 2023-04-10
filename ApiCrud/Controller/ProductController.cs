using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiCrud.Models;
using ApiCrud.Data.DAL;

namespace ApiCrud.Controllers;

public class ProductController:BaseController
{  
    private readonly AppDbContext _appDbContext;

    public ProductController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products =_appDbContext.Products.ToList();
         return StatusCode(200,products);
    }

      
       [HttpGet("{id}")] 
       public IActionResult GetOne(int id)
    {
        var product = _appDbContext.Products.FirstOrDefault(p=>p.Id==id);
        if(product==null) return StatusCode(StatusCodes.Status404NotFound);
        return StatusCode(200,product);
    }
    
     [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        _appDbContext.Products.Add(product);
        _appDbContext.SaveChanges();
         return StatusCode(StatusCodes.Status201Created,product);
    }
     
     
     [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _appDbContext.Products.FirstOrDefault(p=>p.Id==id);
        if(product==null) return StatusCode(StatusCodes.Status404NotFound);
        _appDbContext.Products.Remove(product);
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPut]
    public IActionResult UpdateProduct(Product product)
    {
       var existProduct = _appDbContext.Products.FirstOrDefault(p=>p.Id==product.Id);
       if(existProduct==null)return StatusCode(StatusCodes.Status404NotFound);
       existProduct.Name=product.Name;
       existProduct.Price=product.Price;
       existProduct.DiscountPrice=product.DiscountPrice;
       existProduct.IsActive=product.IsActive;
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);

    }

    [HttpPatch]
    public IActionResult ChangeStatus(int id,bool IsActive)
    {
        var existProduct = _appDbContext.Products.FirstOrDefault(p=>p.Id==id);
        if(existProduct==null) return StatusCode(StatusCodes.Status404NotFound);
        existProduct.IsActive=IsActive;
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);
    }



}