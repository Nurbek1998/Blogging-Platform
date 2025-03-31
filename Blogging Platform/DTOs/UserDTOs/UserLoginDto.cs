using System.ComponentModel.DataAnnotations;

namespace Blogging_Platform.DTOs.UserDTOs;

public class UserLoginDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
