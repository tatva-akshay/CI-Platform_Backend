using System.Security.Cryptography.X509Certificates;
using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Presentation.DTO.Mission;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Mission;

public class MissionRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.Mission>, IMissionRepo
{

    private readonly ApplicationDbContext _dbContext;

    public MissionRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CI_Platform_Backend_DBEntity.DataModels.Mission> GetWithAllDataAsync(long userId, long missionId)
    {
        return await _dbContext.Missions
            .Include(x=>x.MissionMedia)
            .Include(x=>x.MissionGoals)
            .Include(x=>x.MissionFavs.Where(x=>x.UserId == userId))
            .Include(x=>x.Volunteers)
            .ThenInclude(x=>x.User)
            .FirstOrDefaultAsync(x=>x.MissionId == missionId);
    }

    public async Task<List<RelatedMissionDTO>> GetRelatedMissionsAsync(long countryId, long cityId, long themeId)
    {
        return new List<RelatedMissionDTO>();
        // return await _dbContext.Missions.OrderBy(x=>x.).Take(3).Select(x=>new RelatedMissionDTO()
        // {

        // }).ToListAsync();
    }


}
