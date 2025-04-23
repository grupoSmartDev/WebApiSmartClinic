using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Services.ConnectionsService;

namespace WebApiSmartClinic.Helpers;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = config.GetConnectionString("DefaultContext");

        var fakeProvider = new ConnectionStringProvider();
        fakeProvider.SetConnectionString(connectionString);

        var fakeConfig = Microsoft.Extensions.Options.Options.Create(new ConnectionStringConfig
        {
            CurrentKey = "Default"
        });

        return new AppDbContext(optionsBuilder.Options, fakeConfig, fakeProvider);
    }
}