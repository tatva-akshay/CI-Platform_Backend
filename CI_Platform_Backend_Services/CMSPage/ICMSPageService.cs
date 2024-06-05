using CI_Platform_Backend_Presentation.DTO.CMSPages;

namespace CI_Platform_Backend_Services.CMSPage;

public interface ICMSPageService
{
    Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy cmsPrivacyPolicy);
    
    Task<bool> UpdateAsync(long id, CreateCMSPageDTO cMSPageDTO);

    Task<bool> DeleteAsync(long id);

    Task<List<CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy>> GetCMSPagesAsync();

    Task<CI_Platform_Backend_DBEntity.DataModels.CmsPrivacyPolicy> GetCMSPageAsync(long id);
}
