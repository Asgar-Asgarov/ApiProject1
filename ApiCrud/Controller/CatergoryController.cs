using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiCrud.Models;
using ApiCrud.Data.DAL;
using ApiCrud.Dtos;
using AutoMapper;

namespace ApiCrud.Controllers;

public class CatergoryController : BaseController
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;


    public CatergoryController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll(int page ,string search)
    {
        var query = _appDbContext.Categories
        .Where(p => !p.IsDeleted);
        
        CategoryListDto categoryListDto = new();
         categoryListDto.CurrentPage = page;
        categoryListDto.TotalCount = query.Count();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => c.Name.Contains(search));
        }
        
        categoryListDto.items = query.Skip((page - 1) * 2)
       .Take(2)
       .Select(c => new CategoryListItemDto
       {
           Name = c.Name,
           Desc=c.Desc,
           ImageUrl=c.ImageUrl,
           CreatedTime = c.CreatedTime,
           UpdatedTime = c.UpdatedTime
       }).ToList();

        return StatusCode(200, categoryListDto);
    }


    [HttpGet("{id}")]
    public IActionResult GetOne(int id)
    {
        var category = _appDbContext.Categories
        .Where(c => !c.IsDeleted)
        .FirstOrDefault(c => c.Id == id);
        if (category == null) return StatusCode(StatusCodes.Status404NotFound);

        CategoryReturnDto categoryReturnDto = _mapper.Map<CategoryReturnDto>(category);

        return StatusCode(200, categoryReturnDto);
    }

    [HttpPost]
    public IActionResult AddCategory(CategoryCreateDto categoryCreateDto)
    {
        Category newCategory = new();

        this._mapper.Map(categoryCreateDto, newCategory);

        _appDbContext.Categories.Add(newCategory);
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status201Created, newCategory);
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null) return StatusCode(StatusCodes.Status404NotFound);
        _appDbContext.Categories.Remove(category);
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductUpdateDto categoryUpdateDto)
    {
        var existProduct = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (existProduct == null) return NotFound();

        this._mapper.Map(categoryUpdateDto, existProduct);

        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);

    }

    [HttpPatch]
    public IActionResult ChangeStatus(int id)
    {
        var existProduct = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (existProduct == null) return StatusCode(StatusCodes.Status404NotFound);
        // existProduct.IsActive = IsActive;
        _appDbContext.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);
    }



}