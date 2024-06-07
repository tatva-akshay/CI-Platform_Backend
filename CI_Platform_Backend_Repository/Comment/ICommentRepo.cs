using CI_Platform_Backend_Presentation.DTO.Comment;
using CI_Platform_Backend_Repository.Repository;

namespace CI_Platform_Backend_Repository.Comment;

public interface ICommentRepo : IRepository<CI_Platform_Backend_DBEntity.DataModels.Comment>
{

    Task<List<CommentDTO>> GetByMissionAsync(long missionId);

}
