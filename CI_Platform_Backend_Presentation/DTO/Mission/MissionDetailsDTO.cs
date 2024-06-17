using CI_Platform_Backend_Presentation.DTO.Comment;
using CI_Platform_Backend_Presentation.DTO.Volunteer;
using Microsoft.EntityFrameworkCore.Storage;

namespace CI_Platform_Backend_Presentation.DTO.Mission;

public class MissionDetailsDTO
{
    public long MissionId { get; set; }
    
    public byte[] Thumbnail { get; set; }

    public string Title { get; set; }

    public int Ratings { get; set; }

    public string ShortDescription { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public long TotalSeats { get; set; }

    public long SeatsLeft { get; set; } 

    public DateOnly? RegistrationDeadline { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Theme { get; set; }

    public string OrganisationName { get; set; }

    public byte[]?[] Media { get; set; }

    public string Description { get; set; }

    public string OrganisationDetails { get; set; }

    public byte[]?[] Documents { get; set; }

    public List<string> Skills { get; set; }

    public string Availability { get; set; }

    public long RatingCount { get; set; }

    public List<RecentVolunteerDTO>? RecentVolunteers{ get; set; }

    public long VolunteerCount { get; set; }

    public bool IsFavourite { get; set; }

    public string? Goal { get; set; }

    public bool? IsApplied { get; set; }
}
