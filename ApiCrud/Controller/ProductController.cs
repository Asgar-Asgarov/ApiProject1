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
        return StatusCode(200,..)
    }

       [Route("getOne")] 
       [HttpGet] 
       public IActionResult GetOne()
    {
        return StatusCode(200,..)
    }
    
}