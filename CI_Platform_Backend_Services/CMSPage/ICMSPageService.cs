using CI_Platform_Backend_Presentation.DTO.CMSPages;

namespace CI_Platform_Backend_Services.CMSPage;

public interface ICMSPageService
{
    Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DbModels.CmsPrivacyPolicy cmsPrivacyPolicy);
    
    Task<bool> UpdateAsync(long id, CreateCMSPageDTO cMSPageDTO);

    Task<bool> IsExistsAsync(long id);

    Task<bool> IsExistsAsync(long id, string slug);

    Task<bool> DeleteAsync(long id);

    Task<List<CI_Platform_Backend_DBEntity.DbModels.CmsPrivacyPolicy>> GetCMSPagesAsync();

    Task<CI_Platform_Backend_DBEntity.DbModels.CmsPrivacyPolicy> GetCMSPageAsync(long id);

    Task<CI_Platform_Backend_DBEntity.DbModels.CmsPrivacyPolicy> GetCMSPageAsync(string slug);
}
