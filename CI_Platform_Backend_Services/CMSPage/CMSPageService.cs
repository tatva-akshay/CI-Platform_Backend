using CI_Platform_Backend_Presentation.DTO.CMSPages;
using CI_Platform_Backend_Repository.CMSPrivacyPolicyRepo;

namespace CI_Platform_Backend_Services.CMSPage;

public class CMSPageService : ICMSPageService
{
    private readonly ICMSPrivacyPolicyRepo _cMSPrivacyPolicyRepo;

    public CMSPageService(ICMSPrivacyPolicyRepo cMSPrivacyPolicyRepo)
    {
        _cMSPrivacyPolicyRepo = cMSPrivacyPolicyRepo;
    }

    public async Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy cmsPrivacyPolicy)
    {
        CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy cmsPrivacyPolicyTemp = await _cMSPrivacyPolicyRepo.GetAsync(x => x.Slug == cmsPrivacyPolicy.Slug);
    
        return cmsPrivacyPolicyTemp == null || cmsPrivacyPolicyTemp.CmsId == 0 ? 
            await _cMSPrivacyPolicyRepo.AddAsync(cmsPrivacyPolicy) : 
            false;
    }
    
    public async Task<bool> UpdateAsync(long id, CreateCMSPageDTO cMSPageDTO)
    {
        CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy cmsPrivacyPolicy = await _cMSPrivacyPolicyRepo.GetAsync(x => x.CmsId == id);
        CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy cmsPrivacyPolicy2 = await _cMSPrivacyPolicyRepo.GetAsync(x => x.Slug == cMSPageDTO.Slug);
        
        if(cmsPrivacyPolicy == null || cmsPrivacyPolicy.CmsId == 0 || (cmsPrivacyPolicy2 != null && cmsPrivacyPolicy2.CmsId > 0))
        {
            return false;
        }
        cmsPrivacyPolicy.PageTitle = cMSPageDTO.Title;
        cmsPrivacyPolicy.PageDescription = cMSPageDTO.Description;
        cmsPrivacyPolicy.Slug = cMSPageDTO.Slug;
        cmsPrivacyPolicy.Status = cMSPageDTO.IsActive;
        return await _cMSPrivacyPolicyRepo.UpdateAsync(cmsPrivacyPolicy);
    }

    public async Task<bool> IsExistsAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy cmsPrivacyPolicy = await _cMSPrivacyPolicyRepo.GetAsync(x => x.CmsId == id);
        
        return !(cmsPrivacyPolicy == null || cmsPrivacyPolicy.CmsId == 0);
    }

    public async Task<bool> IsExistsAsync(long id, string slug)
    {
        CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy cmsPrivacyPolicy = await _cMSPrivacyPolicyRepo.GetAsync(x => x.Slug.ToLower() == slug.ToLower());
        
        return (cmsPrivacyPolicy == null || cmsPrivacyPolicy.CmsId == 0 || cmsPrivacyPolicy.CmsId == id);
    }


    public async Task<List<CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy>> GetCMSPagesAsync()
    {
        return await _cMSPrivacyPolicyRepo.GetAsync();
    }

    public async Task<CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy> GetCMSPageAsync(long id)
    {
        return await _cMSPrivacyPolicyRepo.GetAsync(x => x.CmsId == id);
    }

    public async Task<CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy> GetCMSPageAsync(string slug)
    {
        return await _cMSPrivacyPolicyRepo.GetAsync(x => x.Slug == slug);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy cmsPrivacyPolicy = await _cMSPrivacyPolicyRepo.GetAsync(x => x.CmsId == id);
        
        return cmsPrivacyPolicy != null && cmsPrivacyPolicy.CmsId != 0 && await _cMSPrivacyPolicyRepo.DeleteAsync(cmsPrivacyPolicy);
    }
}
