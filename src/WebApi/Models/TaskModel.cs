using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class TaskModel
{
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public int ColumnId { get; set; }
}