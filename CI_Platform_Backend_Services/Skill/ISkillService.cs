using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.CreateSkill;

namespace CI_Platform_Backend_Services.Skill;

public interface ISkillService
{
    Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DataModels.Skill skill);
    
    Task<bool> UpdateAsync(long id, CreateSkillDTO skillDTO);

    Task<bool> DeleteAsync(long id);

    Task<List<CI_Platform_Backend_DBEntity.DataModels.Skill>> GetSkillsAsync();

    Task<CI_Platform_Backend_DBEntity.DataModels.Skill> GetSkillAsync(long id);
}
