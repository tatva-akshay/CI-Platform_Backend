using System.Linq;
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

    public async Task<List<CI_Platform_Backend_DBEntity.DbModels.Mission>> GetMissionsAsync(long userId, List<string> themes, List<string> skills, List<string> countries, List<string> cities, int page, int pageSize, string search, string orderBy)
    {
        page = page == 0 ? 1 : page;
        pageSize = pageSize == 0 ? 10 : pageSize;
        IQueryable<CI_Platform_Backend_DBEntity.DbModels.Mission> missions = _dbContext.Missions.Include(x => x.MissionMedia).Include(x => x.Volunteers).Include(x => x.MissionFavs).Include(x => x.MissionApplications)
            .Where(x =>
                (themes.Count == 0 || themes.Contains(x.MissionTheme)) &&
                (skills.Count == 0 || (x.MissionSkills != null && skills.Any(skill => x.MissionSkills.Contains(skill)))) &&
                (countries.Count == 0 || countries.Contains(x.Country)) &&
                (cities.Count == 0 || cities.Contains(x.City)) &&
                (string.IsNullOrEmpty(search) ||
                    (x.MissionTitle.ToLower().Contains(search.ToLower())) ||
                    (x.MissionOrganisationName.ToLower().Contains(search.ToLower())) ||
                    (x.MissionTheme.ToLower().Contains(search.ToLower())) ||
                    (x.Country.ToLower().Contains(search.ToLower())) ||
                    (x.MissionSkills == null || x.MissionSkills.ToLower().Contains(search.ToLower())) ||
                    (x.City.ToLower().Contains(search.ToLower()))
                )
            );
        if (string.IsNullOrEmpty(orderBy))
        {
            return await missions.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        List<long> missionFavs = _dbContext.MissionFavs.Where(x=>x.UserId == 2).Select(x=>x.MissionId).ToList();
        return orderBy switch
        {
            "newest" => await missions.OrderByDescending(x => x.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            "oldest" => await missions.OrderBy(x => x.City).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            "lowestAvailableSeats" => await missions.OrderBy(x => x.TotalSeats - x.Volunteers.Count()).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            "highestAvailableSeats" => await missions.OrderByDescending(x => x.TotalSeats - x.Volunteers.Count()).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            "myFavs" => await missions.OrderByDescending(x=>x.MissionFavs.Count(x=>x.UserId == userId)).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            "registrationDeadline" => await missions.OrderByDescending(x => x.MissionRegistrationDeadline).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            _ => await missions.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),

        };
                
        
    }
    
    public async Task<int> GetMissionsCountAsync(List<string> themes, List<string> skills, List<string> countries, List<string> cities, string search)
    {        
        return await _dbContext.Missions.Include(x => x.MissionMedia).Include(x => x.Volunteers).Include(x=>x.MissionFavs).Include(x=>x.MissionApplications)
            .Where(x=> 
                (themes.Count == 0 || themes.Contains(x.MissionTheme)) &&
                (skills.Count == 0 || (x.MissionSkills != null && skills.Any(skill => x.MissionSkills.Contains(skill)))) &&
                (countries.Count == 0 || countries.Contains(x.Country)) &&
                (cities.Count == 0 || cities.Contains(x.City)) &&
                 (string.IsNullOrEmpty(search) ||
                    (x.MissionTitle.ToLower().Contains(search.ToLower())) ||
                    (x.MissionOrganisationName.ToLower().Contains(search.ToLower())) ||
                    (x.MissionTheme.ToLower().Contains(search.ToLower())) ||
                    (x.Country.ToLower().Contains(search.ToLower())) ||
                    (x.MissionSkills == null || x.MissionSkills.ToLower().Contains(search.ToLower())) ||
                    (x.City.ToLower().Contains(search.ToLower()))
                )
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
