using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CommentModel
{
    [Required] public string Content { get; set; }
    [Required] public string UserId { get; set; }
    [Required] public int TaskId { get; set; }
}