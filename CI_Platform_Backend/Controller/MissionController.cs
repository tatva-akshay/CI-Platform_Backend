using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation;
using CI_Platform_Backend_Presentation.DTO.Mission;
using CI_Platform_Backend_Services.Mission;
using CI_Platform_Backend_Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("missions")]
public class MissionController : ControllerBase
{
    private readonly IMissionService _missionService;
    private readonly IUserService _userService;
    private readonly APIResponse _aPIResponse = new APIResponse();

    public MissionController(IMissionService missionService, IUserService userService)
    {
        _missionService = missionService;
        _userService = userService;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateAsync(long userId, CreateMissionDTO createMissionDTO)
    {
        return !await _missionService.IsExistAsync(createMissionDTO.Title) && await _missionService.AddAsync(userId, createMissionDTO) ?
            Ok() :
            BadRequest();
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllAsync(long userId, [FromQuery] List<string> themes, [FromQuery] List<string> skills, [FromQuery] List<string> countries, [FromQuery] List<string> cities, [FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? search, [FromQuery] string? orderBy)
      {
        if(await _userService.IsExistAsync(userId))
        {
            _aPIResponse.IsSuccess = true;
            _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
            _aPIResponse.Result = await _missionService.GetAllAsync(userId, themes, skills, countries, cities, page, pageSize, search ?? "", orderBy);
            _aPIResponse.RowCount = await _missionService.GetMissionsCountAsync(themes, skills, countries, cities, search ?? "");
            _aPIResponse.Page = page == 0 ? 1 : page;
            _aPIResponse.PageSize = pageSize == 0 ? 10 : pageSize;
            return Ok(_aPIResponse);
        }
        _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
        _aPIResponse.ErrorMessages = ["User does not Exists"];
        return Unauthorized(_aPIResponse);
    }

    [HttpGet]
    [Route("{missionId}")]
    public async Task<IActionResult> GetAsync(long userId, long missionId)
    {
        if(await _userService.IsExistAsync(userId))
        {
            if (await _missionService.IsExistAsync(missionId))
            {
                _aPIResponse.IsSuccess = true;
                _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _aPIResponse.Result = await _missionService.GetAsync(userId, missionId);
                return Ok(_aPIResponse);
            }
            _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            _aPIResponse.ErrorMessages = ["Mission does not Exists"];
            return NotFound(_aPIResponse);
        }
        _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
        _aPIResponse.ErrorMessages = ["User does not Exists"];
        return Unauthorized(_aPIResponse);
    }

    [HttpGet]
    [Route("related-missions")]
    public async Task<IActionResult> GetRelatedMissionsAsync(long userId, long missionId)
    {
        if(await _userService.IsExistAsync(userId))
        {
            if (await _missionService.IsExistAsync(missionId))
            {
                _aPIResponse.IsSuccess = true;
                _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _aPIResponse.Result = await _missionService.RelatedMissionsAsync(userId, missionId);
                return Ok(_aPIResponse);
            }
            _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            _aPIResponse.ErrorMessages = ["Mission does not Exists"];
            return NotFound(_aPIResponse);
        }
        _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
        _aPIResponse.ErrorMessages = ["User does not Exists"];
        return Unauthorized(_aPIResponse);
    }

    [HttpPost]
    [Route("apply")]
    public async Task<ActionResult> ApplyAsync(long missionId, long userId)
    {
        if(await _userService.IsExistAsync(userId))
        {
            if (await _missionService.IsExistAsync(missionId) && await _missionService.IsValidRegistraionCriteria(missionId, userId))
            {
                _aPIResponse.IsSuccess = true;
                _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _aPIResponse.Result = await _missionService.ApplyAsync(userId, missionId);
                return Ok(_aPIResponse);
            }
            _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            _aPIResponse.ErrorMessages = ["Mission does not Exists or Invalid User to apply"];
            return NotFound(_aPIResponse);
        }
        _aPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
        _aPIResponse.ErrorMessages = ["User does not Exists"];
        return Unauthorized(_aPIResponse);
    }

    [HttpPost]
    [Route("approve")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> ApproveAsync(long missionId, long userId)
    {
        return await _missionService.IsExistAsync(missionId) ?
            Ok(await _missionService.ApproveAsync(userId, missionId)) :
            NotFound();
    }

    [HttpPost]
    [Route("decline")]
    // [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeclineAsync(long missionId, long userId)
    {
        return await _missionService.IsExistAsync(missionId) ?
            Ok(await _missionService.DeclineAsync(userId, missionId)) :
            NotFound();
    }
}