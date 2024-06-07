namespace CI_Platform_Backend_Presentation.DTO.Story;

public class EditStoryDataDTO
{
    public long StoryId { get; set; }

    public string Title { get; set; }

    public long MissionId { get; set; }

    public string Description { get; set; }

    public string VideoUrls { get; set; }

    public byte[][]? Images { get; set; }
}
