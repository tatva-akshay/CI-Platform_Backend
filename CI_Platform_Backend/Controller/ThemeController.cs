using AutoMapper;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.CreateTheme;
using CI_Platform_Backend_Presentation.DTO.Theme;
using CI_Platform_Backend_Services.Theme;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class ThemeController : ControllerBase
{

    private readonly IThemeService _themeService;

    private readonly IMapper _mapper;

    public ThemeController(IThemeService themeService, IMapper mapper)
    {
        _themeService = themeService;
        _mapper = mapper;
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Create new Theme option
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateAsync(CreateThemeDTO themeDTO)
    {
        if(await _themeService.AddAsync(_mapper.Map<Theme>(themeDTO)))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Update Theme option
    [HttpPost]
    [Route("update")]
    public async Task<ActionResult> UpdateAsync(long id, CreateThemeDTO themeDTO)
    {
        if(await _themeService.UpdateAsync(id, themeDTO))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Delete Theme option
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        if(await _themeService.DeleteAsync(id))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get All Theme options
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAllAsync()
    {
        try
        {
            return Ok(_mapper.Map<List<ThemeDTO>>(await _themeService.GetThemesAsync()));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get Theme option using Theme ID
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetAsync(long id)
    {
        try
        {
            ThemeDTO theme = _mapper.Map<ThemeDTO>(await _themeService.GetThemeAsync(id));
            return theme == null || theme.ThemeID == 0 ?
                NotFound() :
                Ok(theme);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }
}
