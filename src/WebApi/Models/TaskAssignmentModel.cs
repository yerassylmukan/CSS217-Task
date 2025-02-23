using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class TaskAssignmentModel
{
    [Required] public int TaskId { get; set; }
    [Required] public string UserId { get; set; }
}