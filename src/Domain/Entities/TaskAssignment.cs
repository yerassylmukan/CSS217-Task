namespace Domain.Entities;

public class TaskAssignment
{
    public int TaskId { get; set; }
    public Task Task { get; set; }
    public string UserId { get; set; }
}