using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("recent_volunteers")]
public partial class RecentVolunteer
{
    [Key]
    [Column("volunteer_id")]
    public long VolunteerId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("RecentVolunteers")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("RecentVolunteers")]
    public virtual User User { get; set; } = null!;
}
