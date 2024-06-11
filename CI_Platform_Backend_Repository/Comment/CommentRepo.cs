using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Presentation.DTO.Comment;
using CI_Platform_Backend_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Comment;

public class CommentRepo : Repository<CI_Platform_Backend_DBEntity.DbModels.Comment>, ICommentRepo
{
    private readonly CIPlatformDbContext _dbContext;

    public CommentRepo(CIPlatformDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CommentDTO>> GetByMissionAsync(long missionId)
    {
        return await _dbContext.Comments.Where(c => c.MissionId == missionId).Select(c => new CommentDTO()
        {
            CommentId = c.CommentId,
            UserId = c.UserId,
            UserName = c.UserName,
            Comment = c.Comment1,
            CreatedAt = c.CreatedAt
        }).ToListAsync();
    }


}
