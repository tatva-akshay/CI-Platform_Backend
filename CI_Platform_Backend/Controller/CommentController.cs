using CI_Platform_Backend_Presentation.DTO.Comment;
using CI_Platform_Backend_Services.Comment;
using CI_Platform_Backend_Services.Mission;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("mission-comments")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMissionService _missionService;

    public CommentController(ICommentService commentService, IMissionService missionService)
    {
        _commentService = commentService;
        _missionService = missionService;
    }

    [HttpGet]
    [Route("{missionId}")]
    public async Task<ActionResult> GetByMissionAsync(long missionId)
    {
        return await _missionService.IsExistAsync(missionId) ? 
            Ok(await _commentService.GetByMissionAsync(missionId)) :
            NotFound();        
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult> AddAsync(CreateCommentDTO createCommentDTO)
    {
        return await _missionService.IsExistAsync(createCommentDTO.MissionId) ?
                await _commentService.AddAsync(createCommentDTO) ? 
                    Ok() :
                    BadRequest() :
            NotFound();
    }
}