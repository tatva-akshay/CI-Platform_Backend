using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("mission_application")]
public partial class MissionApplication
{
    [Key]
    [Column("application_id")]
    public long ApplicationId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [Column("is_approved")]
    public bool? IsApproved { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionApplications")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("MissionApplications")]
    public virtual User User { get; set; } = null!;
}
