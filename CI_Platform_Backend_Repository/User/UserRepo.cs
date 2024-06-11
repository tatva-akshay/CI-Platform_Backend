using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.UserRepo;

public class UserRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.User>, IUserRepo
{
    private readonly CIPlatformDbContext _dbContext;
    public UserRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsExistAsync(long id)
    {
        return await _dbContext.Users.AnyAsync(x=>x.UserId == id);
    }

}
