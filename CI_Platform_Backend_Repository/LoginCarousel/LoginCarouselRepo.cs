using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.LoginCarousel;

public class LoginCarouselRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.LoginCarousel>, ILoginCarouselRepo
{
    private readonly CIPlatformDbContext _dbContext;

    public LoginCarouselRepo(CIPlatformDbContext dbContext) : base(dbContext) 
    {
        _dbContext = dbContext;
    }   
    
    //public async Task<string> GetNameAsync(long id)
    //{
    //    CI_Platform_Backend_DBEntity.DbModels.LoginCarousel loginCarousel = await _dbContext.LoginCarousels.FirstOrDefaultAsync(c => c.CarouselId == id);
    //    return loginCarousel?.Theme1 ?? "";
    //}
}
