using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.CreateTheme;

namespace CI_Platform_Backend_Services.Theme;

public interface IThemeService
{
    Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DbModels.Theme theme);
    
    Task<bool> UpdateAsync(long id, CreateThemeDTO themeDTO);

    Task<bool> DeleteAsync(long id);

    Task<List<CI_Platform_Backend_DBEntity.DbModels.Theme>> GetThemesAsync();

    Task<CI_Platform_Backend_DBEntity.DbModels.Theme> GetThemeAsync(long id);
}
