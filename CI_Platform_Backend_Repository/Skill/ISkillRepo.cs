using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.Skill;

public interface ISkillRepo : IRepository<CI_Platform_Backend_DBEntity.DataModels.Skill>
{
    Task<List<string>> GetAsync(List<long> skillIDs);

}
