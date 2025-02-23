using Domain.DTOs;

namespace Application.Interfaces;

public interface IProfileService
{
    Task<IEnumerable<ApplicationUserDTO>> GetUsersAsync();
    Task<ApplicationUserDTO> GetProfileByUsernameAsync(string username);
    Task<ApplicationUserDTO> GetProfileByUserIdAsync(string userId);
    Task<bool> CheckProfileByUserIdAsync(string userId);
}