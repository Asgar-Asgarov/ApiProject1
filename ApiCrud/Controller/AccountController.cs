using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCrud.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrud.Controller
{
   
    public class AccountController : BaseController
    {
        public IActionResult Register()
        {
         
          return StatusCode(201);
        }        
    }
}