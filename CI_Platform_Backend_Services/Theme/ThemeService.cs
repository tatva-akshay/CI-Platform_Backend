using CI_Platform_Backend_DBEntity.DataModels;
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

    public async Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DataModels.Theme theme)
    {
        CI_Platform_Backend_DBEntity.DataModels.Theme themeTemp = await _themeRepo.GetAsync(x => x.Theme1 == theme.Theme1);
        if(themeTemp == null || themeTemp.ThemeId == 0)
        {
            return await _themeRepo.AddAsync(theme);
        }
        return false;
    }
    
    public async Task<bool> UpdateAsync(long id, CreateThemeDTO themeDTO)
    {
        CI_Platform_Backend_DBEntity.DataModels.Theme theme = await _themeRepo.GetAsync(x => x.ThemeId == id);
        if(theme == null || theme.ThemeId == 0)
        {
            return false;
        }        

        theme.Theme1 = themeDTO.Name;
        theme.Status = themeDTO.IsActive;

        return await _themeRepo.UpdateAsync(theme);
    }

    public async Task<List<CI_Platform_Backend_DBEntity.DataModels.Theme>> GetThemesAsync()
    {
        return await _themeRepo.GetAsync();
    }

    public async Task<CI_Platform_Backend_DBEntity.DataModels.Theme> GetThemeAsync(long id)
    {
        return await _themeRepo.GetAsync(x => x.ThemeId == id);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DataModels.Theme theme = await _themeRepo.GetAsync(x => x.ThemeId == id);
        if(theme == null || theme.ThemeId == 0)
        {
            return false;
        }  
        return await _themeRepo.DeleteAsync(theme);
    }


}
