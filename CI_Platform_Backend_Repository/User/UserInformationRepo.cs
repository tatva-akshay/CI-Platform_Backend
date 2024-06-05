using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.User;

public class UserInformationRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.UserInformation>, IUserInformationRepo
{
    private readonly ApplicationDbContext _dbContext;

    public UserInformationRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
