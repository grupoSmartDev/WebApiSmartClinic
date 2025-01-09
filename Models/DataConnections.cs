using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Models;

public class DataConnections
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Key { get; set; }

    [Required]
    public required string StringConnection { get; set; }
}
