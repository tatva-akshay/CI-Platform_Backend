using CI_Platform_Backend_Presentation.DTO.Mission;
using CI_Platform_Backend_Services.Mission;
using CI_Platform_Backend_Services.User;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class MissionController : ControllerBase
{
    private readonly IMissionService _missionService;
    private readonly IUserService _userService;

    public MissionController(IMissionService missionService, IUserService userService)
    {
        _missionService = missionService;
        _userService = userService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateAsync(long userId, CreateMissionDTO createMissionDTO)
    {
        return !await _missionService.IsExistAsync(createMissionDTO.Title) && await _missionService.AddAsync(userId, createMissionDTO) ? 
            Ok() :
            BadRequest();
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllAsync(long userId)
    {
        return await _userService.IsExistAsync(userId) ?
            Ok(await _missionService.GetAllAsync(userId)) :
            BadRequest();
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetAsync(long userId, long missionId)
    {
        if(await _userService.IsExistAsync(userId))
        {
            return await _missionService.IsExistAsync(missionId) ?
                Ok(await _missionService.GetAsync(userId, missionId)) :
                NotFound();
        }
        return Unauthorized();
    }

    [HttpGet]
    [Route("get-related-mission")]
    public async Task<IActionResult> GetRelatedMissionsAsync(long userId, long missionId)
    {
        if(await _userService.IsExistAsync(userId))
        {
            return await _missionService.IsExistAsync(missionId) ?
                Ok(await _missionService.RelatedMissionsAsync(userId, missionId)) :
                NotFound();
        }
        return Unauthorized();
    }
}