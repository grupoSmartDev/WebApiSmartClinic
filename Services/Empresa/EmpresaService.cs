using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApiSmartClinic.Services.Empresa;

public class EmpresaService
{
    private readonly AppDbContext _context;

    public EmpresaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> GetConnectionStringForCompany(int companyId)
    {
        var company = await _context.Empresa.FindAsync(companyId);
        return company?.DatabaseConnectionString;
    }

    public async Task<bool> CreateNewDatabaseForCompany(string companyName)
    {
        var newDbName = $"CompanyDb_{companyName}";
        var connectionString = $"Server=.;Database={newDbName};Trusted_Connection=True;";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = $"CREATE DATABASE {newDbName}";
            await command.ExecuteNonQueryAsync();
        }

        var newCompany = new Models.EmpresaModel
        {
            Nome = companyName,
            DatabaseConnectionString = connectionString
        };

        _context.Empresa.Add(newCompany);
        await _context.SaveChangesAsync();

        return true;
    }
}
