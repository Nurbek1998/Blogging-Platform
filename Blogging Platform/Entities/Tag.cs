using System.ComponentModel.DataAnnotations;

namespace Blogging_Platform.Entities
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<PostTag> PostTags { get; set; } = [];
    }
}
