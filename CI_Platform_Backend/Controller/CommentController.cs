using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation;
using CI_Platform_Backend_Presentation.DTO.CMSPages;
using CI_Platform_Backend_Presentation.DTO.Comment;
using CI_Platform_Backend_Services.Comment;
using CI_Platform_Backend_Services.Mission;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("mission-comments")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMissionService _missionService;
    private readonly APIResponse _aPIResponse = new APIResponse();

    public CommentController(ICommentService commentService, IMissionService missionService)
    {
        _commentService = commentService;
        _missionService = missionService;
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
            return Ok(commentDTOs);
        }
        _aPIResponse.StatusCode = HttpStatusCode.NotFound;
        _aPIResponse.ErrorMessages = ["No Mission Found."];
        return NotFound(_aPIResponse);   
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult> AddAsync(CreateCommentDTO createCommentDTO)
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
}