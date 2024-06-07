using CI_Platform_Backend_Presentation.DTO.Story;

namespace CI_Platform_Backend_Services.Story;

public interface IStoryService
{
    Task<bool> AddOrUpdateAsync(CreateStoryDTO createStoryDTO);

    Task<List<StoryDTO>> GetAllAsync(long missionId);
    
    Task<StoryDetailsDTO> GetAsync(long storyId, long userId);

}
