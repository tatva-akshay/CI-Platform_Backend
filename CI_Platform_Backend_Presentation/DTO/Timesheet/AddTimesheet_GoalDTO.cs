using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.Timesheet;

public class AddTimesheet_GoalDTO
{
    [Required]
    public long MissionId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public long Action { get; set; }

    [Required]
    public string Notes { get; set; }
}
