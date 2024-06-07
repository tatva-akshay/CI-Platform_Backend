using CI_Platform_Backend_Presentation.DTO.Timesheet;
using CI_Platform_Backend_Services.User;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("dashboard/timesheet")]
public class TimesheetController : ControllerBase
{
    private readonly IUserService _userService;

    public TimesheetController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAsync(long userId)
    {
        if(!await _userService.IsExistAsync(userId))
            return Unauthorized();

        
        return Ok();
    }

    [HttpPost]
    [Route("time")]
    public async Task<ActionResult> AddAsync(long userId, AddTimesheet_TimeDTO _timesheetDTO)
    {
        if(!await _userService.IsExistAsync(userId))
            return Unauthorized();

        
        return Ok();
    }

    [HttpPost]
    [Route("goal")]
    public async Task<ActionResult> AddAsync(long userId, AddTimesheet_GoalDTO _timesheetDTO)
    {
        if(!await _userService.IsExistAsync(userId))
            return Unauthorized();

        
        return Ok();
    }
}
