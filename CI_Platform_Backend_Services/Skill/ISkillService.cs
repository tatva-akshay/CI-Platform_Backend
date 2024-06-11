using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.CreateSkill;

namespace CI_Platform_Backend_Services.Skill;

public interface ISkillService
{
    Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DbModels.Skill skill);
    
    Task<bool> UpdateAsync(long id, CreateSkillDTO skillDTO);

    Task<bool> DeleteAsync(long id);

    Task<List<CI_Platform_Backend_DBEntity.DbModels.Skill>> GetSkillsAsync();

    Task<CI_Platform_Backend_DBEntity.DbModels.Skill> GetSkillAsync(long id);
}
