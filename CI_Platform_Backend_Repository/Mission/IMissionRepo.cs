using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.Mission;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.Mission;

public interface IMissionRepo : IRepository<CI_Platform_Backend_DBEntity.DbModels.Mission>
{

    Task<CI_Platform_Backend_DBEntity.DbModels.Mission> GetWithAllDataAsync(long userId, long missionId);

    Task<List<RelatedMissionDTO>> GetRelatedMissionsAsync(long missionId, long userId);

    Task<List<CI_Platform_Backend_DBEntity.DbModels.Mission>> GetMissionsAsync(long userId, List<string> themes, List<string> skills, List<string> countries, List<string> cities, int page, int pageSize, string search, string orderBy);

    Task<int> GetMissionsCountAsync(List<string> themes, List<string> skills, List<string> countries, List<string> cities, string search);
}
