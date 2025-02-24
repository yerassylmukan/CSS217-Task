using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetByCommentId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        var comment = await _commentService.GetByIdAsync(id);
        return Ok(comment);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllComment(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var comments = await _commentService.GetAllAsync();
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CommentModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _commentService.AddAsync(model.Content, model.UserId, model.TaskId);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment([Required] int id, [FromBody] CommentModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _commentService.UpdateAsync(id, model.Content, model.UserId, model.TaskId);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByCommentId([Required] int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        cancellationToken.ThrowIfCancellationRequested();

        await _commentService.DeleteAsync(id);
        return Ok();
    }
}