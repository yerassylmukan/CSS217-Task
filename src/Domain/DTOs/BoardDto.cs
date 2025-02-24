namespace Domain.DTOs;

public class BoardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public ICollection<ColumnDto> Columns { get; set; }
}