using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Theme;

public class ThemeRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.Theme>, IThemeRepo
{
    private readonly ApplicationDbContext _dbContext;

    public ThemeRepo(ApplicationDbContext dbContext) : base(dbContext) 
    {
        _dbContext = dbContext;
    }   

}
