using System.Security.Authentication;
using Application.Interfaces;
using Domain.Exceptions;
using Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services;

public class AuthService : IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        ITokenClaimsService tokenClaimsService, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenClaimsService = tokenClaimsService;
        _signInManager = signInManager;
    }

    public async Task RegisterAsync(string username, string password)
    {
        var userExist = await _userManager.FindByNameAsync(username);

        if (userExist != null) throw new UserAlreadyExistsException(username);

        var user = new ApplicationUser
        {
            UserName = username,
            Email = username
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new ArgumentException("Invalid password");

        await _userManager.AddToRoleAsync(user, "User");
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username)
                   ?? throw new UserNotFoundException(username);

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) throw new InvalidCredentialException("Invalid password");

        return await _tokenClaimsService.GenerateToken(username);
    }

    public async Task<string> AddUserToRolesAsync(string username, IEnumerable<string> roles)
    {
        var user = await _userManager.FindByNameAsync(username)
                   ?? throw new UserNotFoundException(username);

        foreach (var role in roles)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist) throw new RoleDoesNotExistsException(role);

            await _userManager.AddToRoleAsync(user, role);
        }

        return await _tokenClaimsService.GenerateToken(username);
    }

    public async Task<string> ChangeUsernameAsync(string username, string newUsername)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new UserNotFoundException(username);

        var userExists = await _userManager.FindByNameAsync(newUsername);

        if (userExists != null)
            throw new UserAlreadyExistsException(newUsername);

        user.UserName = newUsername;
        await _userManager.UpdateAsync(user);

        return await _tokenClaimsService.GenerateToken(user.UserName);
    }

    public async Task<string> ChangePasswordAsync(string username, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new UserNotFoundException(username);

        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded)
            throw new InvalidCredentialException("Invalid password");

        return await _tokenClaimsService.GenerateToken(username);
    }
}