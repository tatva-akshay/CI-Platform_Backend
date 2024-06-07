using CI_Platform_Backend_Presentation.DTO.Mission;

namespace CI_Platform_Backend_Services.Mission;

public interface IMissionService
{
    Task<bool> IsExistAsync(string title);

    Task<bool> IsExistAsync(long missionId);

    Task<bool> AddAsync(long userId, CreateMissionDTO createMissionDTO);

    Task<List<MissionDTO>> GetAllAsync(long userId);

    Task<MissionDetailsDTO> GetAsync(long userId, long missionId);

    Task<List<RelatedMissionDTO>> RelatedMissionsAsync(long userId, long missionId);
}
