using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using CI_Platform_Backend_DBEntity.Context;
namespace CI_Platform_Backend_Presentation.DTO.Mission;

public class CreateMissionDTO
{

    [Required]
    public string Country { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    [MaxLength(128)]
    [MinLength(3)]
    public string Title { get; set; }

    [Required]
    [MaxLength(256)]
    [MinLength(3)]
    public string Description { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string OrganisationName { get; set; }
    
    [Required]
    [MaxLength(2048)]
    [MinLength(3)]
    public string OrganisationDetails { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    public long? TotalSeats { get; set; }

    public DateOnly? RegistrationDeadline { get; set; }

    [Required]
    public string Theme { get; set; }

    public List<string>? Skills { get; set; }

    public List<IFormFile>? Images { get; set; }

    //public byte[][]? Documents { get; set; }

    [Required]
    public string Availability { get; set; }

    [Required]
    [Range(0, 3)]
    public int MissionType { get; set; }

    public string? Goal { get; set; }    
    
}
