using CI_Platform_Backend_Presentation;
using CI_Platform_Backend_Presentation.DTO.Story;
using CI_Platform_Backend_Services.Story;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("stories")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;
    private readonly APIResponse _aPIResponse = new APIResponse();

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> AddOrUpdateAsync(CreateStoryDTO createStoryDTO)
    {
        if(await _storyService.IsValidAsync(createStoryDTO.MissionId, createStoryDTO.UserId))
        {
            return await _storyService.AddOrUpdateAsync(createStoryDTO) ?
                Ok() :
                BadRequest();
        }
        return Unauthorized();
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAllAsync()
    {
        _aPIResponse.IsSuccess = true;
        _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
        _aPIResponse.Result = await _storyService.GetAllAsync();
        return Ok(_aPIResponse);
    }

    [HttpGet]
    [Route("{storyId}")]
    public async Task<ActionResult> GetAsync(long storyId, [FromQuery] long userId)
    {
        StoryDetailsDTO story = await _storyService.GetAsync(storyId, userId);
        if(story != null && story.StoryId >= 0)
        {
            _aPIResponse.IsSuccess = true;
            _aPIResponse.StatusCode=System.Net.HttpStatusCode.OK;
            _aPIResponse.Result = story;
            return Ok(_aPIResponse);
        }
        _aPIResponse.StatusCode=System.Net.HttpStatusCode.NotFound;
        _aPIResponse.ErrorMessages = ["Story not found"];
        return NotFound(_aPIResponse);

    }
}