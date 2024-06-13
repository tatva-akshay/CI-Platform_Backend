namespace CI_Platform_Backend_Presentation.DTO.Mission;

public class MissionDTO
{
    public long MissionId { get; set; }

    public string Theme { get; set; }

    public string Title { get; set; }

    public string ShortDescription { get; set; }

    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }

    public DateOnly? RegistrationDeadline { get; set; }

    public long? TotalSeats { get; set; }

    public long SeatsLeft { get; set; }

    public string Goal { get; set; }

    public int GoalStatus { get; set; }

    public string OrganisationName { get; set; }

    public int? Ratings { get; set; }

    public byte[] Thumbnail { get; set; }

    public string Country { get; set; }

    public long? CountryId { get; set; }

    public string City { get; set; }

    public long? CityId { get; set; }

    public int Status { get; set; }

    public List<string> Skills { get; set; } = new List<string>();

    public MissionUserDTO missionUserDTO { get; set; }

}
