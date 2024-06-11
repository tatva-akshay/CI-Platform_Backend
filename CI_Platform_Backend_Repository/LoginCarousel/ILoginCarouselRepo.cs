using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.LoginCarousel;

public interface ILoginCarouselRepo : IRepository<CI_Platform_Backend_DBEntity.DbModels.LoginCarousel> 
{
    //Task<string> GetNameAsync(long id);
}
