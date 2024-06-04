using CI_Platform_Backend_DBEntity.DataModels;

namespace CI_Platform_Backend_Repository.UserRepo;

public interface IUserRepo
{
    Task<bool> AddAsync(User user);

    Task<User> GetByEmailAsync(string email);
}
