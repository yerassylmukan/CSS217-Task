using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetByTaskId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var task = await _taskService.GetByIdAsync(id);
        return Ok(task);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTask(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] TaskModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _taskService.AddAsync(model.Title, model.Description, model.ColumnId);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask([Required] int id, [FromBody] TaskModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _taskService.UpdateAsync(id, model.Title, model.Description, model.ColumnId);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByTaskId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _taskService.DeleteAsync(id);
        return Ok();
    }
}