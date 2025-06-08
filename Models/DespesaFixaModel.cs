﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiSmartClinic.Dto.Financ_Pagar;

namespace WebApiSmartClinic.Models;

public class DespesaFixaModel
{

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; }

    [Required]
    public int DiaVencimento { get; set; }

    [Required]
    
    private DateTime? _DataInicio;
    public DateTime? DataInicio
    {
        get => _DataInicio?.ToLocalTime();
        set => _DataInicio = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }

    private DateTime? _DataFim;
    public DateTime? DataFim
    {
        get => _DataFim?.ToLocalTime();
        set => _DataFim = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }


    public bool? Ativo { get; set; } = true;

    [Required]
    public int Frequencia { get; set; } = 1;

    // Propriedades de Relacionamento 1:N
    public List<Financ_PagarModel> FinancPagar { get; set; } = new List<Financ_PagarModel>();
    // Fornecedor (opcional)
    public int? FornecedorId { get; set; }
    public FornecedorModel? Fornecedor { get; set; }

    public int? PlanoContaId { get; set; }
    public PlanoContaModel? PlanoConta { get; set;}
    
    public int? CentroCustoId { get; set; }

    public CentroCustoModel? CentroCusto { get; set; }

    public int? TipoPagamentoId { get; set; }

    public TipoPagamentoModel? TipoPagamento{ get; set; }

    public int? FormaPagamentoId { get; set; }

    public FormaPagamentoModel? FormaPagamento { get; set; }
    public DateTime DataAlteracao { get; internal set; }
}

