using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.User;

public class UserInformationRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.UserInformation>, IUserInformationRepo
{
    private readonly CIPlatformDbContext _dbContext;

    public UserInformationRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
