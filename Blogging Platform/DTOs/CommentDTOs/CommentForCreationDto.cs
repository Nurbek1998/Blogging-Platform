namespace Blogging_Platform.DTOs.CommentDTOs;
public class CommentForCreationDto
{
    public string Content { get; set; }
    public Guid PostId { get; set; }
}
