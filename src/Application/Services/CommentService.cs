﻿using Application.Interfaces;
using Application.Mappers;
using Domain.DTOs;
using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IProfileService _profileService;
    private readonly ITaskRepository _taskRepository;

    public CommentService(ICommentRepository commentRepository, ITaskRepository taskRepository,
        IProfileService profileService)
    {
        _commentRepository = commentRepository;
        _taskRepository = taskRepository;
        _profileService = profileService;
    }

    public async Task<CommentDto> GetByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id) ?? throw new ArgumentException("Comment not found");

        return comment.MapToDto();
    }

    public async Task<IEnumerable<CommentDto>> GetAllAsync()
    {
        var comments = await _commentRepository.GetAllAsync();

        var commentsDto = comments.Select(comment => comment.MapToDto());

        return commentsDto;
    }

    public async Task AddAsync(string content, string userId, int taskId)
    {
        var checkProfile = await _profileService.CheckProfileByUserIdAsync(userId);

        if (checkProfile) throw new ArgumentException("Profile not found");

        var task = await _taskRepository.GetByIdAsync(taskId) ?? throw new ArgumentException("Task not found");

        var comment = new Comment
        {
            Content = content,
            UserId = userId,
            TaskId = taskId
        };

        await _commentRepository.AddAsync(comment);
    }

    public async Task UpdateAsync(int id, string content, string userId, int taskId)
    {
        var comment = await _commentRepository.GetByIdAsync(id) ?? throw new ArgumentException("Comment not found");

        var checkProfile = await _profileService.CheckProfileByUserIdAsync(userId);

        if (checkProfile) throw new ArgumentException("Profile not found");

        var task = await _taskRepository.GetByIdAsync(taskId) ?? throw new ArgumentException("Task not found");

        comment.Content = content;

        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id) ?? throw new ArgumentException("Comment not found");

        await _commentRepository.DeleteAsync(comment);
    }
}