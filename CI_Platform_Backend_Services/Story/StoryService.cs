using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.Story;
using CI_Platform_Backend_Repository.Mission;
using CI_Platform_Backend_Repository.Story;
using CI_Platform_Backend_Repository.StoryView;
using CI_Platform_Backend_Repository.UserRepo;

namespace CI_Platform_Backend_Services.Story;

public class StoryService : IStoryService
{
    private readonly IStoryRepo _storyRepo;

    private readonly IMissionRepo _missionRepo;

    private readonly IUserRepo _userRepo;

    private readonly IStoryViewRepo _storyViewRepo;

    public StoryService(IStoryRepo storyRepo, IMissionRepo missionRepo, IUserRepo userRepo, IStoryViewRepo storyViewRepo)
    {
        _storyRepo = storyRepo;
        _missionRepo = missionRepo;
        _userRepo = userRepo;
        _storyViewRepo = storyViewRepo;
    }

    public async Task<bool> AddOrUpdateAsync(CreateStoryDTO createStoryDTO)
    {
        CI_Platform_Backend_DBEntity.DataModels.Mission mission = await _missionRepo.GetAsync(x => x.MissionId == createStoryDTO.MissionId);
        
        if(mission == null || mission.MissionId == 0)
        {
            return false;
        }

        CI_Platform_Backend_DBEntity.DataModels.Story story = await _storyRepo.GetAsync(x => x.UserId == createStoryDTO.UserId && x.MissionTitle.ToLower() == mission.MissionTitle.ToLower());
        
        if(story == null || story.StoryId == 0)
        {
            story = new CI_Platform_Backend_DBEntity.DataModels.Story()
            {
                StoryTitle = createStoryDTO.Title,
                MissionTitle = mission.MissionTitle,
                UserId = createStoryDTO.UserId,
                StoryDescription = createStoryDTO.Description,
                Publish = false,
            };
            if(createStoryDTO.Images?.Count > 0)
            {
                createStoryDTO.Images.ForEach(image => 
                {
                    byte[] imageBytes;
                    using (var item = new MemoryStream())
                    {
                        image.CopyTo(item);
                        imageBytes = item.ToArray();
                    }
                    story.StoryMedia.Add(new CI_Platform_Backend_DBEntity.DataModels.StoryMedium()
                    {
                        MissionId = createStoryDTO.MissionId,
                        Image = imageBytes,
                    });
                });
            }
            await _storyRepo.AddAsync(story);
            return true;
        }

        story.StoryTitle = createStoryDTO.Title;
        story.StoryDescription = createStoryDTO.Description;
        if(createStoryDTO.Images?.Count > 0)
        {
            createStoryDTO.Images.ForEach(image => 
            {
                byte[] imageBytes;
                using (var item = new MemoryStream())
                {
                    image.CopyTo(item);
                    imageBytes = item.ToArray();
                }
                story.StoryMedia.Add(new CI_Platform_Backend_DBEntity.DataModels.StoryMedium()
                {
                        MissionId = createStoryDTO.MissionId,
                    Image = imageBytes,
                });
            });
        }
        return await _storyRepo.UpdateAsync(story);
    }

    public async Task<List<StoryDTO>> GetAllAsync(long missionId)
    {
        CI_Platform_Backend_DBEntity.DataModels.Mission mission = await _missionRepo.GetAsync(x => x.MissionId == missionId);
        if(mission == null || mission.MissionId == 0)
        {
            return new List<StoryDTO>();
        }
        List<CI_Platform_Backend_DBEntity.DataModels.Story> stories = await _storyRepo.GetStoriesAsync(mission.MissionTitle);
        
        return stories.Select(x=> new StoryDTO()
        {
            StoryId = x.StoryId,
            Thumbnail = x.StoryMedia.FirstOrDefault()?.Image,
            Theme = mission.MissionTheme,
            Title = x.StoryTitle,
            Description = x.StoryDescription,
            UserId = x.UserId,
            UserName = x.User.FirstName + " " + x.User.LastName,
            UserProfile = x.User.Avatar,
            CreatedAt = x.CreatedAt,
        }).ToList();
    }

    public async Task<StoryDetailsDTO> GetAsync(long storyId, long userId)
    {
        CI_Platform_Backend_DBEntity.DataModels.Story story = await _storyRepo.GetStoryAsync(storyId);
        if(story == null || story.StoryId == 0)
        {
            return new StoryDetailsDTO();
        }

        StoryView storyView = await _storyViewRepo.GetAsync(x=>x.StoryId == storyId);
        if(userId > 0 && await _userRepo.IsExistAsync(userId))
        {
            if(storyView == null || storyView.ViewId == 0)
            {
                await _storyViewRepo.AddAsync(new StoryView()
                {
                    StoryId = storyId,
                    UserIds = userId.ToString()
                });
            }
            else
            {
                List<string> userIds =  storyView.UserIds?.Split(", ").ToList();
                if(userIds == null || userIds.Count == 0)
                {
                    storyView.UserIds = userId.ToString();
                    await _storyViewRepo.UpdateAsync(storyView);
                }
                else
                {
                    if(!userIds.Contains(userId.ToString()))
                    {
                        userIds.Add(userId.ToString());
                        storyView.UserIds = String.Join(", ", userIds);
                        await _storyViewRepo.UpdateAsync(storyView);
                    }
                }
            }
            
        }

        CI_Platform_Backend_DBEntity.DataModels.Mission mission = await _missionRepo.GetAsync(x=>x.MissionTitle == story.MissionTitle);
        return new StoryDetailsDTO()
        {
            StoryId = story.StoryId,
            Title = story.StoryTitle,
            Views = storyView?.UserIds?.Split(", ").Count(),
            Images = story.StoryMedia.Select(x=>x.Image).ToArray(),
            UserName = story.User.FirstName + " " + story.User.LastName,
            UserCity = story.User?.City?.City1,
            UserCountry = story.User?.Country?.Country1,
            UserIntroduction = story.User.WhyIVolunteer,
            Description = story.StoryDescription,
            MissionId = mission.MissionId,
            ProfileImage = story.User?.Avatar,
            // VideoUrls = story
        };
    }

    
}
