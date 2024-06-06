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
        try
        {
            if(!await _missionService.IsExistAsync(createMissionDTO.Title))
            {
                if(await _missionService.AddAsync(userId, createMissionDTO))
                {
                    return Ok();
                }
            }
           return BadRequest();
        }
        catch (Exception ex)
        {

        return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllAsync(long userId)
    {
        try
        {
            if(!await _userService.IsExistAsync(userId))
            {
                return Ok(await _missionService.GetAllAsync(userId));
            }
           return BadRequest();
        }
        catch (Exception ex)
        {

        }
        return BadRequest();
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetAsync(long userId, long missionId)
    {
        try
        {
            if(await _userService.IsExistAsync(userId))
            {
                if(await _missionService.IsExistAsync(missionId))
                {
                    return Ok(await _missionService.GetAsync(userId, missionId));
                }
                return NotFound();
            }
           return Unauthorized();
        }
        catch (Exception ex)
        {

        return BadRequest(ex.ToString());
        }
    }
}
