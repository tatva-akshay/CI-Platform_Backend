namespace CI_Platform_Backend_Presentation.DTO.Mission;

public class RelatedMissionDTO
{
    public long MissionId { get; set; }

    public byte[]? Thumbnail { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Title { get; set; }

    public string ShortDescription { get; set; }   

    public string OrganisationName { get; set; }

    public int Rating { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateOnly RegistrationDeadline { get; set; }

    public long TotalSeats { get; set; }

    public long SeatsLeft { get; set; } 

    public bool IsFavourite { get; set; }

    public bool IsApplied { get; set; }

    public string Theme { get; set; }

    public string? Goal {  get; set; }
}
