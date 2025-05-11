namespace WebApiSmartClinic.Dto.User;

public class UserResponseRequest 
{
    public string Id { get; set; }
    public string? UserKey { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? ProfilePictureBase64 { get; set; }
}
