using System.ComponentModel.DataAnnotations;

namespace Blogging_Platform.DTOs.UserDTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
