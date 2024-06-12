using System.Reflection.Metadata;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.Mission;
using CI_Platform_Backend_Presentation.DTO.Volunteer;
using CI_Platform_Backend_Repository.City;
using CI_Platform_Backend_Repository.Country;
using CI_Platform_Backend_Repository.Mission;
using CI_Platform_Backend_Repository.MissionApplication;
using CI_Platform_Backend_Repository.Skill;
using CI_Platform_Backend_Repository.Theme;
using CI_Platform_Backend_Utilities.ENUMS;

namespace CI_Platform_Backend_Services.Mission;

public class MissionService : IMissionService
{
    private readonly IMissionRepo _missionRepo;
    private readonly ICountryRepo _countryRepo;
    private readonly ICityRepo _cityRepo;
    private readonly IThemeRepo _themeRepo;
    private readonly IMissionApplicationRepo _missionApplicationRepo;

    private readonly ISkillRepo _skillRepo;
    public MissionService(IMissionRepo missionRepo, ICountryRepo countryRepo, ICityRepo cityRepo, IThemeRepo themeRepo, ISkillRepo skillRepo, IMissionApplicationRepo missionApplicationRepo)
    {
        _missionRepo = missionRepo;
        _countryRepo = countryRepo;
        _cityRepo = cityRepo;
        _themeRepo = themeRepo;
        _skillRepo = skillRepo;
        _missionApplicationRepo = missionApplicationRepo;
    }

    public async Task<bool> IsExistAsync(string title)
    {
        CI_Platform_Backend_DBEntity.DbModels.Mission mission = await _missionRepo.GetAsync(x => x.MissionTitle == title);
        
        return !(mission == null || mission.MissionId == 0);
    }

    public async Task<bool> IsExistAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DbModels.Mission mission = await _missionRepo.GetAsync(x => x.MissionId == id);
        
