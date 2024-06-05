using CI_Platform_Backend_DBEntity.Context;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.City;

public class CityRepo : ICityRepo
{
    private readonly ApplicationDbContext _dbContext;

    public CityRepo(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

}
