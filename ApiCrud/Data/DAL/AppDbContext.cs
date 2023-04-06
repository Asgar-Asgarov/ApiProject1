using Microsoft.EntityFrameworkCore;
using ApiCrud.Models;

namespace ApiCrud.Data.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
}