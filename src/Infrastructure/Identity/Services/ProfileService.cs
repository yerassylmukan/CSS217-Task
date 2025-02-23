using Application.Interfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<ApplicationUserDTO>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var usersDto = new List<ApplicationUserDTO>();

        foreach (var user in users)
        {
            var userDto = new ApplicationUserDTO
            {
                UserId = user.Id,
                Username = user.UserName!,
                Roles = await _userManager.GetRolesAsync(user)
            };

            usersDto.Add(userDto);
        }

        return usersDto;
    }

    public async Task<ApplicationUserDTO> GetProfileByUsernameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new UserNotFoundException(username);

        var roles = await _userManager.GetRolesAsync(user);

        var userDto = new ApplicationUserDTO
        {
            UserId = user.Id,
            Username = user.UserName!,
            Roles = roles
        };

        return userDto;
    }

    public async Task<ApplicationUserDTO> GetProfileByUserIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException();

        var roles = await _userManager.GetRolesAsync(user);

        var userDto = new ApplicationUserDTO
        {
            UserId = user.Id,
            Username = user.UserName!,
            Roles = roles
        };

        return userDto;
    }

    public async Task<bool> CheckProfileByUserIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user == null;
    }
}