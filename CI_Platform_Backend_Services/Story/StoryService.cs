using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.Story;
using CI_Platform_Backend_Repository.Mission;
using CI_Platform_Backend_Repository.MissionApplication;
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
    private readonly IMissionApplicationRepo _missionApplicationRepo;

    public StoryService(IStoryRepo storyRepo, IMissionRepo missionRepo, IUserRepo userRepo, IStoryViewRepo storyViewRepo, IMissionApplicationRepo missionApplicationRepo)
    {
        _storyRepo = storyRepo;
        _missionRepo = missionRepo;
        _userRepo = userRepo;
        _storyViewRepo = storyViewRepo;
        _missionApplicationRepo = missionApplicationRepo;
    }

    public async Task<bool> AddOrUpdateAsync(CreateStoryDTO createStoryDTO)
    {
        CI_Platform_Backend_DBEntity.DbModels.Mission mission = await _missionRepo.GetAsync(x => x.MissionId == createStoryDTO.MissionId);
        
        if(mission == null || mission.MissionId == 0)
        {
            return false;
        }

        CI_Platform_Backend_DBEntity.DbModels.Story story = await _storyRepo.GetAsync(x => x.UserId == createStoryDTO.UserId && x.MissionTitle.ToLower() == mission.MissionTitle.ToLower());
        
        if(story == null || story.StoryId == 0)
        {
            story = new CI_Platform_Backend_DBEntity.DbModels.Story()
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
                    story.StoryMedia.Add(new CI_Platform_Backend_DBEntity.DbModels.StoryMedium()
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
                story.StoryMedia.Add(new CI_Platform_Backend_DBEntity.DbModels.StoryMedium()
                {
                        MissionId = createStoryDTO.MissionId,
                    Image = imageBytes,
                });
            });
        }
        return await _storyRepo.UpdateAsync(story);
    }

    public async Task<bool> IsValidAsync(long missionId, long userId)
    {
        MissionApplication missionApplication = await _missionApplicationRepo.GetAsync(x=>x.MissionId == missionId && x.UserId == userId && x.IsApproved== true);
        return !(missionApplication == null || missionApplication.ApplicationId == 0);
    }

    public async Task<List<StoryDTO>> GetAllAsync()
    {
        
        List<CI_Platform_Backend_DBEntity.DbModels.Story> stories = await _storyRepo.GetStoriesAsync();
        List<StoryDTO> storiesDTO = new List<StoryDTO>();
        foreach (var item in stories)
        {
            string theme = await _missionRepo.GetThemeAsync(item.MissionTitle);
            storiesDTO.Add(new StoryDTO()
            {
                StoryId = item.StoryId,
                Thumbnail = item.StoryMedia.FirstOrDefault()?.Image,
                Theme = theme,
                Title = item.StoryTitle,
                Description = item.StoryDescription,
                UserId = item.UserId,
                UserName = item.User.FirstName + " " + item.User.LastName,
                UserProfile = item.User.Avatar,
                CreatedAt = item.CreatedAt,
            });
        }
        return storiesDTO;
    }

    public async Task<StoryDetailsDTO> GetAsync(long storyId, long userId)
    {
        CI_Platform_Backend_DBEntity.DbModels.Story story = await _storyRepo.GetStoryAsync(storyId);
        if(story == null || story.StoryId == 0)
        {
            return new StoryDetailsDTO();
        }


        StoryView storyView = await _storyViewRepo.GetAsync(x=>x.StoryId == storyId);
        bool isNewView = false;
        if(userId > 0 && await _userRepo.IsExistAsync(userId))
        {
            if(storyView == null || storyView.ViewId == 0)
            {
                await _storyViewRepo.AddAsync(new StoryView()
                {
                    StoryId = storyId,
                    UserIds = userId.ToString()
                });
                isNewView = true;
            }
            else
            {
                List<string> userIds =  storyView.UserIds?.Split(", ").ToList();
                if(userIds == null || userIds.Count == 0)
                {
                    storyView.UserIds = userId.ToString();
                    await _storyViewRepo.UpdateAsync(storyView);
                    isNewView = true;
                }
                else
                {
                    if(!userIds.Contains(userId.ToString()))
                    {
                        userIds.Add(userId.ToString());
                        storyView.UserIds = String.Join(", ", userIds);
                        await _storyViewRepo.UpdateAsync(storyView);
                        isNewView = true;
                    }
                }
            }            
        }

        CI_Platform_Backend_DBEntity.DbModels.Mission mission = await _missionRepo.GetAsync(x=>x.MissionTitle == story.MissionTitle);
        return new StoryDetailsDTO()
        {
            StoryId = story.StoryId,
            Title = story.StoryTitle,
            Views = isNewView ? storyView?.UserIds?.Split(",").Count() + 1 : storyView?.UserIds?.Split(",").Count(),
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
