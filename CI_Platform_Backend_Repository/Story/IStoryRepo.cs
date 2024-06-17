using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.Story;

public interface IStoryRepo : IRepository<CI_Platform_Backend_DBEntity.DbModels.Story>
{
    Task<List<CI_Platform_Backend_DBEntity.DbModels.Story>> GetStoriesAsync(); 

    Task<CI_Platform_Backend_DBEntity.DbModels.Story> GetStoryAsync(long storyId); 
}
