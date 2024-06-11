using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.Country;

public interface ICountryRepo : IRepository<CI_Platform_Backend_DBEntity.DbModels.Country>
{
    Task<string> GetNameAsync(long id);
}
