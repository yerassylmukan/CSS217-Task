using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CredentialsModel requestModel,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _authService.RegisterAsync(requestModel.Username, requestModel.Password);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] CredentialsModel requestModel,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var token = await _authService.LoginAsync(requestModel.Username, requestModel.Password);

        return Ok(token);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{username}")]
    public async Task<IActionResult> AddUserToRoles([Required] string username,
        [Required] IEnumerable<string> roles, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var token = await _authService.AddUserToRolesAsync(username, roles);

        return Ok(token);
    }

    [HttpPost("{username}")]
    public async Task<IActionResult> ChangeUsername([Required] string username, [Required] string newUsername,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var currentUsername = User.Identity?.Name;

        if (currentUsername != username)
            return Unauthorized("You can only change your own account username");

        var token = await _authService.ChangeUsernameAsync(username, newUsername);

        return Ok(token);
    }

    [HttpPost("{username}")]
    public async Task<ActionResult<string>> ChangePassword([Required] string username, [Required] string oldPassword,
        string newPassword, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var currentUsername = User.Identity?.Name;

        if (currentUsername != username)
            return Unauthorized("You can only change your own account password");

        var token = await _authService.ChangePasswordAsync(username, oldPassword, newPassword);

        return Ok(token);
    }
}