using System.ComponentModel.DataAnnotations;

namespace User_Management_API.Models;

public class PostForUpdateDto
{
    [Required(ErrorMessage = "You have to complete all fields")]
    [MaxLength(40)]
    public string Title { get; set; }
    [MaxLength(1000)]
    public string Content { get; set; }
}