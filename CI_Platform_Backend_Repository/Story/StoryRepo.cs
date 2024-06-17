using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Story;

public class StoryRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.Story>, IStoryRepo
{
    private readonly CIPlatformDbContext _dbContext;
    public StoryRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CI_Platform_Backend_DBEntity.DbModels.Story>> GetStoriesAsync()
    {
        return await _dbContext.Stories.Include(x=>x.StoryMedia).Include(x=>x.User).ToListAsync();
    }
    public async Task<CI_Platform_Backend_DBEntity.DbModels.Story> GetStoryAsync(long storyId)
    {
        return await _dbContext.Stories.Where(x=> x.StoryId == storyId).Include(x=>x.StoryMedia).Include(x=>x.User).ThenInclude(x=>x.City).ThenInclude(x=>x.Country).FirstOrDefaultAsync();
    }


}
