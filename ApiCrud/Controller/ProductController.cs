using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiCrud.Models;

namespace ApiCrud.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController:ControllerBase
{  
    [HttpGet]
    public IActionResult GetAll()
    {
         return Ok(StatusCodes.Status200OK);
    }

      
       [HttpGet("{id}")] 
       public IActionResult GetOne()
    {
        return Ok(StatusCodes.Status200OK);
    }
    
}