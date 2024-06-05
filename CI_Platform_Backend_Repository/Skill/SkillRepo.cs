using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Skill;

public class SkillRepo : Repository<CI_Platform_Backend_DBEntity.DataModels.Skill>, ISkillRepo
{
    private readonly ApplicationDbContext _dbContext;

    public SkillRepo(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<string>> GetAsync(List<long> skillIDs)
    {
        return await _dbContext.Skills.Where(c => skillIDs.Contains(c.SkillId) && c.Status).Select(x=>x.Skills).ToListAsync();
    }

}
