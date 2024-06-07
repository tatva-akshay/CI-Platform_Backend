using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.Mission;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.Mission;

public interface IMissionRepo : IRepository<CI_Platform_Backend_DBEntity.DataModels.Mission>
{

    Task<CI_Platform_Backend_DBEntity.DataModels.Mission> GetWithAllDataAsync(long userId, long missionId);

    Task<List<RelatedMissionDTO>> GetRelatedMissionsAsync(long missionId, long userId);
}
