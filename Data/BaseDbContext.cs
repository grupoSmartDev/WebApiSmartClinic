using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Infra;

namespace WebApiSmartClinic.Data;

public abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrEmpty(EmpresasConnectionString.Current))
        {
            optionsBuilder.UseSqlServer(EmpresasConnectionString.Current);
        }

        base.OnConfiguring(optionsBuilder);
    }
}
