namespace Domain.DTOs;

public class ApplicationUserDto
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public ICollection<string> Roles { get; set; }
}