using CI_Platform_Backend_Presentation.DTO.Comment;

namespace CI_Platform_Backend_Services.Comment;

public interface ICommentService
{
    Task<List<CommentDTO>> GetByMissionAsync(long missionId);

    Task<bool> AddAsync(CreateCommentDTO createCommentDTO);
}
