using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.ContactUs;

public class ContactUsRepo : Repository<ContactUss>, IContactUsRepo
{
    private readonly CIPlatformDbContext _dbContext;

    public ContactUsRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}
