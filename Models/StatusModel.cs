namespace WebApiSmartClinic.Models;

public class StatusModel
{
  public int Id { get; set; }
  public string Status { get; set; }
  public string Legenda { get; set; }
  public string Cor { get; set; }
    public bool IsSystemDefault { get; internal set; }
}
