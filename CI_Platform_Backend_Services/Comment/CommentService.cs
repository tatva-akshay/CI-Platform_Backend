using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.Comment;
using CI_Platform_Backend_Repository.Comment;
using CI_Platform_Backend_Repository.Mission;
using CI_Platform_Backend_Repository.UserRepo;

namespace CI_Platform_Backend_Services.Comment;

public class CommentService : ICommentService
{
    private readonly ICommentRepo _commentRepo;

    private readonly  IMissionRepo _missionRepo;

    private readonly IUserRepo _userRepo;

    public CommentService(ICommentRepo commentRepo, IUserRepo userRepo, IMissionRepo missionRepo)
    {
        _commentRepo = commentRepo;
        _userRepo = userRepo;
        _missionRepo = missionRepo;
    }

    public async Task<List<CommentDTO>> GetByMissionAsync(long missionId)
    {
        return await _commentRepo.GetByMissionAsync(missionId);
    }

    public async Task<bool> AddAsync(CreateCommentDTO createCommentDTO)
    {
        CI_Platform_Backend_DBEntity.DbModels.Comment comment = await _commentRepo.GetAsync(x => x.UserId == createCommentDTO.UserId && x.MissionId == createCommentDTO.MissionId);

        if(comment != null && comment.CommentId > 0)
        {
            comment.Comment1 = createCommentDTO.Comment;
            return await _commentRepo.UpdateAsync(comment);
        }

        CI_Platform_Backend_DBEntity.DbModels.User user = await _userRepo.GetAsync(x => x.UserId == createCommentDTO.UserId);
        
        if(user == null || user.UserId == 0)
        {
            return false;
        }
        
        CI_Platform_Backend_DBEntity.DbModels.Mission mission= await _missionRepo.GetAsync(x =>x.MissionId == createCommentDTO.MissionId);
        
        return mission != null && mission.MissionId != 0 && await _commentRepo.AddAsync(new CI_Platform_Backend_DBEntity.DbModels.Comment()
            {
                UserId = createCommentDTO.UserId,
                MissionId = createCommentDTO.MissionId,
                UserName = user.FirstName + " " + user.LastName,
                Comment1 = createCommentDTO.Comment,
                MissionTitle = mission.MissionTitle,
            });
    }
}