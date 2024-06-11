using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("mission_media")]
public partial class MissionMedium
{
    [Key]
    [Column("media_id")]
    public long MediaId { get; set; }

    [Column("image")]
    public byte[]? Image { get; set; }

    [Column("document")]
    public byte[]? Document { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionMedia")]
    public virtual Mission Mission { get; set; } = null!;
}