        return !(mission == null || mission.MissionId == 0);
    }

    public async Task<bool> IsValidRegistraionCriteria(long missionId, long userId)
    {
        CI_Platform_Backend_DBEntity.DbModels.Mission mission = await _missionRepo.GetAsync(x => x.MissionId == missionId && x.MissionRegistrationDeadline >= DateOnly.FromDateTime(DateTime.Now) && x.TotalSeats > x.MissionApplications.Count(x=>x.IsApproved == true && x.DeletedAt == null));
        
        return !(mission == null || mission.MissionId == 0);
    }

    public async Task<bool> AddAsync(long userId, CreateMissionDTO createMissionDTO)
    {
        string countryName = await _countryRepo.GetNameAsync(createMissionDTO.CountryId);
        string cityName = await _cityRepo.GetNameAsync(createMissionDTO.CityId);
        string themeName = await _themeRepo.GetNameAsync(createMissionDTO.ThemeId);
        string skillNames = String.Join(", ", await _skillRepo.GetAsync(createMissionDTO.SkillIds));
        string availability = Enum.GetName(typeof(AVAILABILITY), createMissionDTO.Availability);

        if(string.IsNullOrEmpty(countryName) || string.IsNullOrEmpty(cityName) || string.IsNullOrEmpty(themeName) || string.IsNullOrEmpty(availability))
        {
            return false;
        }
        
        CI_Platform_Backend_DBEntity.DbModels.Mission mission = new CI_Platform_Backend_DBEntity.DbModels.Mission()
        {
            MissionTitle = createMissionDTO.Title,
            MissionShortDescription = createMissionDTO.Description.Length >= 256 ? createMissionDTO.Description.Substring(0, 256) : createMissionDTO.Description,
            MissionDescription = createMissionDTO.Description,
            Country = countryName,
            City = cityName,
            MissionOrganisationName = createMissionDTO.OrganisationName,
            MissionOrganisationDetail = createMissionDTO.OrganisationDetails,
            MissionStartDate = createMissionDTO.StartDate,
            MissionEndDate = createMissionDTO.EndDate,
            MissionType = createMissionDTO.MissionType == 1 ? "Time" : "Goal",
            TotalSeats = createMissionDTO.TotalSeats,
            MissionRegistrationDeadline = createMissionDTO.RegistrationDeadline,
            MissionTheme = themeName,
            MissionSkills = skillNames,
            MissionAvailability = availability,
            Status = 1
        };

        if(createMissionDTO.MissionType == 2 && !string.IsNullOrEmpty(createMissionDTO.Goal))
        {
            mission.MissionGoals = [
                new MissionGoal()
                {
                    Goal = createMissionDTO.Goal,
                    GoalStatus = 1
                }
            ];
        }

        if(createMissionDTO.Images?.Count > 0)
        {
            createMissionDTO.Images.ForEach(image => 
            {
                byte[] imageBytes;
                using (var item = new MemoryStream())
                {
                    image.CopyTo(item);
                    imageBytes = item.ToArray();
                }
                mission.MissionMedia.Add(new MissionMedium()
                {
                    Image = imageBytes,
                });
            });
        }

        if(createMissionDTO.Documents?.Count > 0)
        {
            createMissionDTO.Documents.ForEach(document => 
            {
                byte[] documentBytes;
                using (var item = new MemoryStream())
                {
                    document.CopyTo(item);
                    documentBytes = item.ToArray();
                }
                mission.MissionMedia.Add(new MissionMedium()
                {
                    Document = documentBytes,
                });
            });
        }
        var a = await _missionRepo.AddAsync(mission);
        return true;
    }

    public async Task<List<MissionDTO>> GetAllAsync(long userId)
    {
        List<MissionDTO> missionDTOs = new List<MissionDTO>();
        List<CI_Platform_Backend_DBEntity.DbModels.Mission> missions = await _missionRepo.GetMissionsAsync();

        foreach (var mission in missions)
        {
            missionDTOs.Add(new MissionDTO()
            {
                MissionId = mission.MissionId,
                Theme = mission.MissionTheme,
                Title = mission.MissionTitle,
                ShortDescription = mission.MissionShortDescription,
                StartDate = mission.MissionStartDate,
                EndDate = mission.MissionEndDate,
                RegistrationDeadline = mission.MissionRegistrationDeadline,
                TotalSeats = mission.TotalSeats,
                SeatsLeft = mission.TotalSeats != null ? mission.TotalSeats.Value - mission.Volunteers.Count(x=>x.DeletedAt == null) : 0,
                Goal = "",
                GoalStatus = 1,
                OrganisationName = mission.MissionOrganisationName,
                Ratings = mission.MissionRating,
                Thumbnail = mission.MissionMedia.FirstOrDefault(x => x.Image != null).Image,
                Country = mission.Country,
                City = mission.City,
                // CountryId = mission.Country,
                // CityId = mission.City,
                Status = mission.Status,
                // Skills = mission.Skills,
                missionUserDTO = new MissionUserDTO()
                {
                    UserId = userId,
                    IsApplied = mission.MissionApplications.Any(x=>x.UserId == userId),
                    IsFavourite = mission.MissionFavs.Any(x=>x.UserId == userId),
                }
            });
        }
        return missionDTOs;
    }

    public async Task<MissionDetailsDTO> GetAsync(long userId, long missionId)
    {
        CI_Platform_Backend_DBEntity.DbModels.Mission mission = await _missionRepo.GetWithAllDataAsync(userId, missionId);
        
        return new MissionDetailsDTO()
        {
            MissionId = mission.MissionId,
            Thumbnail = mission.MissionMedia?.FirstOrDefault(x=>x.Image != null)?.Image,
            Title = mission.MissionTitle,
            Ratings = mission.MissionRating != null ? mission.MissionRating.Value : 0,
            ShortDescription = mission.MissionShortDescription,
            StartDate = mission.MissionStartDate,
            EndDate = mission.MissionEndDate,
            TotalSeats = mission.TotalSeats != null ? mission.TotalSeats.Value : 0,
            SeatsLeft = mission.TotalSeats != null ? mission.TotalSeats.Value - mission.Volunteers.Count : 0, //Update 
            RegistrationDeadline = mission.MissionRegistrationDeadline != null ? mission.MissionRegistrationDeadline.Value : null,
            City = mission.City,
            Country = mission.Country,
            Theme = mission.MissionTheme,
            OrganisationName = mission.MissionOrganisationName,
            Media = mission.MissionMedia != null ? mission.MissionMedia.Where(x=>x.Image != null).Select(x=>x.Image).ToArray() : null,
            Description = mission.MissionDescription,
            OrganisationDetails = mission.MissionOrganisationDetail,
            Documents = mission.MissionMedia != null ? mission.MissionMedia.Where(x=>x.Document != null).Select(x=>x.Document).ToArray() : null,
            Skills = mission.MissionSkills?.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList(),
            Availability = mission.MissionAvailability,
            RatingCount = mission.MissionRatingCount!= null? mission.MissionRatingCount.Value : 0,
            RecentVolunteers = mission.Volunteers != null ? mission.Comments.Select(x=>new RecentVolunteerDTO()
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    ProfileImage = x.User.Avatar,
                    CreatedAt = x.CreatedAt,
                }).ToList() : null,
            VolunteerCount = mission.Volunteers != null ? mission.Volunteers.Count : 0,
            IsFavourite = mission.MissionFavs.Count != 0,
            Goal = mission.MissionGoals !=null ? mission.MissionGoals.FirstOrDefault()?.Goal : null,
        };
    }

    public async Task<List<RelatedMissionDTO>> RelatedMissionsAsync(long userId, long missionId)
    {
        return await _missionRepo.GetRelatedMissionsAsync(missionId, userId);
    }

    public async Task<bool> ApplyAsync(long userId, long missionId)
    {
        MissionApplication missionApplication = await _missionApplicationRepo.GetAsync(x=>x.MissionId == missionId && x.UserId == userId && x.DeletedAt == null);
        return (missionApplication == null || missionApplication.ApplicationId == 0) ? await _missionApplicationRepo.AddAsync(new MissionApplication()
        {
            UserId = userId,
            MissionId = missionId,
        }) : false;
    }

    public async Task<bool> ApproveAsync(long userId, long missionId)
    {
        CI_Platform_Backend_DBEntity.DbModels.Mission mission = await _missionRepo.GetAsync(x=>x.MissionId == missionId && x.TotalSeats > x.MissionApplications.Count(x=>x.IsApproved == true && x.DeletedAt == null));
        
        if(mission == null || mission.MissionId == 0)
        {
            return false;
        }

        MissionApplication missionApplication = await _missionApplicationRepo.GetAsync(x=>x.MissionId == missionId && x.UserId == userId);
        if(missionApplication == null || missionApplication.MissionId == 0)
        {
            return false;
        }
        missionApplication.IsApproved = true;
        return await _missionApplicationRepo.UpdateAsync(missionApplication);
    }

    public async Task<bool> DeclineAsync(long userId, long missionId)
    {
        MissionApplication missionApplication = await _missionApplicationRepo.GetAsync(x=>x.MissionId == missionId && x.UserId == userId);
        if(missionApplication == null || missionApplication.MissionId == 0)
        {
            return false;
        }
        missionApplication.IsApproved = false;
        return await _missionApplicationRepo.UpdateAsync(missionApplication);
    }

}
