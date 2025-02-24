namespace Domain.DTOs;

public class ColumnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BoardId { get; set; }
    public ICollection<TaskDto> Tasks { get; set; }
}