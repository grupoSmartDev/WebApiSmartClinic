
namespace WebApiSmartClinic.Models;

public class FornecedorModel
{
    public int Id { get; set; }
    public string? Razao { get; set; }
    public string? Fantasia { get; set; }
    public string? Tipo { get; set; }
    public string? EstadoCivil { get; set; }
    public string? Sexo { get; set; }
    public string? IE { get; set; }
    public string? IM { get; set; }
    public string? CPF { get; set; }
    public string? CNPJ { get; set; }
    public string? Pais { get; set; }
    public string? UF { get; set; }
    public string? Cidade { get; set; }
    public string? Bairro { get; set; }
    public string? Complemento { get; set; }
    public string? Logradouro { get; set; }
    public string? NrLogradouro { get; set; }
    public string? CEP { get; set; }
    public string? Celular { get; set; }
    public string? TelefoneFixo { get; set; }
    public string? Banco { get; set; }
    public string? Agencia { get; set; }
    public string? Conta { get; set; }
    public string? TipoPIX { get; set; }
    public string? ChavePIX { get; set; }
    public string? Email { get; set; }
    private DateTime? _DataNascimento;
    public DateTime? DataNascimento 
    {
        get => _DataNascimento;
        set => _DataNascimento = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
    }
    public string? Nome { get; set; }
    public string? Observacao { get; set; }

    //public ICollection<Financ_Pagar> Financ_Pagar { get; set; }
}