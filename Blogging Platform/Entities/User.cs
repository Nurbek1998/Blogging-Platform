using System.ComponentModel.DataAnnotations;

namespace Blogging_Platform.Entities;
public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(20)]
    public string Username { get; set; }

    [Required, MaxLength(255)]
    public string PasswordHash { get; set; }

    [Required]
    public string Role { get; set; }

    public ICollection<Post> Posts { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Like> Likes { get; set; } = [];
    public ICollection<Notification> Notifications { get; set; } = [];
}
