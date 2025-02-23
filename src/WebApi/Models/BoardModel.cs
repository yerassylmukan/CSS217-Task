using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class BoardModel
{
    [Required] public string Name { get; set; }
    [Required] public string OwnerId { get; set; }
}