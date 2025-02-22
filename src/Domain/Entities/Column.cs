using Task = Domain.Entities.Task;

namespace Domain.Entities;

public class Column
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BoardId { get; set; }
    public Board Board { get; set; }
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}