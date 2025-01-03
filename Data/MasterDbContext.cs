using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Data;

public class MasterDbContext : DbContext
{
    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
    }

    public DbSet<EmpresaModel> Empresas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmpresaModel>()
            .HasIndex(t => t.Identificador)
            .IsUnique();
    }
}
