using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;

public static class PaginationHelper
{
    private const int DefaultPageSize = 10;
    private const int MaxPageSize = 200;

    public static async Task<ResponseModel<List<T>>> PaginateAsync<T>(
        IQueryable<T> query,
        int pageNumber,
        int pageSize
    ) where T : class
    {
        ResponseModel<List<T>> response = new ResponseModel<List<T>>();

        try
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? DefaultPageSize : Math.Min(pageSize, MaxPageSize);

            query = query.AsNoTracking();

            // Total de itens
            int totalCount = await query.CountAsync();

            // Paginação
            var paginatedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Configurar a resposta
            response.Dados = paginatedData;
            response.Mensagem = "Dados encontrados com sucesso.";
            response.Status = true;
            response.TotalCount = totalCount;
            response.PageSize = pageSize;

            return response;
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Mensagem = ex.Message;
            return response;
        }
    }
}
