namespace CI_Platform_Backend_Presentation.DTO.Story;

public class StoryDetailsDTO
{

    public long StoryId { get; set; }

    public string Title { get; set; }

    public long? Views { get; set; }

    public byte[]?[]? Images { get; set; }

    public string VideoUrls { get; set; }

    public byte[]? ProfileImage { get; set; }

    public string UserName { get; set; }

    public string? UserCity { get; set; }

    public string? UserCountry { get; set; }

    public string UserIntroduction { get; set; }

    public string Description { get; set; }

    public long MissionId { get; set; }
}
