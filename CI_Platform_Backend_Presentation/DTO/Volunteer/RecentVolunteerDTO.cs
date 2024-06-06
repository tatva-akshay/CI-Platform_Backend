namespace CI_Platform_Backend_Presentation.DTO.Volunteer;

public class RecentVolunteerDTO
{
    public long UserId { get; set; }

    public string UserName { get; set; }

    public byte[] ProfileImage { get; set; }

    public DateTime CreatedAt { get; set; }
}
