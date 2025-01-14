using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Helpers;

public class DataConnectionContext : DbContext
{
    public DataConnectionContext(DbContextOptions<DataConnectionContext> options)
        : base(options)
    {
    }

    public DbSet<DataConnections> DataConnection { get; set; }
}
