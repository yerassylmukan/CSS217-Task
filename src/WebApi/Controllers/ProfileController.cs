using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetUsers(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var users = await _profileService.GetUsersAsync();

        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<ApplicationUserDto>> GetProfileByUsername([Required] string username,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var user = await _profileService.GetProfileByUsernameAsync(username);

        return Ok(user);
    }
}