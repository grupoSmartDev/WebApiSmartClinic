using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.SubCentroCusto;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.SubCentroCusto
{
    public class SubCentroCustoService : ISubCentroCustoInterface
    {
        private readonly AppDbContext _context;

        public SubCentroCustoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<SubCentroCustoModel>>> Criar(SubCentroCustoCreateDto dto)
        {
            ResponseModel<List<SubCentroCustoModel>> resposta = new ResponseModel<List<SubCentroCustoModel>>();

            try
            {
                var subCentroCusto = new SubCentroCustoModel();

                subCentroCusto.Nome = dto.Nome;
                subCentroCusto.CentroCustoId = dto.CentroCustoId;
                

                _context.SubCentroCusto.Add(subCentroCusto);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.SubCentroCusto.ToListAsync();
                resposta.Mensagem = "Sub Centro de Custo criado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<SubCentroCustoModel>>> Editar(SubCentroCustoEdicaoDto dto)
        {
            ResponseModel<List<SubCentroCustoModel>> resposta = new ResponseModel<List<SubCentroCustoModel>>();
            
            try
            {
                var subCentroCusto = _context.SubCentroCusto.FirstOrDefault(x => x.Id == dto.Id);
                if (subCentroCusto == null)
                {
                    resposta.Mensagem = "Sub Centro de Custo não encontrado";
                    return resposta;
                }

                subCentroCusto.Nome = dto.Nome;
                subCentroCusto.CentroCustoId = dto.CentroCustoId;

                _context.Update(subCentroCusto);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.SubCentroCusto.ToListAsync();
                resposta.Mensagem = "Sub Centro de Custo atualizado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<SubCentroCustoModel>>> Listar()
        {
            var resposta = new ResponseModel<List<SubCentroCustoModel>>();

            try
            {
                var subCentrosCusto = await _context.SubCentroCusto.ToListAsync();
                resposta.Dados = subCentrosCusto;
                resposta.Mensagem = "Sub Centros de Custo listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<SubCentroCustoModel>> BuscarPorId(int id)
        {
            var resposta = new ResponseModel<SubCentroCustoModel>();

            try
            {
                var subCentroCusto = await _context.SubCentroCusto.FirstOrDefaultAsync(x => x.Id == id);
                if (subCentroCusto == null)
                {
                    resposta.Mensagem = "Sub Centro de Custo não encontrado";
                    return resposta;
                }

                resposta.Dados = subCentroCusto;
                resposta.Mensagem = "Sub Centro de Custo encontrado";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<SubCentroCustoModel>>> Delete(int id)
        {
            var resposta = new ResponseModel<List<SubCentroCustoModel>>();

            try
            {
                var subCentroCusto = await _context.SubCentroCusto.FindAsync(id);
                if (subCentroCusto == null)
                {
                    resposta.Mensagem = "Sub Centro de Custo não encontrado";
                    return resposta;
                }

                _context.SubCentroCusto.Remove(subCentroCusto);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.SubCentroCusto.ToListAsync();
                resposta.Mensagem = "Sub Centro de Custo excluído com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
