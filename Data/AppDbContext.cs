using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {

  }

  public DbSet<AutorModel> Autores { get; set; }
  public DbSet<LivroModel> Livros { get; set; }
  public DbSet<StatusModel> Status { get; set; }  
  
}
