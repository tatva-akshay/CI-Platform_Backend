using System.Reflection.Metadata;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.Mission;
using CI_Platform_Backend_Presentation.DTO.Volunteer;
using CI_Platform_Backend_Repository.City;
using CI_Platform_Backend_Repository.Country;
using CI_Platform_Backend_Repository.Mission;
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

    private readonly ISkillRepo _skillRepo;
    public MissionService(IMissionRepo missionRepo, ICountryRepo countryRepo, ICityRepo cityRepo, IThemeRepo themeRepo, ISkillRepo skillRepo)
    {
        _missionRepo = missionRepo;
        _countryRepo = countryRepo;
        _cityRepo = cityRepo;
        _themeRepo = themeRepo;
        _skillRepo = skillRepo;
    }

    public async Task<bool> IsExistAsync(string title)
    {
        CI_Platform_Backend_DBEntity.DataModels.Mission mission = await _missionRepo.GetAsync(x => x.MissionTitle == title);
        if (mission == null || mission.MissionId == 0)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> IsExistAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DataModels.Mission mission = await _missionRepo.GetAsync(x => x.MissionId == id);
        if (mission == null || mission.MissionId == 0)
        {
            return false;
        }
        return true;
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
        CI_Platform_Backend_DBEntity.DataModels.Mission mission = new CI_Platform_Backend_DBEntity.DataModels.Mission()
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
            Status = 1,
            MissionApplications = [
                new MissionApplication()
                {
                    UserId = userId  ,
                    CreatedAt = DateTime.Now,
                }
            ],
            MissionGoals = createMissionDTO.MissionType == 2 ? [
                new MissionGoal()
                {
                    Goal = createMissionDTO.Goal,
                    GoalStatus = 1
                }
            ] : [],
        };

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
        await _missionRepo.AddAsync(mission);
        return true;
    }

    public async Task<List<MissionDTO>> GetAllAsync(long userId)
    {
        List<MissionDTO> missionDTOs = new List<MissionDTO>();
        List<CI_Platform_Backend_DBEntity.DataModels.Mission> missions = await _missionRepo.GetAsync();

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
                SeatsLeft = 0,
                Goal = "",
                GoalStatus = 1,
                OrganisationName = mission.MissionOrganisationName,
                Ratings = mission.MissionRating,
                // Thumbnail = mission.MissionThumbnail,
                Country = mission.Country,
                City = mission.City,
                // CountryId = mission.Country,
                // CityId = mission.City,
                Status = mission.Status,
                // Skills = mission.Skills,
                missionUserDTO = new MissionUserDTO()
                {
                    UserId = userId,
                    IsApplied = false,
                    IsFavourite = false,
                }
            });
        }
        return missionDTOs;
    }

    public async Task<MissionDetailsDTO> GetAsync(long userId, long missionId)
    {
        CI_Platform_Backend_DBEntity.DataModels.Mission mission = await _missionRepo.GetWithAllDataAsync(userId, missionId);
        Country country= await _countryRepo.GetAsync(x=>x.Country1 == mission.Country);
        City city= await _cityRepo.GetAsync(x=>x.City1 == mission.City);
        CI_Platform_Backend_DBEntity.DataModels.Theme theme = await _themeRepo.GetAsync(x=>x.Theme1 == mission.MissionTheme);
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
            Comments = mission.Comments != null ? mission.Comments.Select(x=>new CI_Platform_Backend_Presentation.DTO.Comment.CommentDTO()
                {
                    CommentId = x.CommentId,
                    UserId = x.UserId,
                    Comment = x.Comment1,
                    UserName = x.UserName,
                    CreatedAt = x.CreatedAt,

                }).ToList() : null,
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
            RelatedMissions = await _missionRepo.GetRelatedMissionsAsync(country.CountryId, city.CityId, theme.ThemeId),
            IsFavourite = mission.MissionFavs.Count != 0,
            Goal = mission.MissionGoals !=null ? mission.MissionGoals.FirstOrDefault()?.Goal : null,
        };
    }


}
