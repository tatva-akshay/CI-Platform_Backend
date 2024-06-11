using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
namespace CI_Platform_Backend_Repository.StoryView;

public class StoryViewRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.StoryView>, IStoryViewRepo
{
    private readonly CIPlatformDbContext _dbContext;

    public StoryViewRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}