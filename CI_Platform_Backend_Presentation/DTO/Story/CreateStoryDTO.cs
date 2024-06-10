using Microsoft.AspNetCore.Http;

namespace CI_Platform_Backend_Presentation.DTO.Story;

public class CreateStoryDTO
{
    public string Title { get; set; }

    public long MissionId { get; set; }

    public long UserId { get; set; }

    public string Description { get; set; }

    public string? VideoUrls { get; set; }

    public List<IFormFile>? Images { get; set; }
}
