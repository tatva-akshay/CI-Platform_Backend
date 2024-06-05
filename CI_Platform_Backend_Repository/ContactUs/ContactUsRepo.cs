using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.ContactUs;

public class ContactUsRepo : Repository<ContactUss>, IContactUsRepo
{
    private readonly ApplicationDbContext _dbContext;

    public ContactUsRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}
