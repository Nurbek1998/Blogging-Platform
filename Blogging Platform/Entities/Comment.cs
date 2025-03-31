using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogging_Platform.Entities;

public class Comment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid PostId { get; set; }
    public Post Post { get; set; }
}
