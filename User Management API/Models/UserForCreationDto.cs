using System.ComponentModel.DataAnnotations;

namespace User_Management_API.Models;

public class UserForCreationDto
{
    [Required(ErrorMessage = "You have to complete all fields")]
    [MaxLength(20)]
    public string FirstName { get; set; }
    [MaxLength(20)]
    public string LastName { get; set; }
    [MaxLength(40)]
    public string Mail { get; set; }
}