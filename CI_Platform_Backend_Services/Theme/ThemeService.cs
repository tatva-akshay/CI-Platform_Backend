using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.CreateTheme;
using CI_Platform_Backend_Repository.Theme;

namespace CI_Platform_Backend_Services.Theme;

public class ThemeService : IThemeService
{
    private readonly IThemeRepo _themeRepo;

    public ThemeService(IThemeRepo themeRepo)
    {
        _themeRepo = themeRepo;
    }

    public async Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DbModels.Theme theme)
    {
        CI_Platform_Backend_DBEntity.DbModels.Theme themeTemp = await _themeRepo.GetAsync(x => x.Theme1 == theme.Theme1);
        
        return (themeTemp == null || themeTemp.ThemeId == 0) && await _themeRepo.AddAsync(theme);
    }
    
    public async Task<bool> UpdateAsync(long id, CreateThemeDTO themeDTO)
    {
        CI_Platform_Backend_DBEntity.DbModels.Theme theme = await _themeRepo.GetAsync(x => x.ThemeId == id);
        CI_Platform_Backend_DBEntity.DbModels.Theme theme2 = await _themeRepo.GetAsync(x => x.Theme1 == themeDTO.Name);
        
        if(theme == null || theme.ThemeId == 0 || (theme2!=null && theme2.ThemeId > 0))
        {
            return false;
        }
        theme.Theme1 = themeDTO.Name;
        theme.Status = themeDTO.IsActive;
        return await _themeRepo.UpdateAsync(theme);
    }

    public async Task<List<CI_Platform_Backend_DBEntity.DbModels.Theme>> GetThemesAsync()
    {
        return await _themeRepo.GetAsync();
    }

    public async Task<CI_Platform_Backend_DBEntity.DbModels.Theme> GetThemeAsync(long id)
    {
        return await _themeRepo.GetAsync(x => x.ThemeId == id);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DbModels.Theme theme = await _themeRepo.GetAsync(x => x.ThemeId == id);
        
        return theme != null && theme.ThemeId != 0 && await _themeRepo.DeleteAsync(theme);
    }
}