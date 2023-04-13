using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCrud.Controllers;
using ApiCrud.Dtos.UserDtos;
using ApiCrud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrud.Controller;


public class AccountController : BaseController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<AppUser> userManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var user = await _userManager.FindByNameAsync(registerDto.UserName);
        if (user != null) return StatusCode(409);

        
        AppUser appUser = new AppUser();
        appUser.Fullname=registerDto.Fullname;
        appUser.UserName=registerDto.UserName;

        var result = await _userManager.CreateAsync(appUser,registerDto.Password);
        if(!result.Succeeded) return BadRequest(result.Errors);
        result = await _userManager.AddToRoleAsync(appUser,"Member");
        if(!result.Succeeded) return BadRequest(result.Errors);

        return StatusCode(201);
    }


    public async Task<IActionResult> Login(LoginDto loginDto)
    {

        return StatusCode(201);
    };
 }

