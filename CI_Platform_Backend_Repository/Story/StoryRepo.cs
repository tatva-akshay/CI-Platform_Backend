using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Story;

public class StoryRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.Story>, IStoryRepo
{
    private readonly ApplicationDbContext _dbContext;
    public StoryRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CI_Platform_Backend_DBEntity.DataModels.Story>> GetStoriesAsync(string missionTitle)
    {
        return await _dbContext.Stories.Where(s => s.MissionTitle == missionTitle).Include(x=>x.StoryMedia).Include(x=>x.User).ToListAsync();
    }
    public async Task<CI_Platform_Backend_DBEntity.DataModels.Story> GetStoryAsync(long storyId)
    {
        return await _dbContext.Stories.Where(x=> x.StoryId == storyId).Include(x=>x.StoryMedia).Include(x=>x.User).ThenInclude(x=>x.City).ThenInclude(x=>x.Country).FirstOrDefaultAsync();
    }


}
