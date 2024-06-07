using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.MissionApplication;

public class MissionApplicationRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.MissionApplication>, IMissionApplicationRepo
{
    private readonly ApplicationDbContext _dbContext;
    
    public MissionApplicationRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
