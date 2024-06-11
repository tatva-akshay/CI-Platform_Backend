using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.City;

public interface ICityRepo : IRepository<CI_Platform_Backend_DBEntity.DbModels.City>
{
    Task<string> GetNameAsync(long id);
}
