using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class ColumnController : ControllerBase
{
    private readonly IColumnService _columnService;

    public ColumnController(IColumnService columnService)
    {
        _columnService = columnService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ColumnDto>> GetByColumnId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var column = await _columnService.GetByIdAsync(id);
        return Ok(column);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetAllColum(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var columns = await _columnService.GetAllAsync();
        return Ok(columns);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddColumn([FromBody] ColumnModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _columnService.AddAsync(model.Name, model.BoardId);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateColumn([Required] int id, [FromBody] ColumnModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _columnService.UpdateAsync(id, model.Name, model.BoardId);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteByColumnId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _columnService.DeleteAsync(id);
        return Ok();
    }
}