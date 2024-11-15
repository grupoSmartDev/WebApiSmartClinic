using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;

public static class PaginationHelper
{
    public static async Task<ResponseModel<List<T>>> PaginateAsync<T>(
        IQueryable<T> query,
        int pageNumber,
        int pageSize
    )
    {
        ResponseModel<List<T>> response = new ResponseModel<List<T>>();

        try
        {
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
