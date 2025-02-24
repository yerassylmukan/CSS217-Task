using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class TaskAssignmentController : ControllerBase
{
    private readonly ITaskAssignmentService _taskAssignmentService;

    public TaskAssignmentController(ITaskAssignmentService taskAssignmentService)
    {
        _taskAssignmentService = taskAssignmentService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskAssignmentDto>> GetByTaskIdAssignment([Required] int id,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var taskAssignment = await _taskAssignmentService.GetByTaskIdAsync(id);
        return Ok(taskAssignment);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<TaskAssignmentDto>> GetByUserIdAssignment([Required] string userId,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var taskAssignment = await _taskAssignmentService.GetByUserIdAsync(userId);
        return Ok(taskAssignment);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskAssignmentDto>>> GetAllTaskAssignment(
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var taskAssignments = await _taskAssignmentService.GetAllAsync();
        return Ok(taskAssignments);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddTaskAssignment([FromBody] TaskAssignmentModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _taskAssignmentService.AssignTaskAsync(model.TaskId, model.UserId);
        return Ok();
    }

    [HttpDelete("{taskId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveTaskAssignment([Required] int taskId, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _taskAssignmentService.DeleteByTaskIdAsync(taskId);
        return Ok();
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveUserAssignment([Required] string userId, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _taskAssignmentService.DeleteByUserIdAsync(userId);
        return Ok();
    }
}