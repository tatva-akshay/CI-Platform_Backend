using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.CMSPrivacyPolicyRepo;

public class CMSPrivacyPolicyRepo : Repository<CmsPrivacyPolicy>, ICMSPrivacyPolicyRepo
{
    private readonly CIPlatformDbContext _dbContext;

    public CMSPrivacyPolicyRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
}
