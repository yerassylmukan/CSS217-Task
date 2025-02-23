namespace Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(string username, string password);
    Task<string> LoginAsync(string username, string password);
    Task<string> AddUserToRolesAsync(string username, IEnumerable<string> roles);
    Task<string> ChangeUsernameAsync(string username, string newEmail);
    Task<string> ChangePasswordAsync(string username, string oldPassword, string newPassword);
}