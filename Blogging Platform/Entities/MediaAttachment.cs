using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogging_Platform.Entities;
public class MediaAttachment
{
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(500)]
    public string FileUrl { get; set; }

    [Required, MaxLength(50)]
    public string FileType { get; set; }

    
    public Guid PostId { get; set; }
    public Post Post { get; set; }
}
