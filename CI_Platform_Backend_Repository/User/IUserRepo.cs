using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.UserRepo;

public interface IUserRepo : IRepository<CI_Platform_Backend_DBEntity.DbModels.User>
{
    Task<bool> IsExistAsync(long id);
}
