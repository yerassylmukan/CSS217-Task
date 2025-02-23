using Application.Interfaces;
using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Services;

public class TaskAssignmentService : ITaskAssignmentService
{
    private readonly ITaskAssignmentRepository _assignmentRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IProfileService _profileService;

    public TaskAssignmentService(ITaskAssignmentRepository assignmentRepository, ITaskRepository taskRepository, IProfileService profileService)
    {
        _assignmentRepository = assignmentRepository;
        _taskRepository = taskRepository;
        _profileService = profileService;
    }

    public async Task<TaskAssignment> GetByTaskIdAsync(int taskId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId) ?? throw new ArgumentException("Task not found");
        
        var taskAssignment = await _assignmentRepository.GetByTaskIdAsync(taskId) ?? throw new ArgumentException("Task assignment not found");

        return taskAssignment;
    }

    public async Task<TaskAssignment> GetByUserIdAsync(string userId)
    {
        var checkProfile = await _profileService.CheckProfileByUserIdAsync(userId);

        if (checkProfile)
        {
            throw new ArgumentException("Profile not found");
        }
        
        var taskAssignment = await _assignmentRepository.GetByUserIdAsync(userId) ?? throw new ArgumentException("Task assignment not found");

        return taskAssignment;
    }

    public async Task<IEnumerable<TaskAssignment>> GetAllAsync()
    {
        return await _assignmentRepository.GetAllAsync();
    }

    public async Task AssignTaskAsync(int taskId, string userId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId) ?? throw new ArgumentException("Task not found");
        
        var checkProfile = await _profileService.CheckProfileByUserIdAsync(userId);

        if (checkProfile)
        {
            throw new ArgumentException("Profile not found");
        }
        
        var taskAssignment = new TaskAssignment
        {
            TaskId = taskId,
            UserId = userId
        };

        await _assignmentRepository.AddAsync(taskAssignment);
    }

    public async Task DeleteByTaskIdAsync(int taskId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId) ?? throw new ArgumentException("Task not found");
        
        var taskAssignment = await _assignmentRepository.GetByTaskIdAsync(taskId) ?? throw new ArgumentException("Task assignment not found");

        await _assignmentRepository.DeleteAsync(taskAssignment);
    }

    public async Task DeleteByUserIdAsync(string userId)
    {
        var checkProfile = await _profileService.CheckProfileByUserIdAsync(userId);

        if (checkProfile)
        {
            throw new ArgumentException("Profile not found");
        }
        
        var taskAssignment = await _assignmentRepository.GetByUserIdAsync(userId) ?? throw new ArgumentException("Task assignment not found");

        await _assignmentRepository.DeleteAsync(taskAssignment);
    }
}