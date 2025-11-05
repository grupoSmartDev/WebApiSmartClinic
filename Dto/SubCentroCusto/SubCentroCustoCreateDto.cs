namespace WebApiSmartClinic.Dto.SubCentroCusto
{
    public sealed class SubCentroCustoCreateDto
    {
        public string Nome { get; set; }
        public int CentroCustoId { get; set; } // ID do Centro de Custo PAI
    }
}
