namespace Domain.Entities;

public class Board
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public ICollection<Column> Columns { get; set; } = new List<Column>();
}