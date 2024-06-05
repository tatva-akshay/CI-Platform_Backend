using CI_Platform_Backend_DBEntity.Context;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Country;

public class CountryRepo : ICountryRepo
{
    private readonly ApplicationDbContext _dbContext;

    public CountryRepo(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

}
