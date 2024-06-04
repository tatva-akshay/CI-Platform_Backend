using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.UserRepo;

public class UserRepo : IUserRepo
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepo(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        return await _dbContext.SaveChangesAsync() == 1;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email == email);
    }

}
