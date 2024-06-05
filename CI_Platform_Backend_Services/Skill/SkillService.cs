using CI_Platform_Backend_Presentation.DTO.CreateSkill;
using CI_Platform_Backend_Repository.Skill;

namespace CI_Platform_Backend_Services.Skill;

public class SkillService : ISkillService
{
    private readonly ISkillRepo _skillRepo;

    public SkillService(ISkillRepo skillRepo)
    {
        _skillRepo = skillRepo;
    }

    public async Task<bool> AddAsync(CI_Platform_Backend_DBEntity.DataModels.Skill skill)
    {
        CI_Platform_Backend_DBEntity.DataModels.Skill skillTemp = await _skillRepo.GetAsync(x => x.Skills == skill.Skills);
        if(skillTemp == null || skillTemp.SkillId == 0)
        {
            return await _skillRepo.AddAsync(skill);
        }
        return false;
    }
    
    public async Task<bool> UpdateAsync(long id, CreateSkillDTO skillDTO)
    {
        CI_Platform_Backend_DBEntity.DataModels.Skill skill = await _skillRepo.GetAsync(x => x.SkillId == id);
        if(skill == null || skill.SkillId == 0)
        {
            return false;
        }        

        skill.Skills = skillDTO.Name;
        skill.Status = skillDTO.IsActive;

        return await _skillRepo.UpdateAsync(skill);
    }

    public async Task<List<CI_Platform_Backend_DBEntity.DataModels.Skill>> GetSkillsAsync()
    {
        return await _skillRepo.GetAsync();
    }

    public async Task<CI_Platform_Backend_DBEntity.DataModels.Skill> GetSkillAsync(long id)
    {
        return await _skillRepo.GetAsync(x => x.SkillId == id);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DataModels.Skill skill = await _skillRepo.GetAsync(x => x.SkillId == id);
        if(skill == null || skill.SkillId == 0)
        {
            return false;
        }  
        return await _skillRepo.DeleteAsync(skill);
    }


}
