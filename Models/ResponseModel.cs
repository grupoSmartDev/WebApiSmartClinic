namespace WebApiSmartClinic.Models;

public class ResponseModel<T>
{
    public T? Dados { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public int? TotalCount { get; set;} = 0;
    public int? PageSize { get; set;} = 0;
}
