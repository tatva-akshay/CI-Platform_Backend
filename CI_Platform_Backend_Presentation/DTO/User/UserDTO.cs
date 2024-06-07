using Microsoft.AspNetCore.Http;

namespace CI_Platform_Backend_Presentation.DTO.User;

public class UserDTO
{
    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? EmployeeId { get; set; }

    public string? Title { get; set; }

    public string? Department { get; set; }

    public string? Summary { get; set; }

    public string? WhyIVolunteer { get; set; }

    public long? CountryId { get; set; }

    public long? CityId { get; set; }

    public int? Availability { get; set; }

    public int? Gender { get; set; }

    public string? Description { get; set; }

    public int? AgeGroup { get; set; }

    public string Email { get; set; }


    public string? Skills { get; set; }

    public byte[]? ProfileImage { get; set; }

}
