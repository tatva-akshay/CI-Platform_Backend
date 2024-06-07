using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.Story;

public interface IStoryRepo : IRepository<CI_Platform_Backend_DBEntity.DataModels.Story>
{
    Task<List<CI_Platform_Backend_DBEntity.DataModels.Story>> GetStoriesAsync(string missionTitle); 

    Task<CI_Platform_Backend_DBEntity.DataModels.Story> GetStoryAsync(long storyId); 
}
