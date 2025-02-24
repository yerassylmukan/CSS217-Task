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
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BoardDto>> GetByBoardId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var board = await _boardService.GetByIdAsync(id);
        return Ok(board);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetAllBoard(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var boards = await _boardService.GetAllAsync();
        return Ok(boards);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBoard([FromBody] BoardModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _boardService.AddAsync(model.Name, model.OwnerId);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBoard([Required] int id, [FromBody] BoardModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _boardService.UpdateAsync(id, model.Name, model.OwnerId);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteByBoardId([Required] int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _boardService.DeleteAsync(id);
        return Ok();
    }
}