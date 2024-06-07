using CI_Platform_Backend_Presentation.DTO.Story;
using CI_Platform_Backend_Services.Story;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpPost]
    [Route("add-update")]
    public async Task<ActionResult> AddOrUpdateAsync(CreateStoryDTO createStoryDTO)
    {
        return await _storyService.AddOrUpdateAsync(createStoryDTO) ?
            Ok() :
            BadRequest();
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAllAsync(long missionId)
    {
        return Ok(await _storyService.GetAllAsync(missionId));
    }

    [HttpGet]
    [Route("story")]
    public async Task<ActionResult> GetAsync(long storyId, long userId)
    {
        StoryDetailsDTO story = await _storyService.GetAsync(storyId, userId);
        return (story == null || story.StoryId == 0) ? 
            NotFound() :
            Ok(story);
    }
}