using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiCrud.Models;
using ApiCrud.Data.DAL;

namespace ApiCrud.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController:ControllerBase
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
        return StatusCode(200,product);
    }
    

    public IActionResult AddProduct(Product product)
    {
        _appDbContext.Products.Add(product);
        _appDbContext.SaveChanges();
         return StatusCode(StatusCodes.Status201Created,product);
    }
}