using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("volunteers")]
public partial class Volunteer
{
    [Key]
    [Column("volunteer_id")]
    public long VolunteerId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("status")]
    public int Status { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("Volunteers")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Volunteers")]
    public virtual User User { get; set; } = null!;
}
