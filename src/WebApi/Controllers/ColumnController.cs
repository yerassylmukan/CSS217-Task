﻿using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class ColumnController : ControllerBase
{
    private readonly IColumnService _columnService;

    public ColumnController(IColumnService columnService)
    {
        _columnService = columnService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Column>> GetByColumnId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var column = await _columnService.GetByIdAsync(id);
        return Ok(column);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Column>>> GetAllColum(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var columns = await _columnService.GetAllAsync();
        return Ok(columns);
    }

    [HttpPost]
    public async Task<IActionResult> AddColumn([FromBody] ColumnModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _columnService.AddAsync(model.Name, model.BoardId);
        return Ok();
    }

    [HttpPut("{id}")]
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
    public async Task<IActionResult> DeleteByColumnId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _columnService.DeleteAsync(id);
        return Ok();
    }
}