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

    public async Task<IEnumerable<ApplicationUserDto>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var usersDto = new List<ApplicationUserDto>();

        foreach (var user in users)
        {
            var userDto = new ApplicationUserDto
            {
                UserId = user.Id,
                Username = user.UserName!,
                Roles = await _userManager.GetRolesAsync(user)
            };

            usersDto.Add(userDto);
        }

        return usersDto;
    }

    public async Task<ApplicationUserDto> GetProfileByUsernameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new UserNotFoundException(username);

        var roles = await _userManager.GetRolesAsync(user);

        var userDto = new ApplicationUserDto
        {
            UserId = user.Id,
            Username = user.UserName!,
            Roles = roles
        };

        return userDto;
    }

    public async Task<ApplicationUserDto> GetProfileByUserIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException();

        var roles = await _userManager.GetRolesAsync(user);

        var userDto = new ApplicationUserDto
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