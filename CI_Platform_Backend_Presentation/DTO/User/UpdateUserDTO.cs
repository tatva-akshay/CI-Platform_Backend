using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.User;

public class UpdateUserDTO
{
    [Required]
    [MinLength(2)]
    [MaxLength(16)]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set;}

    [MaxLength(16)]
    public string? EmployeeId { get; set; }

    [MaxLength(255)]
    public string? Title { get; set; }

    [MaxLength(16)]
    public string? Department { get; set; }

    public string? ProfileSummary { get; set; }

    public string? WhyIVolunteer { get; set; }

    [Required]
    [Range(1, long.MaxValue)]
    public long CountryId { get; set; }

    [Required]
    [Range(1, long.MaxValue)]
    public long CityId { get; set; }

    [Required]
    [Range(1, 3)]
    public int Availability { get; set; }

    [Required]
    [Range(1, 3)]
    public int Gender { get; set; }

    [Required]
    [Range(1, 6)]
    public int AgeGroup { get; set; }
    
    [Required]
    [MaxLength(128)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; }

}
