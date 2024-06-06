using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("mission_goals")]
public partial class MissionGoal
{
    [Key]
    [Column("goal_id")]
    public long GoalId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("goal")]
    [StringLength(100)]
    [Unicode(false)]
    public string Goal { get; set; } = null!;

    [Column("goal_status")]
    public int? GoalStatus { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionGoals")]
    public virtual Mission Mission { get; set; } = null!;
}
