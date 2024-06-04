using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("volunteering_timesheet")]
public partial class VolunteeringTimesheet
{
    [Key]
    [Column("volunteering_id")]
    public long VolunteeringId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("mission_title")]
    [StringLength(50)]
    [Unicode(false)]
    public string MissionTitle { get; set; } = null!;

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("hours")]
    public TimeOnly Hours { get; set; }

    [Column("minutes")]
    public TimeOnly Minutes { get; set; }

    [Column("action")]
    public long Action { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("VolunteeringTimesheets")]
    public virtual Mission Mission { get; set; } = null!;
}
