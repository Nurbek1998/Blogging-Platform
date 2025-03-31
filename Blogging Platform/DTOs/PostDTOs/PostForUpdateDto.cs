namespace Blogging_Platform.DTOs.PostDTOs;

public class PostForUpdateDto
{
    public string Title { get; set; }

    public string Content { get; set; }

    public string Status { get; set; } = "Draft";

    public Guid CategoryId { get; set; }
    public List<Guid> TagIds { get; set; } = [];

}
