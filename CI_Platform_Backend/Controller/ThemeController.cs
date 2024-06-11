using AutoMapper;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation;
using CI_Platform_Backend_Presentation.DTO.CreateTheme;
using CI_Platform_Backend_Presentation.DTO.Theme;
using CI_Platform_Backend_Services.Theme;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("themes")]
public class ThemeController : ControllerBase
{
    private readonly IThemeService _themeService;
    private readonly IMapper _mapper;
    private readonly APIResponse _aPIResponse = new APIResponse();

    public ThemeController(IThemeService themeService, IMapper mapper)
    {
        _themeService = themeService;
        _mapper = mapper;
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Create new Theme option
    [HttpPost]
    [Route("")]
    public async Task<ActionResult> CreateAsync(CreateThemeDTO themeDTO)
    {
        return await _themeService.AddAsync(_mapper.Map<Theme>(themeDTO)) ?
            Ok() :
            BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Update Theme option
    [HttpPut]
    [Route("")]
    public async Task<ActionResult> UpdateAsync(long id, CreateThemeDTO themeDTO)
    {
        return await _themeService.UpdateAsync(id, themeDTO) ?
            Ok() :
            BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Delete Theme option
    [HttpDelete]
    [Route("")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        return await _themeService.DeleteAsync(id) ?
            Ok() :
            BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get All Theme options
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAllAsync()
    {
        _aPIResponse.IsSuccess = true;
        _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
        _aPIResponse.Result = _mapper.Map<List<ThemeDTO>>(await _themeService.GetThemesAsync());
        return Ok(_aPIResponse);
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get Theme option using Theme ID
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetAsync(long id)
    {
        ThemeDTO theme = _mapper.Map<ThemeDTO>(await _themeService.GetThemeAsync(id));
        return theme == null || theme.ThemeID == 0 ?
            NotFound() :
            Ok(theme);
    }
}