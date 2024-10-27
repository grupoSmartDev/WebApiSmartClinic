
namespace WebApiSmartClinic.Dto.Profissional
{
    public class ProfissionalEdicaoDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public string Sexo { get; set; }
        public int ConselhoId { get; set; }
        public string RegistroConselho { get; set; }
        public string UfConselho { get; set; }
        public int ProfissaoId { get; set; }
        public string Cbo { get; set; }
        public string Rqe { get; set; }
        public string Cnes { get; set; }
    }
}