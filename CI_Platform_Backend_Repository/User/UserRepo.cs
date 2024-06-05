using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.UserRepo;

public class UserRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.User>, IUserRepo
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsExistAsync(long id)
    {
        return await _dbContext.Users.AnyAsync(x=>x.UserId == id);
    }

}
