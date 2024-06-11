using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.Theme;

public interface IThemeRepo : IRepository<CI_Platform_Backend_DBEntity.DbModels.Theme> 
{
    Task<string> GetNameAsync(long id);
}
