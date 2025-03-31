using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogging_Platform.Entities;
public class Post
{
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(255)]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Required, MaxLength(20)]
    public string Status { get; set; } = "Draft";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }


    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Like> Likes { get; set; } = [];
    public ICollection<MediaAttachment> MediaAttachments { get; set; } = [];
    public ICollection<PostTag> PostTags { get; set; } = [];
}
