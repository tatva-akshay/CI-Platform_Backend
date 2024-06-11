using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.MissionApplication;

public class MissionApplicationRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.MissionApplication>, IMissionApplicationRepo
{
    private readonly CIPlatformDbContext _dbContext;
    
    public MissionApplicationRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
