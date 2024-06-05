using CI_Platform_Backend_DBEntity.DataModels;

namespace CI_Platform_Backend_Services;

public interface IRegisterUserService
{
    Task<bool> RegisterUserAsync(CI_Platform_Backend_DBEntity.DataModels.User user);
}
