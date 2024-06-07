namespace CI_Platform_Backend_Presentation.DTO.Comment;

public class CreateCommentDTO
{
    public long MissionId { get; set; }

    public long UserId { get; set;}

    public string Comment { get; set; }
}
