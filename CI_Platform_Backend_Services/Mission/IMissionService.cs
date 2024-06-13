using CI_Platform_Backend_Presentation.DTO.Mission;

namespace CI_Platform_Backend_Services.Mission;

public interface IMissionService
{
    Task<bool> IsExistAsync(string title);

    Task<bool> IsExistAsync(long missionId);

    Task<bool> IsValidRegistraionCriteria(long missionId, long userId);

    Task<bool> AddAsync(long userId, CreateMissionDTO createMissionDTO);

    Task<List<MissionDTO>> GetAllAsync(long userId, List<string> themes, List<string> skills, List<string> countries, List<string> cities, int page, int pageSize);

    Task<int> GetMissionsCountAsync(List<string> themes, List<string> skills, List<string> countries, List<string> cities);

    Task<MissionDetailsDTO> GetAsync(long userId, long missionId);

    Task<List<RelatedMissionDTO>> RelatedMissionsAsync(long userId, long missionId);

    Task<bool> ApplyAsync(long userId, long missionId);

    Task<bool> ApproveAsync(long userId, long missionId);

    Task<bool> DeclineAsync(long userId, long missionId);
}
