using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.City;

public class CityRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.City>, ICityRepo
{
    private readonly ApplicationDbContext _dbContext;

    public CityRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GetNameAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DataModels.City city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == id);
        return city.City1;
    }
}
