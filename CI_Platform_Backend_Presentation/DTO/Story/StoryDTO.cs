namespace CI_Platform_Backend_Presentation.DTO.Story;

public class StoryDTO
{
    public long StoryId { get; set; }

    public byte[]? Thumbnail { get; set; }

    public string Theme { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public long UserId { get; set; }

    public string UserName { get; set; }

    public byte[]? UserProfile { get; set; }

    public DateTime CreatedAt { get; set; }
}
