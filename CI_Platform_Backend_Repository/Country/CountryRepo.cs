using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Country;

public class CountryRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.Country>, ICountryRepo
{
    private readonly CIPlatformDbContext _dbContext;

    public CountryRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GetNameAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DbModels.Country country = await _dbContext.Countries.FirstOrDefaultAsync(c => c.CountryId == id);
        return country?.Country1 ?? "";
    }

}
