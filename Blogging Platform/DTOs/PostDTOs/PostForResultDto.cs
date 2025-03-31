namespace Blogging_Platform.DTOs.PostDTOs;

public class PostForResultDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Status { get; set; } = "Draft";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> TagIds { get; set; } = [];
    public List<Guid> CommentIds { get; set; } = [];

}
