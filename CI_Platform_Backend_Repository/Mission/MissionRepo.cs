using System.Security.Cryptography.X509Certificates;
using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.Mission;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Mission;

public class MissionRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.Mission>, IMissionRepo
{

    private readonly CIPlatformDbContext _dbContext;

    public MissionRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CI_Platform_Backend_DBEntity.DbModels.Mission> GetWithAllDataAsync(long userId, long missionId)
    {
        return await _dbContext.Missions
            .Include(x=>x.MissionMedia)
            .Include(x=>x.MissionGoals)
            .Include(x=>x.MissionFavs.Where(x=>x.UserId == userId))
            .Include(x=>x.Volunteers)
            .ThenInclude(x=>x.User)
            .FirstOrDefaultAsync(x=>x.MissionId == missionId);
    }

    public async Task<List<CI_Platform_Backend_DBEntity.DbModels.Mission>> GetMissionsAsync(List<string> themes, List<string> skills, List<string> countries, List<string> cities, int page, int pageSize)
    {
        page = page == 0 ? 1 : page;
        pageSize = pageSize == 0 ? 10 : pageSize;
        return await _dbContext.Missions.Include(x => x.MissionMedia).Include(x => x.Volunteers).Include(x=>x.MissionFavs).Include(x=>x.MissionApplications)
            .Where(x=> 
                (themes.Count == 0 || themes.Contains(x.MissionTheme)) &&
                (skills.Count == 0 || (x.MissionSkills != null && skills.Any(skill => x.MissionSkills.Contains(skill)))) &&
                (countries.Count == 0 || countries.Contains(x.Country)) &&
                (cities.Count == 0 || cities.Contains(x.City))
            )
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToListAsync();
        
    }
    
    public async Task<int> GetMissionsCountAsync(List<string> themes, List<string> skills, List<string> countries, List<string> cities)
    {        
        return await _dbContext.Missions.Include(x => x.MissionMedia).Include(x => x.Volunteers).Include(x=>x.MissionFavs).Include(x=>x.MissionApplications)
            .Where(x=> 
                (themes.Count == 0 || themes.Contains(x.MissionTheme)) &&
                //(skills.Count == 0 || skills.Any(skill=> )) &&
                (countries.Count == 0 || countries.Contains(x.Country)) &&
                (cities.Count == 0 || cities.Contains(x.City))
            )
            .CountAsync();
        
    }

    public async Task<List<RelatedMissionDTO>> GetRelatedMissionsAsync(long missionId, long userId)
    {
        List<RelatedMissionDTO> relatedMissionDTOs = new List<RelatedMissionDTO>();
        CI_Platform_Backend_DBEntity.DbModels.Mission mission = await _dbContext.Missions.FirstOrDefaultAsync(x=>x.MissionId == missionId);
        if(mission == null || mission.MissionId == 0)
        {
            return relatedMissionDTOs;
        }
        relatedMissionDTOs.AddRange(await _dbContext.Missions.Where(x=>x.Country == mission.Country && x.MissionId != missionId && !relatedMissionDTOs.Select(x=>x.MissionId).Contains(x.MissionId)).Take(3).Select(x=>new RelatedMissionDTO()
        {
            MissionId = x.MissionId,
            Status = x.Status,
            City = x.City,
            Country = x.Country,
            Title = x.MissionTitle,
            ShortDescription = x.MissionShortDescription,
            OrganisationName = x.MissionOrganisationName,
            Rating = x.MissionRating.Value,
            StartDate = x.MissionStartDate,
            EndDate = x.MissionEndDate,
            RegistrationDeadline = x.MissionRegistrationDeadline.Value,
            TotalSeats = x.TotalSeats.Value,
            SeatsLeft = x.TotalSeats.Value - x.Volunteers.Count,
            IsFavourite = x.MissionFavs.Any(x=>x.UserId == userId),
            Theme = x.MissionTheme,
        }).ToListAsync());
        if(relatedMissionDTOs.Count>=3)
        {
            return relatedMissionDTOs;
        }
        relatedMissionDTOs.AddRange(await _dbContext.Missions.Where(x=>x.City == mission.City && x.MissionId != missionId && !relatedMissionDTOs.Select(x=>x.MissionId).Contains(x.MissionId)).Take(3 - relatedMissionDTOs.Count).Select(x=>new RelatedMissionDTO()
        {
            MissionId = x.MissionId,
            Status = x.Status,
            City = x.City,
            Country = x.Country,
            Title = x.MissionTitle,
            ShortDescription = x.MissionShortDescription,
            OrganisationName = x.MissionOrganisationName,
            Rating = x.MissionRating.Value,
            StartDate = x.MissionStartDate,
            EndDate = x.MissionEndDate,
            RegistrationDeadline = x.MissionRegistrationDeadline.Value,
            TotalSeats = x.TotalSeats.Value,
            SeatsLeft = x.TotalSeats.Value - x.Volunteers.Count,
            IsFavourite = x.MissionFavs.Any(x=>x.UserId == userId),
            Theme = x.MissionTheme,
        }).ToListAsync());
        if(relatedMissionDTOs.Count>=3)
        {
            return relatedMissionDTOs;
        }
        relatedMissionDTOs.AddRange(await _dbContext.Missions.Where(x=>x.MissionTheme == mission.MissionTheme && x.MissionId != missionId && !relatedMissionDTOs.Select(x=>x.MissionId).Contains(x.MissionId)).Take(3 - relatedMissionDTOs.Count).Select(x=>new RelatedMissionDTO()
        {
            MissionId = x.MissionId,
            Status = x.Status,
            City = x.City,
            Country = x.Country,
            Title = x.MissionTitle,
            ShortDescription = x.MissionShortDescription,
            OrganisationName = x.MissionOrganisationName,
            Rating = x.MissionRating.Value,
            StartDate = x.MissionStartDate,
            EndDate = x.MissionEndDate,
            RegistrationDeadline = x.MissionRegistrationDeadline.Value,
            TotalSeats = x.TotalSeats.Value,
            SeatsLeft = x.TotalSeats.Value - x.Volunteers.Count,
            IsFavourite = x.MissionFavs.Any(x=>x.UserId == userId),
            Theme = x.MissionTheme,
        }).ToListAsync());
        return relatedMissionDTOs;
    }


}
