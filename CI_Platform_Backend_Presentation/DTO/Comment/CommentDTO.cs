namespace CI_Platform_Backend_Presentation.DTO.Comment;

public class CommentDTO
{
    public long CommentId { get; set; }

    public long UserId { get; set; }

    public byte[]? Avatar {  get; set; }

    public string UserName { get; set; }

    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; }
}
