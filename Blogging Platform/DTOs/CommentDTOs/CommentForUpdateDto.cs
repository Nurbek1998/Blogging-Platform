using Blogging_Platform.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blogging_Platform.DTOs.CommentDTOs
{
    public class CommentForUpdateDto
    {
        [Required]
        public string Content { get; set; }
    }
}
