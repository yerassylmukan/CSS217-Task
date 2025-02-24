using Domain.DTOs;

namespace Application.Interfaces;

public interface IProfileService
{
    Task<IEnumerable<ApplicationUserDto>> GetUsersAsync();
    Task<ApplicationUserDto> GetProfileByUsernameAsync(string username);
    Task<ApplicationUserDto> GetProfileByUserIdAsync(string userId);
    Task<bool> CheckProfileByUserIdAsync(string userId);
}