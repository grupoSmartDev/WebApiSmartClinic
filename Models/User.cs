using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Models;

public class User : IdentityUser
{
    [Required]
    public string UserKey { get; set; }
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    public byte[]? ProfilePicture { get; set; }
}
