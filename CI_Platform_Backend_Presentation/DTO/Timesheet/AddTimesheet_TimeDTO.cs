using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.Timesheet;

public class AddTimesheet_TimeDTO
{
    [Required]
    public long MissionId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly Hours { get; set; }

    [Required]
    public TimeOnly Minutes { get; set; }

    [Required]
    public string Notes { get; set; }
}
