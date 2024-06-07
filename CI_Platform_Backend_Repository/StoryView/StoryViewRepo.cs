using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
namespace CI_Platform_Backend_Repository.StoryView;

public class StoryViewRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.StoryView>, IStoryViewRepo
{
    private readonly ApplicationDbContext _dbContext;

    public StoryViewRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}