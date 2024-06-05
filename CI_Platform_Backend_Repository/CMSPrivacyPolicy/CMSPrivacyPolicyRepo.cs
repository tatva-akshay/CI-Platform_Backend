using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.CMSPrivacyPolicyRepo;

public class CMSPrivacyPolicyRepo : Repository<CmsPrivacyPolicy>, ICMSPrivacyPolicyRepo
{
    private readonly ApplicationDbContext _dbContext;

    public CMSPrivacyPolicyRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
}
