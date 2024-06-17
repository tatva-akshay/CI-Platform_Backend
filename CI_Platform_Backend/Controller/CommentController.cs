using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation;
using CI_Platform_Backend_Presentation.DTO.CMSPages;
using CI_Platform_Backend_Presentation.DTO.Comment;
using CI_Platform_Backend_Services.Comment;
using CI_Platform_Backend_Services.Mission;
using CI_Platform_Backend_Services.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("mission-comments")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMissionService _missionService;
    private readonly IUserService _userService;
    private readonly APIResponse _aPIResponse = new APIResponse();

    public CommentController(ICommentService commentService, IMissionService missionService, IUserService userService)
    {
        _commentService = commentService;
        _missionService = missionService;
        _userService = userService;
    }

    [HttpGet]
    [Route("{missionId}")]
    public async Task<ActionResult> GetByMissionAsync(long missionId)
    {
        if(await _missionService.IsExistAsync(missionId))
        {
            List<CommentDTO> commentDTOs = await _commentService.GetByMissionAsync(missionId);
            _aPIResponse.IsSuccess = true;
            _aPIResponse.StatusCode = HttpStatusCode.OK;
            _aPIResponse.Result = commentDTOs;
            return Ok(_aPIResponse);
        }
        _aPIResponse.StatusCode = HttpStatusCode.NotFound;
        _aPIResponse.ErrorMessages = ["No Mission Found."];
        return NotFound(_aPIResponse);   
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult> AddAsync(CreateCommentDTO createCommentDTO)
    {
        if(await _userService.IsExistAsync(createCommentDTO.UserId))
        {

        if (await _missionService.IsExistAsync(createCommentDTO.MissionId))
        {
            if(await _commentService.AddAsync(createCommentDTO))
            {
                _aPIResponse.IsSuccess = true;
                _aPIResponse.StatusCode = HttpStatusCode.OK;
                _aPIResponse.Result = "Comment Posted Successfully";
                return Ok(_aPIResponse);
            }
            _aPIResponse.StatusCode = HttpStatusCode.BadRequest;
            _aPIResponse.ErrorMessages = ["Something went wrong. Please try again."];
            return BadRequest(_aPIResponse);
        }
        _aPIResponse.StatusCode = HttpStatusCode.NotFound;
        _aPIResponse.ErrorMessages = ["No Mission Found."];
        return NotFound(_aPIResponse);
        }
        _aPIResponse.StatusCode= HttpStatusCode.Unauthorized;
        _aPIResponse.ErrorMessages = ["No User Found."];
        return Unauthorized(_aPIResponse);
    }
}